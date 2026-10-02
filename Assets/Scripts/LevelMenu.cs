using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] levelButtons;
    
    [SerializeField] private GameObject levelButton;
    private void Awake() {
        ButtonToArray();
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for(int i = 0; i<levelButtons.Length; i++)
        {
            if(i >= levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }
    public void PlayLevel(int levelId)
    {
        string lvlName= "level" + levelId;
        SceneManager.LoadSceneAsync(lvlName);
    }
    void ButtonToArray()
    {
        int buttonCount = levelButton.transform.childCount;
        levelButtons = new Button[buttonCount];
        for(int i = 0; i < buttonCount; i++)
        {
            levelButtons[i] = levelButton.transform.GetChild(i).GetComponent<Button>();
        }
    }
}
