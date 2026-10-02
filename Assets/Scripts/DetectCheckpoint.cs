using UnityEngine;

public class DetectCheckpoint : MonoBehaviour
{
    Obstacledetection obstacledetection;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform respawnPoint;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite passiev, active;
    BoxCollider2D boxCollider2D;
    AudioManager audioManager;
    void Awake()
    {
        obstacledetection = FindFirstObjectByType<Obstacledetection>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySounds(AudioManager.Sound.CheckPoint);
            spriteRenderer.sprite = active;
            obstacledetection.UpdateCheckpoint(respawnPoint.position);
            boxCollider2D.enabled = false;
        }
    }
}
