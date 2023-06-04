using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevelNextScene : INextScene
{
    public override void moveToNextScene() {
        CurrentLevelTracker.latestLevel = 0;
        base.moveToScene(CurrentLevelTracker.latestLevel);
    }
}
