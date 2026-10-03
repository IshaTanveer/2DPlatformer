using UnityEngine;
using TMPro;
using System;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float countdownTime = 3f;
    [SerializeField] private GameObject gameOverPanel;
    void Awake()
    {
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

    private void GameOver()
    {
        Time.timeScale = 0f;
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
