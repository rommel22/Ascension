using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNextScene : INextScene
{
    public override void moveToNextScene() {
        CurrentLevelTracker.latestLevel = SceneManager.GetActiveScene().buildIndex + 1;
        base.moveToScene(CurrentLevelTracker.latestLevel);
    }
}