using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IngameSettingsPanel : UIPanel<SettingsData>{

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Button backButton;
    [SerializeField] private IngameMenuPanel ingameMenuPanel;
    
    public UnityAction onBack;
    
    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(OnChangeSlider);
        musicSlider.onValueChanged.AddListener(OnChangeSlider);
        SFXSlider.onValueChanged.AddListener(OnChangeSlider);
        backButton.onClick.AddListener(BackButtonClicked);
    }

    private void BackButtonClicked()
    {
        Hide();
        AudioManager.Instance.Play("ButtonClick");
        ingameMenuPanel.Show();
        try
        {
            onBack?.Invoke();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    protected override void SetUp(SettingsData settingsData)
    {
        masterSlider.value = settingsData.masterVolume;
        musicSlider.value = settingsData.musicVolume;
        SFXSlider.value = settingsData.SFXVolume;
    }

    private void OnChangeSlider(float _)
    {
        _argument.masterVolume = masterSlider.value;
        _argument.musicVolume = musicSlider.value;
        _argument.SFXVolume = SFXSlider.value;
        SettingsData.Instance.UpdateAudioVolume();
    }

    protected override void OnShow()
    {
    }

    protected override void OnHide()
    {
        SettingsData.Instance.Save();
    }

    private void OnDestroy()
    {
        masterSlider.onValueChanged.RemoveListener(OnChangeSlider);
        musicSlider.onValueChanged.RemoveListener(OnChangeSlider);
        SFXSlider.onValueChanged.RemoveListener(OnChangeSlider);
    }
}