using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject respawnPointAfterCheckpoint;
    bool hasReachedCheckpoint;
    void Start()
    {
        hasReachedCheckpoint = false;
    }

    public void OnCheckpointReached()
    {
        if (hasReachedCheckpoint) return;

        hasReachedCheckpoint = true;
        FindObjectOfType<HealthManager>().setRespawnPoint(respawnPointAfterCheckpoint);
    }
}
