using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRespawnPoints : MonoBehaviour
{
    public GameObject[] trapPrefabs; //Diffrent trap prefabs - 3 different kinds
    public Transform[] trapRespawnPoints; // the trap's respawn points
    public float respawnTime = 2f; 

    private void Start()
    {
        InvokeRepeating("RespawnTrap", 0f, respawnTime);
    }

    void RespawnTrap()
    {
        foreach (Transform respawnPoint in trapRespawnPoints)
        {
            int randomIndex = Random.Range(0, trapPrefabs.Length);

            if (respawnPoint.childCount > 0)
            {
                Destroy(respawnPoint.GetChild(0).gameObject);
            }

            Instantiate(trapPrefabs[randomIndex], respawnPoint.position, Quaternion.identity, respawnPoint);
        }
    }
}
