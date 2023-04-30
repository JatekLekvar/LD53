using UnityEngine;

public class Forklift : MonoBehaviour
{
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float maxBrakeTorque;
    public float forkSpeed;
    public float forkMin;
    public float forkMax;
    public float forkDamp;
    public float maxAngle;
    public float angleDamp;

    WheelCollider frontLeft;
    WheelCollider frontRight;
    WheelCollider rearLeft;
    WheelCollider rearRight;

    Transform fork;
    Transform forkBase;
    Transform drivingWheel;

    float targetForkHeight;
    float forkHeight;
    float targetAngle;
    float angle;

    public void Start()
    {
        Transform wheelBase = transform.GetChild(0);
        frontLeft = wheelBase.GetChild(0).GetComponent<WheelCollider>();
        frontRight = wheelBase.GetChild(1).GetComponent<WheelCollider>();
        rearLeft = wheelBase.GetChild(2).GetComponent<WheelCollider>();
        rearRight = wheelBase.GetChild(3).GetComponent<WheelCollider>();

        fork = transform.GetChild(1);
        forkBase = fork.GetChild(1);
        drivingWheel = transform.GetChild(3).GetChild(0);

        targetForkHeight = forkMin;
        forkHeight = targetForkHeight;

        targetAngle = 0;
        angle = targetAngle;

        GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0.3f, -0.3f);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (targetAngle > 0.01f)
            {
                targetAngle = 0;
            }
            else
            {
                targetAngle = maxAngle;
            }
        }
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
        drivingWheel.localEulerAngles = new Vector3(0, steering * 3f, 0);

        rearLeft.motorTorque = motor;
        rearRight.motorTorque = motor;

        frontLeft.brakeTorque = braking;
        frontRight.brakeTorque = braking;
        rearLeft.brakeTorque = braking;
        rearRight.brakeTorque = braking;

        SetVisuals(frontLeft, true);
        SetVisuals(frontRight, false);
        SetVisuals(rearLeft, true);
        SetVisuals(rearRight, false);

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
        forkBase.transform.localPosition = new Vector3(0, forkHeight, 0);

        angle = Mathf.Lerp(angle, targetAngle, angleDamp);
        forkBase.localEulerAngles = new Vector3(-angle, 0, 0);
    }

    void SetVisuals(WheelCollider collider, bool flip)
    {
        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
        visualWheel.transform.Rotate(new Vector3(flip ? 180 : 0, 90, 0));
    }
}