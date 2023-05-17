using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerMovement thePlayer;
    
    public float invicibleTime;
    private float invicibleCounter;

    public Text healthText;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "Lives:" +  currentHealth + "/" + maxHealth;
        thePlayer = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invicibleCounter > 0){
            invicibleCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0){
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashTime;
            }
            if (invicibleCounter <= 0){
                playerRenderer.enabled = true;
            }
        }
    }

    public void Hurt(int damage, Vector3 direction){
        if (invicibleCounter <= 0){
            currentHealth -= damage;
            healthText.text = "Lives:" +  currentHealth + "/" + maxHealth;
            thePlayer.Knockback(direction);
            invicibleCounter = invicibleTime;

            playerRenderer.enabled = false;
            flashCounter = flashTime;
        }
        
    }
}
