using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSettings : MonoBehaviour
{
    [Header ("___ Settings : menu ___")]
    [SerializeField] private int simulationId = 1;
    [SerializeField] private Button startButton;
    [SerializeField] private Slider blueDrons;
    [SerializeField] private Slider redDrons;
    [SerializeField] private TMP_Text blueText;
    [SerializeField] private TMP_Text redText;

    private int _blueCount = 1;
    private int _redCount = 1;

    private void Start() {
        if(startButton != null) startButton.onClick.AddListener(LoadSimulation);
        if(blueDrons != null) blueDrons.onValueChanged.AddListener(GetBlueDronsFromInput);
        if(redDrons != null) redDrons.onValueChanged.AddListener(GetRedDronsFromInput);
    }

#region Load simulation

    private void LoadSimulation() {
        SaveDrones();

        var loader = new SceneLoader();
        loader.LoadSceneByIndex(simulationId);
    }

#endregion

#region Get values

    private void GetBlueDronsFromInput(float result) {
        _blueCount = Mathf.RoundToInt(result);
        blueText.text = _blueCount.ToString();
    }

    private void GetRedDronsFromInput(float result) {
        _redCount = Mathf.RoundToInt(result);
        redText.text = _redCount.ToString();
    }

#endregion

#region Saving all

    private void SaveDrones() {
        //Save blue
        _blueCount = Math.Clamp(_blueCount, 1, 5);
        PlayerPrefs.SetInt("BlueDrones", _blueCount);

        //Save red
        _redCount = Math.Clamp(_redCount, 1, 5);
        PlayerPrefs.SetInt("RedDrones", _redCount);
    }

#endregion
}
