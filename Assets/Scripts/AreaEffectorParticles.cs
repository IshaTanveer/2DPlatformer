using UnityEngine;

public class AreaEffectorParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem AreaEffectorParticles1;
    [SerializeField] private ParticleSystem AreaEffectorParticles2;
    void Start()
    {
        AreaEffectorParticles1.Play();
        AreaEffectorParticles2.Play();
    }
}
