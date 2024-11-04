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
        FadeWithLoadScene(() => SceneManager.LoadScene(index));
    }

    public void FadeWithLoadScene(string sceneName)
    {
        FadeWithLoadScene(() => SceneManager.LoadScene(sceneName));
    }   

   private void FadeWithLoadScene(Action loadSceneAction)
    {
        DOTween.Sequence()
            .OnStart(() => fader.gameObject.SetActive(true))
            .Append(fader.DOFade(1f, 1f))
            .AppendInterval(1f)
            .AppendCallback(() => loadSceneAction())
            .AppendInterval(0.5f)
            .Append(fader.DOFade(0f, 0.5f))
            .OnComplete(() => fader.gameObject.SetActive(false))
            .OnKill(() => fader.gameObject.SetActive(false))
            .SetLink(gameObject);
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
