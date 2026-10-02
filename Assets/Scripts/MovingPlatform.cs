using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject Ways;
    [SerializeField] private float WaitDuration;

    public Transform[] WayPoints;

    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    private Vector3 targetPosition;
    private Vector3 moveDirection;

    private Rigidbody2D rb;

    private PlayerMovement playerMovement;
    private Rigidbody2D playerRB;

    private bool isWaiting = false;

    void Awake()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();

        rb = GetComponent<Rigidbody2D>();
        playerRB = playerMovement.GetComponent<Rigidbody2D>();

        WayPoints = new Transform[Ways.transform.childCount];

        for (int i = 0; i < Ways.transform.childCount; i++)
        {
            WayPoints[i] = Ways.transform.GetChild(i);
        }
    }

    void Start()
    {
        pointCount = WayPoints.Length;

        pointIndex = 1;

        targetPosition = WayPoints[pointIndex].position;

        CalculateDirection();
    }

    void FixedUpdate()
    {
        if (isWaiting)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (Vector2.Distance(rb.position, targetPosition) < 0.05f)
        {
            NextPoint();
            return;
        }

        rb.MovePosition(rb.position + (Vector2)moveDirection * speed * Time.fixedDeltaTime);
    }

    void NextPoint()
    {
        // Stop at waypoint
        rb.MovePosition(targetPosition);
        rb.linearVelocity = Vector2.zero;

        moveDirection = Vector3.zero;

        // Reverse direction at the ends
        if (pointIndex == pointCount - 1)
        {
            direction = -1;
        }
        else if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;

        targetPosition = WayPoints[pointIndex].position;

        StartCoroutine(WaitForNextPoint());
    }

    IEnumerator WaitForNextPoint()
    {
        isWaiting = true;

        yield return new WaitForSeconds(WaitDuration);

        CalculateDirection();

        isWaiting = false;
    }

    void CalculateDirection()
    {
        moveDirection =
            (targetPosition - transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.setIsOnPlatform(true);
            playerMovement.platformRB = rb;

            playerRB.gravityScale *= 20.0f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMovement.setIsOnPlatform(false);

            playerRB.gravityScale /= 20.0f;
        }
    }
}