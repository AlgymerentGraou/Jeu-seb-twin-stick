using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    [SerializeField] private string hitTag;
    [SerializeField] private int reboundCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(hitTag))
        {
            other.GetComponent<ZombieHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            if(reboundCount <= 0)
            {                
                Destroy(gameObject);
            }
            else
            {
                reboundCount--;
                print(reboundCount);
            }
        }
    }
}
