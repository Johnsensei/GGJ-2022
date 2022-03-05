using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Level 00", menuName = "Level Loading")]
public class LevelScriptableObject : ScriptableObject
{
    public int BuildIndex;
    public Sprite Image;
    public string Name;
    public float Score;
    public static float MaxScore { get { return 3; } }
}
