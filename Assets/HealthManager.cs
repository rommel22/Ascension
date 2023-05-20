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
    public Text retryText;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashTime = 0.1f;
    // Start is called before the first frame update

    private bool isRespawning;
    private Vector3 respawnPoint;
    private int retries = 0;
    public float restartLength;

    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "Lives:" +  currentHealth + "/" + maxHealth;
        retryText.text = "\nRetries:" + retries;
        // thePlayer = FindObjectOfType<PlayerMovement>();
        respawnPoint = thePlayer.transform.position;
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
        if (thePlayer.transform.position.y < -5){
            Respawn();
        }
    }

    public void Hurt(int damage, Vector3 direction){
        if (invicibleCounter <= 0){
            currentHealth -= damage;
            if (currentHealth <= 0){
                Respawn();
            }else{
                healthText.text = "Lives:" +  currentHealth + "/" + maxHealth;
                thePlayer.Knockback(direction);
                invicibleCounter = invicibleTime;
                playerRenderer.enabled = false;
                flashCounter = flashTime;
            }
            
        }
        
    }

    public void Respawn(){
        // thePlayer.transform.position = respawnPoint;
        // currentHealth = maxHealth;
        // healthText.text = "Lives:" +  currentHealth + "/" + maxHealth;
        // retryText.text = "\nRetries:" + ++retries;

        if (!isRespawning){
            StartCoroutine("RespawnCo");
        }
        
    }

    public IEnumerator RespawnCo(){
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);

        yield return new WaitForSeconds(restartLength);
        isRespawning = false;
        thePlayer.gameObject.SetActive(true);
        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;
        healthText.text = "Lives:" +  currentHealth + "/" + maxHealth;
        retryText.text = "\nRetries:" + ++retries;

        invicibleCounter = invicibleTime;
        playerRenderer.enabled = false;
        flashCounter = flashTime;
    }
}
