using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance { get; private set; }
    [SerializeField] private Animator SceneAnim;
    [SerializeField] private float waitForRespawn = 1f;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void NextLevel()
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1;
            if(next < SceneManager.sceneCountInBuildSettings)
            {
                StartCoroutine(LoadScene(next));
            }
            else
            {
                StartCoroutine(LoadScene(0));
            }
    }
    IEnumerator LoadScene(int sceneIndex)
    {
        SceneAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(waitForRespawn);
        SceneManager.LoadScene(sceneIndex);
        SceneAnim.SetTrigger("FadeIn");
    }
}
