using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class IngameMenuPanel : UIPanel
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private IngameSettingsPanel settingsMenuPanel;

    private void Awake()
    {
        continueButton.onClick.AddListener(ContinueButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
    }

    private void ContinueButtonClicked()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.ResumeGame();
    }

    private void SettingsButtonClicked()
    {
        Hide();
        AudioManager.Instance.Play("ButtonClick");
        settingsMenuPanel.Show(SettingsData.Instance);
    }

    private void MainMenuButtonClicked()
    {
        AudioManager.Instance.Play("ButtonClick");
        GameManager.Instance.ResumeGame();
        SceneManager.LoadScene(0);
    }

    protected override void OnShow()
    {
    }

    protected override void OnHide()
    {
        settingsMenuPanel.Hide();
    }

    private void OnDestroy()
    {
        continueButton.onClick.RemoveListener(ContinueButtonClicked);
        settingsButton.onClick.RemoveListener(SettingsButtonClicked);
        mainMenuButton.onClick.RemoveListener(MainMenuButtonClicked);
    }
}
