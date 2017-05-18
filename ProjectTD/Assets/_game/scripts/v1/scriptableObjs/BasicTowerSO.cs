using UnityEngine;
using System.Collections;
using System.IO;

[CreateAssetMenu(menuName = "avwd/Towers/Basic")]
public class BasicTowerSO : ScriptableObject
{
    [Header("Stats")]
    public BasicProjectileSO ProjectileData;
    public float Range;
    public float CoolDown;

    [Header("Maint")]
    public double Cost;

    // Upgrade costs
    // Damage mods per level

    BasicTowerSO LoadFromFile(string path)
    {
        string json = File.ReadAllText(path);
        BasicTowerSO retVal = CreateInstance<BasicTowerSO>();
        JsonUtility.FromJsonOverwrite(json, retVal);
        return retVal;
    }

}
