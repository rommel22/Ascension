using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            FindObjectOfType<HealthManager>().Hurt(damage, hitDirection);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 hitDirection = collision.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            FindObjectOfType<HealthManager>().Hurt(damage, hitDirection);
        }
    }
}
