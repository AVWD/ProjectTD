using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WaveManager : MonoBehaviour
{
    static WaveManager _instance;
    public static WaveManager Instance
    {
        get
        {
            if (WaveManager._instance == null)
            {
                WaveManager._instance = (UnityEngine.Object.FindObjectOfType(typeof(WaveManager)) as WaveManager);
                if (null == WaveManager._instance)
                {
                    WaveManager._instance = new GameObject("WaveManager").AddComponent<WaveManager>();
                }
            }
            return WaveManager._instance;
        }
    }

    public List<EnemyBase> EnemyList = new List<EnemyBase>();

    public List<EnemyBase> StrongestSort = new List<EnemyBase>();
    public List<EnemyBase> ArmoredSort = new List<EnemyBase>();
    public List<EnemyBase> FastestSort = new List<EnemyBase>();
    public List<EnemyBase> ClosestSort = new List<EnemyBase>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Horribly inefficient I'm sure. Just a crude initial jab at this.
        StrongestSort = EnemyList.OrderBy(o => o.HP).ToList();
        ArmoredSort = EnemyList.OrderBy(o => o.Armor).ToList();
        FastestSort = EnemyList.OrderBy(o => o.Speed).ToList();
        
        // Instead start a broad phase collision vs tower range.
    }
}
