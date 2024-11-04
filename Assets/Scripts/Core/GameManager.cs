using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Scripts.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private IngameMenuPanel ingameMenuPanel;
    [SerializeField] private Image fader;
    [SerializeField]private int level = 1;
    
    public int Level => level;
    public int CurrentLevel { get; set; }
    public bool IsPaused { get; private set; }
    
    private void Awake()
    {
        base.Awake();
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

    public void FinishLevel()
    {
        level = CurrentLevel + 1;
        SaveLevel();
    }

    private void SaveLevel()
    {
        GameData.Instance.Level = level;
        GameData.Instance.Save();
    }
    
    public void FadeWithLoadScene(int index)
    {
        DOTween.Sequence()
            .OnStart(() => fader.gameObject.SetActive(true))
            .Append(fader.DOFade(1, 1))
            .AppendCallback(() => SceneManager.LoadScene(index))
            .SetLink(gameObject)
            .Append(fader.DOFade(0, 1))
            .OnComplete(() => fader.gameObject.SetActive(false))
            .OnKill(() => fader.gameObject.SetActive(false));
    }

    private void OnApplicationQuit()
    {
        SaveLevel();
    }

    private void OnDestroy()
    {
        SaveLevel();
    }
}
