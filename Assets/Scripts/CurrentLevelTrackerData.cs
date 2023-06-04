using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentLevelTrackerData
{
    public int latestLevel;

    public CurrentLevelTrackerData()
    {
        latestLevel = CurrentLevelTracker.latestLevel;
    }
}
