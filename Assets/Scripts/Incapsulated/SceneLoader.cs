using UnityEngine.SceneManagement;

public class SceneLoader
{
    internal void LoadSceneByIndex(int index) {
        SceneManager.LoadScene(index);
    }
}
