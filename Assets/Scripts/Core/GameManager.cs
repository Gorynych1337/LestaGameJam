using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private IngameMenuPanel ingameMenuPanel;
    [SerializeField]private int level = 0;

    public int Level => level;
    public bool IsPaused { get; private set; }


    private void Awake()
    {
        DontDestroyOnLoad(this);
        level = GameData.Instance.Level;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsPaused = true;
        ingameMenuPanel.Show();
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsPaused = false;
        ingameMenuPanel.Hide();
    }

    private void OnApplicationQuit()
    {
        GameData.Instance.Level = level;
        GameData.Instance.Save();
    }

    private void OnDestroy()
    {
        GameData.Instance.Level = level;
        GameData.Instance.Save();
    }
}
