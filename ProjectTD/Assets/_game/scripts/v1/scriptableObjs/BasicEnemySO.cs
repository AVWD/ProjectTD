using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "avwd/Units/Basic")]
public class BasicEnemySO : ScriptableObject
{
    [Header("Stats")]
    public float HP;
    public float Speed;
    public float Armor;
}
