using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnButtonClick() {
        CurrentLevelTracker.latestLevel = 1;
        SceneManager.LoadScene(1);
    }
}
