using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovableScript : MonoBehaviour
{
    public float pushForce = 1;
    public float counteractingForceMultiplier = 3;
    public Vector3 pushDirection;
    public Vector3 actualDirection;
    private bool isPushed = false;

    private void OnCollisionEnter(Collision hitter){
        if(hitter.gameObject.CompareTag("Player")){
            isPushed = true;
            pushDirection = transform.position - hitter.transform.position;
            pushDirection.Normalize();
            if (Math.Abs(pushDirection.x) > Math.Abs(pushDirection.z)){
                actualDirection = new Vector3(pushDirection.x, 0, 0);
                if ((GetComponent<Rigidbody>().constraints & RigidbodyConstraints.FreezePositionX) != 0){
                    GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
                }
                GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionZ;
            }else{
                actualDirection = new Vector3(0,0,pushDirection.z);
                if ((GetComponent<Rigidbody>().constraints & RigidbodyConstraints.FreezePositionZ) != 0){
                    GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
                }
                GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionX;
            }

            GetComponent<Rigidbody>().AddForce(actualDirection * pushForce, ForceMode.Impulse);
        }
    }

    void Update(){
        if (!isPushed){
            if ((GetComponent<Rigidbody>().constraints & RigidbodyConstraints.FreezePositionX) != 0){
                GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
            }
            if ((GetComponent<Rigidbody>().constraints & RigidbodyConstraints.FreezePositionZ) != 0){
                GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
            }
            
            
        }
    }

    void FixedUpdate(){
        if (!isPushed){
            Vector3 counteractingForce = -GetComponent<Rigidbody>().velocity * counteractingForceMultiplier;
            GetComponent<Rigidbody>().AddForce(counteractingForce, ForceMode.Force);
            if ((GetComponent<Rigidbody>().constraints & RigidbodyConstraints.FreezePositionX) != 0){
                GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
            }
            if ((GetComponent<Rigidbody>().constraints & RigidbodyConstraints.FreezePositionZ) != 0){
                GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
            }
            
            
        }
    }
}
