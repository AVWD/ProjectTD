using UnityEngine;
using System.Collections;

public class SimpleTower : MonoBehaviour
{
    public Transform target;
    public GameObject shotPrefab;
    float shotTime = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shotTime += Time.deltaTime;
        if (shotTime >= 0.75f)
        {
            shotTime = 0;
            GameObject shot = (GameObject)Instantiate(shotPrefab, this.transform);
            shot.transform.position = transform.position;
            Missile missileScript = shot.GetComponent<Missile>();
            missileScript.Target = this.target;
        }
    }
}
