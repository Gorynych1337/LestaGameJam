using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelSlot : MonoBehaviour, IDataView<LevelData>
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Button button;
    [SerializeField] private string levelSceneName;
    
    private LevelData _data;
    
    public Button Button => button;
    public LevelData Data => _data;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(StartLevel);
    }

    private void StartLevel()
    {
        AudioManager.Instance.Play("ButtonClick");
        AudioManager.Instance.Stop(AudioType.Music);
        GameManager.Instance.CurrentLevel = _data.levelNumber;
        GameManager.Instance.FadeWithLoadScene(_data.levelSceneName);
    }

    public void SetData(LevelData data)
    {
        _data = data;
        levelText.text = data.levelNumber + "\n" + levelSceneName;
    }
    
    private void OnDestroy()
    {
        button.onClick.RemoveListener(StartLevel);
    }
}
