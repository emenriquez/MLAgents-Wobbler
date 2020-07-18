using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepScript : MonoBehaviour
{

    public GameObject FRThigh;
    public GameObject FLThigh;
    public GameObject FRShin;
    public GameObject FLShin;
    public GameObject RRThigh;
    public GameObject RLThigh;
    public GameObject RRShin;
    public GameObject RLShin;

    public float thighPosition = 0.0f;
    public float shinPosition = 0.0f;

    Rigidbody frt_rb;
    Rigidbody flt_rb;
    Rigidbody frs_rb;
    Rigidbody fls_rb;

    HingeJoint frtJoint;
    HingeJoint fltJoint;
    HingeJoint rrtJoint;
    HingeJoint rltJoint;
    HingeJoint frsJoint;
    HingeJoint flsJoint;
    HingeJoint rrsJoint;
    HingeJoint rlsJoint;

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
        fltJoint = FLThigh.GetComponent<HingeJoint>();
        frtJoint = FRThigh.GetComponent<HingeJoint>();
        rrtJoint = RRThigh.GetComponent<HingeJoint>();
        rltJoint = RLThigh.GetComponent<HingeJoint>();

        frsJoint = FRShin.GetComponent<HingeJoint>();
        flsJoint = FLShin.GetComponent<HingeJoint>();
        rrsJoint = RRShin.GetComponent<HingeJoint>();
        rlsJoint = RLShin.GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SetJointTargetRotation(frtJoint, thighPosition);
        SetJointTargetRotation(fltJoint, thighPosition);
        SetJointTargetRotation(rrtJoint, thighPosition);
        SetJointTargetRotation(rltJoint, thighPosition);

        SetJointTargetRotation(frsJoint, shinPosition);
        SetJointTargetRotation(flsJoint, shinPosition);
        SetJointTargetRotation(rrsJoint, shinPosition);
        SetJointTargetRotation(rlsJoint, shinPosition);
    }
}
