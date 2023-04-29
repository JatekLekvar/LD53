using UnityEngine;
using System.Collections.Generic;

public class Forklift : MonoBehaviour
{
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float maxBrakeTorque;
    public float forkSpeed;
    public float forkMin;
    public float forkMax;
    public float forkDamp;

    WheelCollider frontLeft;
    WheelCollider frontRight;
    WheelCollider rearLeft;
    WheelCollider rearRight;

    Transform fork;
    Transform forkBase;

    float targetForkHeight;
    float forkHeight;

    public void Start()
    {
        Transform wheelBase = transform.GetChild(0);
        frontLeft = wheelBase.GetChild(0).GetComponent<WheelCollider>();
        frontRight = wheelBase.GetChild(1).GetComponent<WheelCollider>();
        rearLeft = wheelBase.GetChild(2).GetComponent<WheelCollider>();
        rearRight = wheelBase.GetChild(3).GetComponent<WheelCollider>();

        fork = transform.GetChild(1);
        forkBase = fork.GetChild(1);

        targetForkHeight = forkMin;
        forkHeight = targetForkHeight;

        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0.5f, 0);
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        float braking = 0f;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            braking = maxBrakeTorque;
        }

        frontLeft.steerAngle = steering;
        frontRight.steerAngle = steering;

        rearLeft.motorTorque = motor;
        rearRight.motorTorque = motor;

        frontLeft.brakeTorque = braking;
        frontRight.brakeTorque = braking;
        rearLeft.brakeTorque = braking;
        rearRight.brakeTorque = braking;

        SetVisuals(frontLeft);
        SetVisuals(frontRight);
        SetVisuals(rearLeft);
        SetVisuals(rearRight);

        if (Input.GetKey(KeyCode.Q))
        {
            targetForkHeight -= forkSpeed * Time.fixedDeltaTime;
            targetForkHeight = Mathf.Clamp(targetForkHeight, forkMin, forkMax);
        }

        if (Input.GetKey(KeyCode.E))
        {
            targetForkHeight += forkSpeed * Time.fixedDeltaTime;
            targetForkHeight = Mathf.Clamp(targetForkHeight, forkMin, forkMax);
        }

        forkHeight = Mathf.Lerp(forkHeight, targetForkHeight, forkDamp);
        forkBase.transform.localPosition = new Vector3(0, forkHeight, 0.15f);
    }

    void SetVisuals(WheelCollider collider)
    {
        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
        visualWheel.transform.Rotate(new Vector3(0, 0, 90));
    }
}