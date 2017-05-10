using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StandardTower : TowerBase
{
    public List<PayloadBase> PayloadList = new List<PayloadBase>();
    float shotTime = 0;

    public Transform Target;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shotTime += Time.deltaTime;
        if(shotTime >= 0.5f)
        {
            shotTime = 0;
            PayloadList[0].Fire(Target);
        }
    }
}
