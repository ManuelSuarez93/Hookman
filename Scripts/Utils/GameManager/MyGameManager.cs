using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager instance;

    [Tooltip("The level that the hook will have at the start of the game.")]
    public int initialHookLevel = 1;
    [Tooltip("The range in units the hook will reach at the start of the game.")]
    public int initialRangeLevel = 1;

    public int hookLevel { get; private set; }

    public int ropeLevel { get; private set; }

    public int enemiesDefeated { get; private set; }

    public int points { get; private set; }

    public CheckpointSystem cs { get; private set; }

    public Events events;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            hookLevel = 1;
            ropeLevel = 1;
            enemiesDefeated = 0;
            points = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hook level functions

        public void HookLevelUp()
        {
            hookLevel += 1;
            events.onHookLevelUp.Invoke();
        }

        public void SetHookLevel(int level)
        {
            hookLevel = level;
        }

    // Rope level functions

        public void RopeLevelUp()
        {
            ropeLevel += 1;
            events.onRopeLevelUp.Invoke();
        }

        public void SetRopeLevel(int level)
        {
            ropeLevel = level;
        }

    // Enemies defeated functions

        public void AddEnemyDefeated()
        {
            AddEnemyDefeated(1);
        }

        public void AddEnemyDefeated(int amount)
        {
            enemiesDefeated += amount;
        }

    // Points functions

        public void AddPoints(int amount)
        {
           points += amount;
           events.onPointsUp.Invoke();
        }

    // Checkpoint System functions

        public void SetCheckpointSystem(CheckpointSystem cs)
        {
            this.cs = cs;
        }

        public void SaveCheckpoint()
        {
            cs.Save();
        }

        public void LoadCheckpoint()
        {
            cs.Load();
        }
}

[Serializable]
public class Events
{
    public UnityEvent onRopeLevelUp;
    public UnityEvent onHookLevelUp;
    public UnityEvent onPointsUp;
}
