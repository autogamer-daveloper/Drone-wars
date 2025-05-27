using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sklad : MonoBehaviour
{
    [System.Serializable]
    internal enum TeamType {
        Blue,
        Red
    }

    [Header ("___ Settings : sklad one of the fraction ___")]
    [SerializeField] private TMP_Text resourcesCountText;
    [SerializeField] private GameObject DroneTeamExample;
    [SerializeField] private TeamType type;
    [SerializeField] private Transform[] skladPoints = new Transform[5];

    private int workingDrones = 0;
    private int resourcesCount = 0;

#region Initialize team

    private void Awake() {
        switch(type) {
            case TeamType.Blue:
                workingDrones = PlayerPrefs.GetInt("BlueDrones", 5);
            break;
            case TeamType.Red:
                workingDrones = PlayerPrefs.GetInt("RedDrones", 5);
            break;
            default:
                workingDrones = PlayerPrefs.GetInt("BlueDrones", 5);
            break;
        }
    }

    private void Start() {
        for(int i = 0; i < workingDrones; i++) {
            Spawn(i);
        }
    }

#endregion

#region Get resource

    internal void GetResource(int count) {
        resourcesCount += count;
        if(resourcesCountText != null) resourcesCountText.text = resourcesCount.ToString();
    }

#endregion

#region Initialize count of drones for resource generator

    internal int GetDrones() {
        return workingDrones;
    }

#endregion

#region Spawn drones

    private void Spawn(int index) {
        var StartRot = Quaternion.Euler(0, 0, 0);

        var obj = Instantiate(DroneTeamExample, skladPoints[index].position, StartRot);
        var droneSettings = obj.GetComponent<DroneMain>();
        if(droneSettings != null) {
            droneSettings.SetId(index, this);
        }
    }

#endregion

#region Get sklad position by id

    internal Transform GetPointById(int id) {
        return skladPoints[id];
    }

#endregion
}
