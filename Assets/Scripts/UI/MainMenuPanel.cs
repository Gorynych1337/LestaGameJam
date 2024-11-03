using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : UIPanel
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private LevelMenuPanel LevelMenuPanel;
    [SerializeField] private SettingsMenuPanel settingsMenuPanel;
    [SerializeField] private LevelsConfig levelsConfig;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
    }

    private void PlayButtonClicked()
    {
        AudioManager.Instance.Play("ButtonClick", AudioType.Sound);
        Hide();
        LevelMenuPanel.Show(levelsConfig);
    }

    private void SettingsButtonClicked()
    {
        Hide();
        AudioManager.Instance.Play("ButtonClick", AudioType.Sound);
        settingsMenuPanel.Show(SettingsData.Instance);
    }

    private void ExitButtonClicked()
    {
        AudioManager.Instance.Play("ButtonClick", AudioType.Sound);
        Application.Quit();
    }

    protected override void OnShow()
    {
    }

    protected override void OnHide()
    {
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(PlayButtonClicked);
        settingsButton.onClick.RemoveListener(SettingsButtonClicked);
        exitButton.onClick.RemoveListener(ExitButtonClicked);
    }
}
