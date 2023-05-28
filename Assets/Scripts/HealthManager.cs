using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    private int _currentHealth;
    public int currentHealth {
        set {
            _currentHealth = value;
            healthBar.SetHealth(value);
        }
        get { return _currentHealth; }
    }

    public PlayerMovement thePlayer;
    
    public float invicibleTime;
    private float invicibleCounter;

    public HealthBar healthBar;
    public GameObject respawnPoint;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashTime = 0.1f;
    // Start is called before the first frame update

    private bool isRespawning;
    private int retries = 0;
    public float restartLength;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
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
            invicibleCounter = invicibleTime;
            flashCounter = flashTime;

            if (currentHealth <= 0){
                Respawn();
            }else{
                thePlayer.Knockback(direction);
            }
        }
    }

    public void InstantKill()
    {
        Hurt(maxHealth, new Vector3(0, 0, 0));
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
        thePlayer.transform.position = respawnPoint.transform.position;
        currentHealth = maxHealth;
    }

    public void setRespawnPoint(GameObject newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
}
