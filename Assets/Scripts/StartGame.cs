using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGame : MonoBehaviour
{
    SceneManagement sceneManagement;
    private void Awake() {
        sceneManagement = FindFirstObjectByType<SceneManagement>();
    }
    public void StartGameButton()
    {
        SceneManager.LoadSceneAsync("Level1");
    }
    public void QuitGameButton()
    {
        Debug.Log("Quit");
        Application.Quit();
        Debug.Log("Quiting");
    }
    public void OpenOptionsMenu()
    {
        // Code to open the options menu
    }
}
