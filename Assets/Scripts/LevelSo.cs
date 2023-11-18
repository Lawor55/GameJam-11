using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "newLevel", menuName = "Level")]
[Serializable]
public class LevelSo : ScriptableObject
{
    public string levelName;
    public SceneAsset scene;
    public float rageNeeded = 100;
    public float fixMultiplier = 1;
}