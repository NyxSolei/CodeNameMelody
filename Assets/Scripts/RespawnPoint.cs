using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public int respawnIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player entered the trigger!");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Respawn point set to: " + respawnIndex);

            PlayerRespawnPoints playerRespawn = other.GetComponent<PlayerRespawnPoints>();
            if (playerRespawn != null)
            {
                playerRespawn.SetRespawnPoint(respawnIndex);
                Debug.Log("Player Respawned at: " + respawnIndex);
            }
        }
    }
}
