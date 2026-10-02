using System.Collections;
using UnityEngine;

public class PlayerPortal : MonoBehaviour
{
    [SerializeField] private Transform destination;
    GameObject player;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private float wait;
    AudioManager audioManager;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            if(Vector2.Distance(player.transform.position, transform.position) > 0.8f)
            {
                StartCoroutine(PlayerPort());
            }
        }
    }
    IEnumerator PlayerPort()
    {
        audioManager.PlaySounds(AudioManager.Sound.PortalIn);
        rb.simulated = false;
        animator.SetBool("Out", true);
        yield return new WaitForSeconds(wait);
        player.transform.position = destination.position;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("Out", false);
        rb.simulated = true;
        audioManager.PlaySounds(AudioManager.Sound.PortalOut);
    }
}
