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

        if (other.CompareTag("Breakable"))
        {
            BreakableWall wall = other.GetComponent<BreakableWall>();
            if (wall != null)
                wall.Break();
            else
                Destroy(other.gameObject);
        }

        // allume le flambeau si le projectile le touche
        if (other.CompareTag("Flambeau"))
        {
            LightToggle flambeau = other.GetComponentInParent<LightToggle>();
            if (flambeau != null)
                flambeau.Allumer();
            return; // ← la balle ne se détruit pas en touchant le flambeau
        }

        if (impactParticles != null)
        {
            ParticleSystem impact = Instantiate(
                impactParticles,
                transform.position,
                Quaternion.Euler(-90f, 0f, 0f)
            );
            impact.Play();
            Destroy(impact.gameObject, impact.main.duration + 0.5f);
        }

        Destroy(gameObject);
    }
}