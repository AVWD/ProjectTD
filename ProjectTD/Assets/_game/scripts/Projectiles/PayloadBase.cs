using UnityEngine;
using System.Collections;


public abstract class PayloadBase : MonoBehaviour
{
    public GameObject projectilePrefab;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        DoUpdate();
    }

    public abstract void Init();
    public abstract void DoUpdate();

    public virtual void Fire(Transform target)
    {

    }
}
