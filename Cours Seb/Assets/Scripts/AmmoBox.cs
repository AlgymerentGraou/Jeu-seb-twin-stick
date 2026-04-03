using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int nbAmmoGiving;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerShoot2D>().ammoCount += nbAmmoGiving;
            Destroy(gameObject);
        }
    }
}
