using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        UnlockNewLevel();

        if (SceneManagement.Instance != null)
            SceneManagement.Instance.NextLevel();
        else
        {
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            bool hasNextScene = nextScene < SceneManager.sceneCountInBuildSettings;
            if (hasNextScene)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
    }

    void UnlockNewLevel()
    {
        int unlocked = SceneManager.GetActiveScene().buildIndex + 1;
        if (unlocked > PlayerPrefs.GetInt("levelReached", 1))
        {
            PlayerPrefs.SetInt("levelReached", unlocked);
            PlayerPrefs.Save();
        }
    }
}