using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public ParticleSystem impactParticles;

    private void Start()
    {
        Physics2D.IgnoreCollision(
            GetComponent<Collider2D>(),
            GameObject.FindWithTag("Player").GetComponent<Collider2D>()
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) return;

        if (impactParticles != null)
        {
            ParticleSystem impact = Instantiate(
                impactParticles,
                transform.position,
                Quaternion.Euler(-90f, 0f, 0f) // ← rotation -90 sur X
            );
            impact.Play();
            Destroy(impact.gameObject, impact.main.duration + 0.5f);
        }

        Destroy(gameObject);
    }
}