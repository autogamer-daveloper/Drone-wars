using UnityEngine;
using UnityEngine.UI;

public class TrailsSettings : MonoBehaviour
{
    [Header ("___ Settings : trails rendering ___")]
    [SerializeField] private Toggle toggle;

    private void Start() {
        bool isOn = toggle.isOn;
        toggle.onValueChanged.AddListener(GetToggleValue);
    }

    private void GetToggleValue(bool value) {
        if(value) {
            ChangeVisibleOfAllTrailsTo(true);
        } else {
            ChangeVisibleOfAllTrailsTo(false);
        }
    }

    private void ChangeVisibleOfAllTrailsTo(bool isVisible) {
        var trails = GameObject.FindObjectsByType<TrailRenderer>(0);

        foreach(TrailRenderer trail in trails) {
            if(isVisible) {
                trail.enabled = true;
            } else {
                trail.enabled = false;
            }
        } 
    }
}
