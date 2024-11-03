using System;
using UnityEngine;

[Serializable]
public struct LevelData
{
    public int levelNumber;
    public string levelSceneName;
}

[CreateAssetMenu(fileName = "LevelsConfig", menuName = "Levels config", order = 1)]
public class LevelsConfig : ScriptableObject
{
    [SerializeField] private LevelData[] levels;

    public LevelData[] Levels => levels;
}
