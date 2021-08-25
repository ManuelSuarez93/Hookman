using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameMessanger : MonoBehaviour
{
    // Hook level functions

    public void HookLevelUp()
    {
        MyGameManager.instance.HookLevelUp();
    }

    public void SetHookLevel(int level)
    {
        MyGameManager.instance.SetHookLevel(level);
    }

    // Rope level functions

    public void RopeLevelUp()
    {
        MyGameManager.instance.RopeLevelUp();
    }

    public void SetRopeLevel(int level)
    {
        MyGameManager.instance.SetRopeLevel(level);
    }

    // Enemies defeated functions

    public void AddEnemyDefeated()
    {
        MyGameManager.instance.AddEnemyDefeated();
    }

    public void AddEnemyDefeated(int amount)
    {
        MyGameManager.instance.AddEnemyDefeated(amount);
    }

    // Points functions

    public void AddPoints(int amount)
    {
        MyGameManager.instance.AddPoints(amount);
    }

    // Checkpoint System functions

    public void SetCheckpointSystem(CheckpointSystem cs)
    {
        MyGameManager.instance.SetCheckpointSystem(cs);
    }

    public void SaveCheckpoint()
    {
        MyGameManager.instance.SaveCheckpoint();
    }

    public void LoadCheckpoint()
    {
        MyGameManager.instance.LoadCheckpoint();
    }
}
