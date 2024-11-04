using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScenePanel : UIPanel
{
    [SerializeField] private List<Sprite> _backgrounds;
    [SerializeField] private Image _imgObj;
    [SerializeField] private Button _continueButton;
    [SerializeField] private string _nextSceneName;
    [SerializeField] private string _musicName;

    private int _index;

    private void Awake()
    {
        _continueButton.onClick.AddListener(ContinueButtonClicked);
        _index = 0;
        _imgObj.sprite = _backgrounds[0];
        AudioManager.Instance.Play(_musicName);
    }

    private void ContinueButtonClicked()
    {
        _index++;
        if (_index == _backgrounds.Count)
        {
            SceneManager.LoadScene(_nextSceneName);
            return;
        }
        
        AudioManager.Instance.Play("ButtonClick");
        _imgObj.sprite = _backgrounds[_index];
    }


    protected override void OnShow()
    {
    }

    protected override void OnHide()
    {
    }
}
