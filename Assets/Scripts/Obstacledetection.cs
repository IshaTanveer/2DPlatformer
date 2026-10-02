using System;
using System.Collections;
using UnityEngine;

public class Obstacledetection : MonoBehaviour
{
    Vector2 checkpoint; 
    [SerializeField] private float waitForRespawn = 1f;
    [SerializeField] private Animator RespawnAnim;
    SpriteRenderer spriteRenderer;
    [SerializeField] private AudioManager audioManager;
    Rigidbody2D rb;
    [SerializeField] private ParticleSystem RespawnParticles;
    void Start()
    {
        checkpoint = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    public void UpdateCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            audioManager.PlaySounds(AudioManager.Sound.death);
            Die();
        }
    }

    private void Die()
    {
        RespawnAnim.SetTrigger("Respawn");
        RespawnParticles.Play();
        RespawnAnim.SetTrigger("NO");
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        rb.simulated = false;
        transform.localScale = new Vector3(0,0,0);
        yield return new WaitForSeconds(waitForRespawn); 
        transform.position = checkpoint;
        transform.localScale = new Vector3(1,1,1);
        rb.linearVelocity = Vector2.zero;
        rb.simulated = true;
    }
}
