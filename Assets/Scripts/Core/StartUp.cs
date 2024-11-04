using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{
    [SerializeField] private MainMenuPanel mainMenuPanel;
    void Start()
    {
        mainMenuPanel.Show();
        SettingsData.Instance.UpdateAudioVolume();
        AudioManager.Instance.Play("MenuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
