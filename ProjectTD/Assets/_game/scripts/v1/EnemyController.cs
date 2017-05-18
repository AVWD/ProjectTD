using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    public BasicEnemySO EnemyData;

    public float HP;
    public float Speed;
    public float Armor;

    // Use this for initialization
    void Start()
    {
        HP = EnemyData.HP;
        Speed = EnemyData.Speed;
        Armor = EnemyData.Armor;

        Vector3 position = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        transform.DOMove(position, 1f, false)
            .SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
