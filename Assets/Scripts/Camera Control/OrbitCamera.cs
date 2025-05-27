using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Header ("___ Settings : orbital camera ___")]
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private Vector3 target = Vector3.zero;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private bool leftMouseButton = true;
    [Range (0f, 89f)]
    [SerializeField] private float yBorder = 75;

    private float _yaw;
    private float _pitch;

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        _yaw = angles.y;
        _pitch = angles.x;

        UpdateCameraPosition();
    }

    private void LateUpdate()
    {
        if(leftMouseButton) {
            if(Input.GetMouseButton(0))
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                _yaw += mouseX * rotationSpeed;
                _pitch -= mouseY * rotationSpeed;
                _pitch = Mathf.Clamp(_pitch, yBorder * -1, yBorder);

                UpdateCameraPosition();
            }
        } else {
            if(Input.GetMouseButton(1))
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                _yaw += mouseX * rotationSpeed;
                _pitch -= mouseY * rotationSpeed;
                _pitch = Mathf.Clamp(_pitch, yBorder * -1, yBorder);

                UpdateCameraPosition();
            }
        }
    }

    private void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
        Vector3 direction = rotation * Vector3.back * distance;

        transform.position = target + direction;
        transform.LookAt(target);
    }
}
