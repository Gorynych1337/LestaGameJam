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
    [SerializeField] private Button creditsButton;
    [SerializeField] private LevelMenuPanel LevelMenuPanel;
    [SerializeField] private SettingsMenuPanel settingsMenuPanel;
    [SerializeField] private CreditsPanel creditsPanel;
    [SerializeField] private LevelsConfig levelsConfig;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
        creditsButton.onClick.AddListener(CreditsButtonClicked);
    }

    private void CreditsButtonClicked()
    {
        AudioManager.Instance.Play("ButtonClick");
        Hide();
        creditsPanel.Show();
    }

    private void PlayButtonClicked()
    {
        AudioManager.Instance.Play("ButtonClick");
        Hide();
        LevelMenuPanel.Show(levelsConfig);
    }

    private void SettingsButtonClicked()
    {
        Hide();
        AudioManager.Instance.Play("ButtonClick");
        settingsMenuPanel.Show(SettingsData.Instance);
    }

    private void ExitButtonClicked()
    {
        AudioManager.Instance.Play("ButtonClick");
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
