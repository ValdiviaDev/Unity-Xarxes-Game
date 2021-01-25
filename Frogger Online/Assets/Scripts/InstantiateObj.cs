using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObj : MonoBehaviour
{
    public float spawnDelay = 1.0f;

    public GameObject obj_left, obj_right;

    public Transform[] spawnPoints_left;
    public Transform[] spawnPoints_right;

    float nextTimeToSpawn = 0.0f;
    bool spawnRight = true;


    void Update()
    {
        //Decides when to spawn an object comming from the left
        if (nextTimeToSpawn <= Time.time)
        {
            if (spawnRight)
                SpawnObjRight();
            else
                SpawnObjLeft();

            spawnRight = !spawnRight;
            nextTimeToSpawn = Time.time + spawnDelay;
        }
    }

    void SpawnObjLeft()
    {
        int randomIndex_l = Random.Range(0, spawnPoints_left.Length);
        Transform spawnPoint_l = spawnPoints_left[randomIndex_l];

        Instantiate(obj_left, spawnPoint_l.position, spawnPoint_l.rotation);

    }

    void SpawnObjRight()
    {
        int randomIndex_r = Random.Range(0, spawnPoints_right.Length);
        Transform spawnPoint_r = spawnPoints_right[randomIndex_r];

        Instantiate(obj_right, spawnPoint_r.position, spawnPoint_r.rotation);
    }

}
