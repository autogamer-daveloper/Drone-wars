using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class StopSimulation : MonoBehaviour
{
    private Button button;

    private void Start() {
        button = GetComponent<Button>();

        button.onClick.AddListener(ExitSimulation);
    }

    private void ExitSimulation() {
        var loader = new SceneLoader();
        loader.LoadSceneByIndex(0);
    }
}
