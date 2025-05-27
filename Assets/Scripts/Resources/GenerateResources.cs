using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateResources : MonoBehaviour
{
    [Header ("___ Settings : resources generation ___")]
    [SerializeField] private GameObject[] resources;
    [SerializeField] private bool zRandomizing = true;
    [SerializeField] private int generateNew = 10;
    [SerializeField] private TMP_InputField spawnSpeedInput;
    [Range (5f, 25f)]
    [SerializeField] private float range = 20f;

    private int _startResources = 20;
    
    private void Start() {
        Initializing();
        InvokeRepeating(nameof(Spawn), generateNew, generateNew);
    }

    private void Initializing() {
        StartSpawn(_startResources);

        spawnSpeedInput.onEndEdit.AddListener(OnSpeedChange);
    }

    private void StartSpawn(int count) {
        for(int i = 0; i < count; i++) {
            Spawn();
        }
    }

    private void Spawn() {
        int randomObject = UnityEngine.Random.Range(0, resources.Length - 1);

        float randX = UnityEngine.Random.Range(range * -1, range);
        float randY = UnityEngine.Random.Range(range * -1, range);
        float randZ = 0;
        if(zRandomizing) randZ = UnityEngine.Random.Range(range * -1, range);

        var FinalPos = new Vector3(randX, randY, randZ);
        var FinalRot = Quaternion.Euler(0, 0, 0);

        Instantiate(resources[randomObject], FinalPos, FinalRot);
    }

    private void OnSpeedChange(string value) {
        int newSpeed;

        if (int.TryParse(value, out newSpeed)) {
            newSpeed = Math.Clamp(newSpeed, 1, 30);
            generateNew = newSpeed;
            CancelInvoke(nameof(Spawn));
            InvokeRepeating(nameof(Spawn), generateNew, generateNew);
        }
    }
}
