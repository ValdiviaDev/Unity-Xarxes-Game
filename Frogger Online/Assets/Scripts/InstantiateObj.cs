using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class InstantiateObj : MonoBehaviour
{
    public float spawnDelay = 1.0f;

    public GameObject obj_left_1, obj_left_2, obj_right_1, obj_right_2, obj_right_3;

    public Transform[] spawnPoints_left;
    public Transform[] spawnPoints_right;

    float nextTimeToSpawn = 0.0f;
    bool spawnRight = true;

    public bool isInLake = false;
    private int lake_index = 0;

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (nextTimeToSpawn <= Time.time)
            {
                // Spawn cars
                if (!isInLake)
                {
                    if (spawnRight)
                        SpawnObjRight();
                    else
                        SpawnObjLeft();

                    spawnRight = !spawnRight;
                }

                // Spawn objects of the lake
                else
                {
                    SpawnObjLake();
                }

                nextTimeToSpawn = Time.time + spawnDelay;
            }
        }
    }

    void SpawnObjLeft()
    {
        //Choses the lane spawner at random
        int randomIndex_l = Random.Range(0, spawnPoints_left.Length);
        Transform spawnPoint_l = spawnPoints_left[randomIndex_l];

        //Choses the object at random
        int randomobj = Random.Range(0, 2);

        //Spawns the object
        if (PhotonNetwork.IsMasterClient)
        {
            if (randomobj == 0)
                PhotonNetwork.Instantiate(obj_left_1.name, spawnPoint_l.position, spawnPoint_l.rotation);
            else
                PhotonNetwork.Instantiate(obj_left_2.name, spawnPoint_l.position, spawnPoint_l.rotation);
        }

    }

    void SpawnObjRight()
    {
        //Choses the lane spawner at random
        int randomIndex_r = Random.Range(0, spawnPoints_right.Length);
        Transform spawnPoint_r = spawnPoints_right[randomIndex_r];

        //Choses the object at random
        int randomobj = Random.Range(0, 3);

        //Spawns the object
        if (PhotonNetwork.IsMasterClient)
        {
            if (randomobj == 0)
                PhotonNetwork.Instantiate(obj_right_1.name, spawnPoint_r.position, spawnPoint_r.rotation);
            else if (randomobj == 1)
                PhotonNetwork.Instantiate(obj_right_2.name, spawnPoint_r.position, spawnPoint_r.rotation);
            else
                PhotonNetwork.Instantiate(obj_right_3.name, spawnPoint_r.position, spawnPoint_r.rotation);
        }
    }

    void SpawnObjLake()
    {
        //Decide which object to spawn
        GameObject to_spawn = null;

        switch (lake_index)
        {
            case 0:
                to_spawn = obj_left_1;
                break;
            case 1:
                to_spawn = obj_left_2;
                break;
            case 2:
                to_spawn = obj_right_1;
                break;
            case 3:
                to_spawn = obj_right_2;
                break;
            case 4:
                to_spawn = obj_right_3;
                break;
        }

        // Instantiate the pertinent GameObject
        if (to_spawn)
            PhotonNetwork.Instantiate(to_spawn.name, spawnPoints_left[lake_index].position, spawnPoints_left[lake_index].rotation);
        else
            Debug.Log("There's no object to spawn on the lake");

        //Update the lake index to decide which object to spawn in the next code iteration
        if (lake_index == 4)
            lake_index = 0;
        else
            lake_index++;
    }
}
