using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField]private int level = 0;
    public int Level => level;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        level = GameData.Instance.Level;
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
