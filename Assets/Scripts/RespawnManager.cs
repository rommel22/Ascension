using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject respawnPointAfterCheckpoint;
    public RectTransform checkpointNotification;
    public RectTransform checkpointDisplayPosition;
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
        FindObjectOfType<HealthManager>().currentHealth = FindObjectOfType<HealthManager>().maxHealth;

        StartCoroutine(MoveCheckpointNotification());
    }

    public IEnumerator MoveCheckpointNotification()
    {
        Vector3 originalPosition = checkpointNotification.transform.position;
        float step = 0.5f * Time.fixedDeltaTime;

        for (float t = 0; t <= 1.0f; t += step) {
            checkpointNotification.transform.position = Vector3.Lerp(checkpointNotification.transform.position, checkpointDisplayPosition.transform.position, t);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(3);

        for (float t = 0; t <= 1.0f; t += step) {
            checkpointNotification.transform.position = Vector3.Lerp(checkpointNotification.transform.position, originalPosition, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
