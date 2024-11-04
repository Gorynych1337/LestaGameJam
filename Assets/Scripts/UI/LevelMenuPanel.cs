using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelMenuPanel : UIPanel<LevelsConfig>
{
    [SerializeField] private MainMenuPanel mainMenuPanel;
    [SerializeField] private SimpleList<LevelSlot, LevelData> levelSlotList;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button backButton;
    
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(BackButtonClicked);
    }

    private void BackButtonClicked()
    {
        Hide();
        AudioManager.Instance.Play("ButtonClick");
        mainMenuPanel.Show();
    }

    protected override void SetUp(LevelsConfig argument)
    {
        levelSlotList.SetData(argument.Levels.ToList());
        scrollRect.normalizedPosition = new Vector2(0, 1);

        foreach (var levelSlot in levelSlotList.Elements())
        {
            levelSlot.Button.interactable = levelSlot.Data.levelNumber <= GameManager.Instance.Level;
        }
    }

    protected override void OnShow()
    {
    }

    protected override void OnHide()
    {
        levelSlotList.Clear();
    }
}
