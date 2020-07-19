using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class StepScript : Agent
{

    public GameObject FRThigh, FLThigh, FRShin, FLShin, RRThigh, RLThigh, RRShin, RLShin, Pelvis;
    Rigidbody rBody;

    public float thighPosition, shinPosition = 0.0f;

    HingeJoint frtJoint, fltJoint, rrtJoint, rltJoint, frsJoint, flsJoint, rrsJoint, rlsJoint;

    public void SetJointTargetRotation(HingeJoint joint, float x) {
        x = (x + 1f) * 0.5f;

        var xRot = Mathf.Lerp(joint.limits.min, joint.limits.max, x);

        JointSpring hingeSpring = joint.spring;
        joint.useSpring = true;
        hingeSpring.spring = 1000f;
        hingeSpring.damper = 0.5f;
        hingeSpring.targetPosition = xRot;
        joint.spring = hingeSpring;
    }

    // Start is called before the first frame update
    void Start()
    {

        rBody = Pelvis.GetComponent<Rigidbody>();

        fltJoint = FLThigh.GetComponent<HingeJoint>();
        frtJoint = FRThigh.GetComponent<HingeJoint>();
        rrtJoint = RRThigh.GetComponent<HingeJoint>();
        rltJoint = RLThigh.GetComponent<HingeJoint>();

        frsJoint = FRShin.GetComponent<HingeJoint>();
        flsJoint = FLShin.GetComponent<HingeJoint>();
        rrsJoint = RRShin.GetComponent<HingeJoint>();
        rlsJoint = RLShin.GetComponent<HingeJoint>();
    }

    public override void OnEpisodeBegin() {
        // Reset velocity and position
        this.transform.localPosition = new Vector3(0, 0.755f, 0);
        rBody.angularVelocity = Vector3.zero;
        rBody.velocity = Vector3.zero;
        Pelvis.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public override void CollectObservations(VectorSensor sensor) {
        // Agent position and velocity
        sensor.AddObservation(Pelvis.transform.position);
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction) {
        // Front leg thighs are symmetrical
        SetJointTargetRotation(frtJoint, vectorAction[0]);
        SetJointTargetRotation(fltJoint, vectorAction[0]);

        // Front leg shins
        SetJointTargetRotation(frsJoint, vectorAction[1]);
        SetJointTargetRotation(flsJoint, vectorAction[1]);

        // Back leg thighs
        SetJointTargetRotation(rrtJoint, vectorAction[2]);
        SetJointTargetRotation(rltJoint, vectorAction[2]);

        // Back leg shins
        SetJointTargetRotation(rrsJoint, vectorAction[3]);
        SetJointTargetRotation(rlsJoint, vectorAction[3]);

        // Set Reward equal to distance that we have traveled forward
        SetReward(Pelvis.transform.position.z);

        // If we fall off platform
        if (Pelvis.transform.position.y < -3f) {
            AddReward(-10f);
            EndEpisode();
        }

        if (Vector3.Dot(Pelvis.transform.up, Vector3.down) > 0) {
            AddReward(-10f);
            EndEpisode();
        }

        // Set a target distance
        if (this.transform.localPosition.z > 100f) {
            EndEpisode();
        }
    }
}
