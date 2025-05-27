using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DroneSpeedSettings : MonoBehaviour
{
    [Header ("___ Settings : drones speed limit ___")]
    [SerializeField] private float speed = 1;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private Slider speedLimit;

    private void Start() {
        speedLimit.onValueChanged.AddListener(ChangedValue);
    }

    private void ChangedValue(float newSpeed) {
        speed = Mathf.Round(newSpeed);

        speedText.text = speed.ToString();

        var drones = GameObject.FindObjectsByType<DroneMain>(0);

        foreach(DroneMain drone in drones) {
            drone.SetSpeed(speed);
        }
    }
}
