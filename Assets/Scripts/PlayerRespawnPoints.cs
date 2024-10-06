using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnPoints : MonoBehaviour
{
    public Transform[] respawnPoints;
    public float fallThreshold = -10f;
    private int currentRespawnIndex = 0;

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    public void SetRespawnPoint(int index)
    {

        if (index > currentRespawnIndex && index < respawnPoints.Length)
        {
            currentRespawnIndex = index;     
        }
    }

    void Respawn()
    {
        transform.position = respawnPoints[currentRespawnIndex].position;
    }
}
