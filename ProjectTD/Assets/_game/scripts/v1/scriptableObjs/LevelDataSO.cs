using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[CreateAssetMenu(menuName = "avwd/Game/Level")]
public class LevelDataSO : ScriptableObject
{
    // TODO: Try this implementation instead

    //[HideInInspector] // Because we have a custom renderer
    public List<MobWave> Waves = new List<MobWave>();

    LevelDataSO LoadFromFile(string path)
    {
        string json = File.ReadAllText(path);
        LevelDataSO retVal = CreateInstance<LevelDataSO>();
        JsonUtility.FromJsonOverwrite(json, retVal);
        return retVal;
    }
}
