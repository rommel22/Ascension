using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNextScene : INextScene
{
    public override void moveToNextScene() {
        base.moveToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}