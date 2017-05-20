using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Serlialize level data in json
public class LevelData : MonoBehaviour {
    [HideInInspector] // Because we have a custom renderer
    public List<MobWave> Waves = new List<MobWave>();

    public Vector2 SpawnLocation = Vector2.zero;
    public float WaveTimer = 10f;
    public float MobDelaySpacer = 0.20f;
    public int CurrentWave = 0;
    private float eTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        eTime += Time.deltaTime;

        if(eTime >= WaveTimer && CurrentWave < Waves.Count)
        {
            MobWave wave = Waves[CurrentWave];

            for(int i = 0; i < wave.Count; i++)
            {
                StartCoroutine(SpawnIt(MobDelaySpacer * i, wave));
            }

            eTime -= WaveTimer;
            CurrentWave++;
        }
	}

    IEnumerator SpawnIt(float delay, MobWave wave)
    {
        yield return new WaitForSeconds(delay);
        GameObject newMob = Instantiate(wave.Prefab) as GameObject;
        newMob.transform.parent = this.transform;
        newMob.transform.position = SpawnLocation;
    }
}
