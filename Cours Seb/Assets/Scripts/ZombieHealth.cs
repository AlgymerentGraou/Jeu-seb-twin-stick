using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public GameObject FX;
    public Transform zombiepos;
    public int healthPoint;
    public int critChancePercent;
    public GameObject DropItem;
    private Boolean itemDroped = false;

    public void Start()
    {
         /*zombiepos = this.GetComponent<GameObject>().transform;*/
    }
    public void TakeDamage(int damage)
    {
        int critRand = UnityEngine.Random.Range(0, 100);
        if(critRand<= critChancePercent)
        {
            healthPoint -= damage * 2;
            if(FX != null)
            {
                Instantiate(FX, zombiepos);
            }            
            print("critHit");
        }
        else
        {
            healthPoint -= damage;
        }

        if (healthPoint <= 0)
        {
            if(DropItem != null)
            {
                Instantiate(DropItem, new Vector3(zombiepos.position.x,zombiepos.position.y + 1f,zombiepos.position.z), transform.rotation);
            }            
            itemDroped = true;
            if(itemDroped == true)
            {
                Destroy(gameObject);
            }
        }
        print(healthPoint);
    }
}
