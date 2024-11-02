using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : UIPanel
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private SettingsMenuPanel settingsMenuPanel;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        exitButton.onClick.AddListener(ExitButtonClicked);
    }

    private void PlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    private void SettingsButtonClicked()
    {
        Hide();
        settingsMenuPanel.Show(SettingsData.Instance);
    }

    private void ExitButtonClicked()
    {
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
