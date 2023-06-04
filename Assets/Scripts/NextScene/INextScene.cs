using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class INextScene: MonoBehaviour
{
    public void moveToScene(int sceneID){
        SceneManager.LoadScene(sceneID);
    }

    public abstract void moveToNextScene();
}
