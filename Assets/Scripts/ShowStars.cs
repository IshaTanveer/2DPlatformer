using UnityEngine.UI;
using UnityEngine;

public class ShowStars : MonoBehaviour
{
    Obstacledetection obstacleDetection;
    [SerializeField] GameObject Stars;
    [SerializeField] Sprite greySprite;
    [SerializeField] Sprite yellowSprite;
    void Start()
    {
        obstacleDetection = FindFirstObjectByType<Obstacledetection>();
        int maxCount = Stars.transform.childCount;
        int count = obstacleDetection.GetNumberOfLives();
        int yellowCount = maxCount - (count - 1);
        for(int i = 0; i < maxCount; i++)
        {
            Image star = Stars.transform.GetChild(i).GetComponent<Image>();
            if( i < yellowCount)
            {
                star.sprite = yellowSprite;
                Debug.Log("yellow sprite");
            }
            else
            {
                star.sprite = greySprite;
                Debug.Log("grey sprite");
            }
        }
    }
}
