using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Obstacledetection : MonoBehaviour
{
    Vector2 checkpoint; 
    [SerializeField] private float waitForRespawn = 1f;
    [SerializeField] private Animator RespawnAnim;
    SpriteRenderer spriteRenderer;
    [SerializeField] private AudioManager audioManager;
    Rigidbody2D rb;
    [SerializeField] private ParticleSystem RespawnParticles;
    private int count;
    private bool isDead;
    private Countdown countdown;
    public Image[] livesImages;
    [SerializeField] private GameObject LifeImage;
    void Start()
    {
        count = 1;
        isDead = false;
        countdown = FindFirstObjectByType<Countdown>();
        checkpoint = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindFirstObjectByType<AudioManager>();
        ImagesToArray();
    }

    private void ImagesToArray()
    {
        int imageCount = LifeImage.transform.childCount;
        livesImages = new Image[imageCount];
        for(int i = 0; i < imageCount; i++)
        {
            livesImages[i] = LifeImage.transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void UpdateCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if(isDead) return;
            if(count == 3)
            {
                countdown.GameOver();
            }
            isDead = true;
            int num = livesImages.Length - count;
            livesImages[num].gameObject.SetActive(false);
            count++;
            Debug.Log("Obstacle hit! Count: " + count);
            audioManager.PlaySounds(AudioManager.Sound.death);
            Die();
        }
    }
    public int GetNumberOfLives()
    {
        return count;
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
        isDead = false;
    }
}
