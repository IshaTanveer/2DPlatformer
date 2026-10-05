using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWin : MonoBehaviour
{
    [SerializeField] GameObject LevelWinPanel;
    
    [SerializeField] Button pauseButton;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        else
        {
            Time.timeScale = 0f;
            pauseButton.interactable = false;
            LevelWinPanel.gameObject.SetActive(true);
        }
        
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
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