using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableScript : MonoBehaviour
{
    public float pushForce = 1;
    private bool isPushed = false;

    private void OnCollisionEnter(Collision hitter){
        if(hitter.gameObject.CompareTag("Player")){
            Vector3 pushDirection = transform.position - hitter.transform.position;
            pushDirection.Normalize();

            GetComponent<Rigidbody>().AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
