using UnityEngine;

public class EnemnyDeath : MonoBehaviour, IDieController
{
    [SerializeField] private ParticleSystem DieParticles;
    
    public void Die()
    {
        Instantiate(DieParticles, transform.position + new Vector3(0, 0.08f, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
