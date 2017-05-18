using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "avwd/Projectiles/Basic")]
public class BasicProjectileSO : ScriptableObject {
    // OnEnter
    // OnExit
    // OnDestroy

    [Header("Stats")]
    public GameObject Projectile;
    public float Damage;
    public float TravelTime;
    public Ease EaseType;
}
