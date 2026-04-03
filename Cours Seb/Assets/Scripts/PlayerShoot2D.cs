using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot2D : MonoBehaviour
{    
    [Tooltip("ShootingPoint Transform must be outside of the Avatar Collider")] 
    public Transform shootingPoint;
    public float ammoCount;

    public KeyCode ShootButton;
    
    [Header("Prefab Settings")]
    [Tooltip("Specify HitTag & Dammages in Bullet Script.")]
    public GameObject ammoPrefab;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
        
        if (Input.GetKeyDown(ShootButton) && ammoCount>0)
        {    
            //Spawn Prefab
            GameObject bullet = Instantiate(ammoPrefab, shootingPoint.position, transform.rotation);
            bullet.transform.rotation = shootingPoint.rotation;

            //Add Prefab Velocity
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);

            ammoCount--;
            print("it remains " + ammoCount + " bullets"); 
        }   
    }
}
