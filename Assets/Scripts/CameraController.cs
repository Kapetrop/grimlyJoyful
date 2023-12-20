using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera _cam;
     public Transform pivotPoint;
    public float rotationSpeed = 2.0f;
    public float zoomSpeed = 20.0f;
    public float minFOV = 5f;
    public float maxFOV = 60f;

    void Update()
    {
        // Rotation avec le clic droit
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            pivotPoint.Rotate(Vector3.up * mouseX * rotationSpeed, Space.World);
            pivotPoint.Rotate(Vector3.left * mouseY * rotationSpeed, Space.Self);
        }

        // Zoom avec la roulette de la souris
        float scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");

        // Utiliser également la position de la souris pour ajuster le zoom
        float zoomFactor = Mathf.Pow(1.1f, -scrollWheel * zoomSpeed);
        _cam.fieldOfView *= zoomFactor;
        _cam.fieldOfView = Mathf.Clamp(_cam.fieldOfView, minFOV, maxFOV);
    }
}
