using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentLevelTracker : MonoBehaviour
{
    public static int _latestLevel = 0;
    public static int latestLevel {
        get {
            return _latestLevel;
        }
        set {
            _latestLevel = value;
            SaveSystem.Save();
        }
    }

    void Start()
    {
        SaveSystem.Load();
    }
    public void StartButtonClick() {
        latestLevel = 1;
        SceneManager.LoadScene(1);
    }
}
