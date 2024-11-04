using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsPanel : UIPanel
{
    [SerializeField] MainMenuPanel mainMenuPanel;
    [SerializeField] Button backButton;
    
    private void Start()
    {
        backButton.onClick.AddListener(BackButtonClicked);
    }

    private void BackButtonClicked()
    {
        AudioManager.Instance.Play("ButtonClick");
        Hide();
        mainMenuPanel.Show();
    }

    protected override void OnShow()
    {
    }

    protected override void OnHide()
    {
    }
    
    protected void OnDestroy()
    {
        backButton.onClick.RemoveListener(BackButtonClicked);
    }
}
