using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "newLevel", menuName = "Level")]
[Serializable]
public class LevelSo : ScriptableObject
{
    public string levelName;
    [FormerlySerializedAs("scene")] public string sceneName;
    public float rageNeeded = 100;
    public float fixMultiplier = 1;
}