using UnityEngine;

public class OrbitCam : MonoBehaviour
{
    public Rigidbody target;

    public float lookSensitivity;
    public float lookDamp;

    public float minRotationX;
    public float maxRotationX;

    public float minZoom;
    public float maxZoom;
    public float zoomMultiplier;
    public float zoomDamp;

    Transform camContainer;
    Transform cam;

    float rotationX;
    float rotationY;

    float finalRotationX;
    float finalRotationY;

    float zoom;
    float finalZoom;

    bool isObstructed;
    Vector3 obstructionPoint;

    float elapsed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        camContainer = transform.GetChild(0);
        cam = camContainer.GetChild(0);
        zoom = (minZoom + maxZoom) / 2f;
        finalZoom = zoom;
    }

    void Update()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scroll > 0.1)
        {
            zoom /= zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        }

        if (scroll < -0.1)
        {
            zoom *= zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        }

        Vector3 origin = transform.position;
        Vector3 camOrigin = cam.position;
        Vector3 rayDir = (camOrigin - origin).normalized;

        RaycastHit[] hits = Physics.RaycastAll(origin, rayDir, finalZoom, ~LayerMask.GetMask("Forklift"));

        isObstructed = false;
        if (hits.Length > 0)
        {
            RaycastHit closestHit = hits[0];
            float minDsqr = (closestHit.point - origin).sqrMagnitude;

            for (int i = 1; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                float dsqr = (hit.point - origin).sqrMagnitude;

                if (dsqr < minDsqr)
                {
                    closestHit = hit;
                    minDsqr = dsqr;
                }
            }

            isObstructed = true;
            obstructionPoint = closestHit.point;
        }

        if (isObstructed)
        {
            float distance = (obstructionPoint - camContainer.position).magnitude;
            cam.localPosition = new Vector3(0, 0, -distance);
        }
    }

    void FixedUpdate()
    {
        elapsed += Time.fixedDeltaTime;

        Vector3 desiredPosition = target.worldCenterOfMass;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f);

        if (elapsed > 0.5f)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSensitivity;
            rotationY += Input.GetAxis("Mouse X") * lookSensitivity;
        }

        finalRotationX = Mathf.Clamp(finalRotationX + rotationX, minRotationX, maxRotationX);
        finalRotationY += rotationY;

        transform.localEulerAngles = new Vector3(0, finalRotationY, 0);
        camContainer.localEulerAngles = new Vector3(finalRotationX, 0, 0);

        rotationX = Mathf.Lerp(rotationX, 0.0f, lookDamp);
        rotationY = Mathf.Lerp(rotationY, 0.0f, lookDamp);

        finalZoom = Mathf.Lerp(finalZoom, zoom, zoomDamp);

        if (!isObstructed)
        {
            cam.localPosition = new Vector3(0, 0, -finalZoom);
        }
    }
}
