using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    public bool _canBePaused;
    public bool canBePaused
    {
        set {
            _canBePaused = value;
            if (!_canBePaused && isPaused) ContinueGame();
        }
        get { return _canBePaused; }
    }
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        canBePaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canBePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) {
                ContinueGame();
            } 
            else {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ContinueGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
