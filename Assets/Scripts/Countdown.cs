using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float countdownTime = 3f;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float wait = 1.0f;
    
    [SerializeField] private TextMeshProUGUI levelNumber;
    void Awake()
    {
        levelNumber.text = "level " + SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
    }
    void Update()
    {
        if(countdownTime > 0)
        {
            UpdateCountdown();
        }
        else if(countdownTime <= 0)
        {
            countdownText.text = "00:00";
            countdownText.color = Color.red;
            GameOver();
        }
        
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        StartCoroutine(ShowGameOverPanel());
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSecondsRealtime(wait);
        gameOverPanel.SetActive(true);
    }

    private void UpdateCountdown()
    {
        countdownTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(countdownTime / 60f);
        int seconds = Mathf.FloorToInt(countdownTime % 60f);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
