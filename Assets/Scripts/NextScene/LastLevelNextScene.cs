using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelNextScene : INextScene
{
    public override void moveToNextScene() {
        base.moveToScene(0);
    }
}
