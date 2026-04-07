using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public float shakeForce = 0.1f;
    public float finalShakeForce = 0.2f;
    public int health = 3;

    private ParticleSystem _debris;

    void Start()
    {
        _debris = GetComponentInChildren<ParticleSystem>();

        if (_debris == null)
            Debug.LogError("Aucun Particle System 'Debris' trouvé !");
    }

    public void Break()
    {
        health--;

        if (health <= 0)
        {
            if (_debris != null)
            {
                _debris.transform.SetParent(null);
                _debris.Emit(10); // ← burst de particules à la destruction
            }

            ScreenShake.Instance.Shake(finalShakeForce);
            Destroy(gameObject);
        }
        else
        {
            ScreenShake.Instance.Shake(shakeForce); // ← léger à chaque hit
        }
    }
}