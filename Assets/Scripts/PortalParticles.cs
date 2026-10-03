using UnityEngine;

public class PortalParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem portalAParticles;
    [SerializeField] private ParticleSystem portalBParticles;
    void Start()
    {
        portalAParticles.Play();
        portalBParticles.Play();
    }
}
