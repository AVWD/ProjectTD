using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct MobWave {

    public enum WaveType
    {
        normal, armored, fast, giant, boss
    }

    public WaveType Type;
    public GameObject Prefab;
    public int Count;
}
