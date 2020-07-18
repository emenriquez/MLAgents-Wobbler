using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepScript : MonoBehaviour
{

    public GameObject FRThigh;
    public GameObject FLThigh;
    public GameObject FRShin;
    public GameObject FLShin;

    public float x_torque = 1000f;

    Rigidbody fr_rb;
    Rigidbody fl_rb;

    HingeJoint r_joint;


    // Start is called before the first frame update
    void Start()
    {
        r_joint = FRThigh.GetComponent<HingeJoint>();
        fr_rb = FRThigh.GetComponent<Rigidbody>();
        fl_rb = FLThigh.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // r_joint.targetPosition = new Vector3(-5, 0, 0);
    }
}
