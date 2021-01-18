using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObj : MonoBehaviour
{

    public float spawnDelay = 0.7f;

    public GameObject obj_left, obj_right;

    public Transform[] spawnPoints_left;
    public Transform[] spawnPoints_right;

    float nextTimeToSpawn = 0.0f;

    void Update()
    {
        if(nextTimeToSpawn <= Time.time)
        {
            SpawnObj();
            nextTimeToSpawn = Time.time + spawnDelay;
        }
    }

    void SpawnObj()
    {
        //TODO Valdivia: left and right spawns
        int randomIndex_l = Random.Range(0, spawnPoints_left.Length);
        Transform spawnPoint_l = spawnPoints_left[randomIndex_l];

        Instantiate(obj_left, spawnPoint_l.position, spawnPoint_l.rotation);


       // int randomIndex_r = Random.Range(0, spawnPoints_right.Length);
       // Transform spawnPoint_r = spawnPoints_left[randomIndex_r];
       //
       // Instantiate(obj_right, spawnPoint_r.position, spawnPoint_r.rotation);
    }
}
