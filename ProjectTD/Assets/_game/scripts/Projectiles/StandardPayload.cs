using UnityEngine;
using System.Collections;
using AVWD.Engine;
using AVWD.Engine.Services;
using System;

public class StandardPayload : PayloadBase
{
    public Transform parentTransform;
    public Pool<GameObject> projectilePool;
    protected int poolSize = 32;
    Rigidbody2D rb;

    public override void Init()
    {
        this.projectilePool = new Pool<GameObject>((o) => {
            return (GameObject)Instantiate(projectilePrefab);
        }, poolSize);

        parentTransform = transform.parent;
    }

    public override void DoUpdate()
    {

    }

    public override void Fire(Transform target)
    {
        GameObject bullet = this.projectilePool.Fetch();
        bullet.SetActive(true);
        bullet.transform.position = parentTransform.position;
        bullet.transform.rotation = parentTransform.rotation;

        rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 600);
        StartCoroutine(Release(bullet));
    }

    IEnumerator Release(GameObject bullet)
    {
        yield return new WaitForSeconds(10);
        bullet.SetActive(false);
        projectilePool.Release(bullet);
    }
}
