using System;
using System.Collections;
using System.Drawing;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject Ways;
    public Transform[] WayPoints;
    int pointIndex, pointCount; 
    private bool direction = true;
    Vector3 targetPosition;
    [SerializeField] private float WaitDuration;
    private  float speedMultiplier = 1.0f;
    void Awake()
    {
        WayPoints = new Transform[Ways.transform.childCount];
        for(int i = 0; i < Ways.transform.childCount; i++)
        {
            WayPoints[i] = Ways.transform.GetChild(i).gameObject.transform;
        }
    }
    void Start()
    {
        pointCount = WayPoints.Length;
        pointIndex = 1;
        targetPosition = WayPoints[pointIndex].transform.position;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMultiplier* speed * Time.fixedDeltaTime);
        if(transform.position == targetPosition)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        if (direction)
        {
            pointIndex++;
            if(pointIndex == pointCount)
            {
                direction = false;
            }
        }
        if (!direction)
        {
            pointIndex--;
            if(pointIndex == 0)
            {
                direction = true;
            }
        }
        targetPosition = WayPoints[pointIndex].transform.position;
        StartCoroutine(WaitBeforeMoving());
    }

    IEnumerator WaitBeforeMoving()
    {
        speedMultiplier = 0.0f;
        yield return new WaitForSeconds(WaitDuration);
        speedMultiplier = 1.0f;
    }
}
