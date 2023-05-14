using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerMovement thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        thePlayer = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt(int damage, Vector3 direction){
        currentHealth -= damage;
        thePlayer.Knockback(direction);
    }
}
