using Unity.VisualScripting;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    int maxPages, currentPage;
    Vector3 targetPosition;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] Vector3 pageStep;
    [SerializeField] LeanTweenType tweenType;
    [SerializeField] float tweenTime;
    void Awake()
    {
        maxPages = 3;
        currentPage = 1;
        targetPosition = levelPagesRect.localPosition;
    }
    public void Next()
    {
        if(currentPage < maxPages)
        {
            currentPage++;
            targetPosition += pageStep;
            MovePage();
        }
    }
    public void Previous()
    {
        if(currentPage > 1)
        {
            currentPage--;
            targetPosition -= pageStep;
            MovePage();
        }
    }
    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPosition, tweenTime).setEase(tweenType);
    }
}
