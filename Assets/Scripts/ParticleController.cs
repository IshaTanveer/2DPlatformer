using System;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using RangeAttribute = UnityEngine.RangeAttribute;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem MovementParticles;
    [SerializeField] private ParticleSystem FallParticles;
    [SerializeField] private ParticleSystem TouchParticles;
    [SerializeField] private ParticleSystem portalAParticles;
    [SerializeField] private ParticleSystem portalBParticles;
    [Range(0,10)]
    [SerializeField] int occurAfterVelocity;
    [Range(0.0f,0.2f)]
    [SerializeField] float dustFormationPeriod;
    [SerializeField] private Rigidbody2D playerRB;
    float counter;
    private bool isOnGround;
    private void Start() {
        portalAParticles.Play();
        portalBParticles.Play();
    }

    public void PlayTouchParticles()
    {
        TouchParticles.Play();
    }
    void Update()
    {
        counter += Time.deltaTime;
        if(isOnGround && Math.Abs(playerRB.linearVelocity.x) > occurAfterVelocity)
        {
            if(counter > dustFormationPeriod)
            {
                MovementParticles.Play();
                counter = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Wall"))
        {
            isOnGround = true;
            FallParticles.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Wall"))
        {
            isOnGround = false;
        }
    }
}
