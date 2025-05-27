using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [Header ("___ Settings : camera looking ___")]
    [SerializeField] private Camera cam;

    private void Update() {
        transform.LookAt(cam.transform);
    }
}
