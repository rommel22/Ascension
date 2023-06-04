using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLevelTracker : MonoBehaviour
{
    public static int latestLevel = 0;
    public void StartButtonClick() {
        latestLevel = 1;
        SceneManager.LoadScene(1);
    }
}
