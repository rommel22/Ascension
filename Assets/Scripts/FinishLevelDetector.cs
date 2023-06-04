using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelDetector : MonoBehaviour
{
    Collider thisCollider;
    void Start()
    {
        thisCollider = GetComponent<Collider>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            FindObjectOfType<FinishLevelManager>().StartCoroutine("OnLevelCompleted");
        }
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject.tag == "Player"){
            thisCollider.isTrigger = false;
        }
    }
}
