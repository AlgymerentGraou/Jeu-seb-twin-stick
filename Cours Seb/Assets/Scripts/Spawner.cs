using System.Numerics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip ("if true, select one the spawn points randomly each time, else all are used")]
    [SerializeField]private bool randomize;
    [SerializeField]private GameObject enemyPrefab;
    [Tooltip("Time between each waves in seconds")]
    [SerializeField]private float spawnInterval = 3.5f;      
    
    [Header("Boss Settings")]
    [SerializeField]private GameObject bossPrefab;
    [Tooltip("Number of waves beetween 2 boss")]
    [SerializeField]private int bossWave = 0;

    [Space(20)]
    [SerializeField]private Transform[] spawnPoints;  

    private Transform target;
    private int index;
    private int loopCounter = 0;  

    
    void Start()
    {
        StartCoroutine(SpawnEnemy(spawnInterval, enemyPrefab)); //Lance la Coroutine SpawnEnemy
    }

    

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval); //Timer

        if(randomize) //Si Random...
        {
            RandomTarget();       //... Appel la Coroutine RandomTarget
            GameObject newEnemy = Instantiate(enemy, target); // Spawn l'ennemi sur la target désignée
            loopCounter++;     //incrémente la valeur loopCounter de 1   
        }
        else
        {
            foreach (Transform spawnPoint in spawnPoints) //Si pas Random, pour chaque point de spawn...
            {
                GameObject newEnemy = Instantiate(enemy, spawnPoint.transform); // spawn un ennemi
                loopCounter++; //incrémente la valeur loopCounter de 1
            }
       
        }

        StartCoroutine(SpawnEnemy(spawnInterval, enemyPrefab)); //relance la coroutine pour faire une boucle 

        if(bossPrefab != null && bossWave != 0 && loopCounter % bossWave == 0) // Si un prefab est dans la propriété bossPrefab, que la fonction bossWave est différente de 0 et que le nombre le wave est égale à bossWave  
        {
            SpawnBoss(); //appel la coroutine SpawnBoss
        }      
        
        
    }

    void RandomTarget()
    {
        index = (Random.Range (0, spawnPoints.Length)); //selectionne aléatoirement une valeur dans le tableau spawnPoint
        target = spawnPoints[index]; //défini cette valeur comme étant "target" 
    }

    void SpawnBoss()
    {
        RandomTarget(); // Appel la Coroutine RandomTarget
        GameObject newEnemy = Instantiate(bossPrefab, target); //Fait apparaitre le prefab boss au point selectionné 
        loopCounter = 0; // remet la valeur loopCounter à 0.
    }
}
