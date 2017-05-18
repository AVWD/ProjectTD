using System;
using UnityEngine;
using System.Collections;
using AVWD.Engine.Services;

public enum DamageType
{
    Impact, Poison, Slow, Burn
}

public class PayloadBase : MonoBehaviour
{
    //public GameObject projectilePrefab;

    public float z, zOffset;
    Vector2 pos, vel, prevPos;
    public float life = 1f;
    public int dmg = 1;
    public float size = 8f; // 16x16
    protected float t = 0.011f;
    protected float speedMod = 1f;
    public DamageType dmgType = DamageType.Impact;
    // audio

    public MonoBehaviour shotBy;
    
    public Vector2 Position
    {
        get
        {
            return pos;
        }
        set
        {
            this.pos = value;
            base.transform.position = new Vector3(value.x, value.y, z);
        }
    }
    public Vector2 Velocity
    {
        get
        {
            return vel;
        } set
        {
            this.vel = value;
            this.SetRotation();
        }
    }

    protected virtual void Awake()
    {

    }

    // Use this for initialization
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.t = Mathf.Clamp(Time.deltaTime, 0f, 0.334f);
        UpdateProjectile(t);
        UpdateLife();
    }

    protected virtual void FixedUpdate()
    {
        //Debug.DrawLine(this.launchPos, this.targetPos, Color.green);
        //Debug.DrawLine(prevPos, pos, Color.yellow);
        //Debug.DrawRay(this.previousPos, dir, Color.cyan);
    }

    protected virtual void UpdateProjectile(float t)
    {
        if (!gameObject.activeSelf || !enabled) return;

        MoveProjectile();

        // Check collision
    }
    protected virtual void UpdateLife()
    {

    }

    protected virtual void MoveProjectile()
    {
        prevPos = pos;
        //pos += pos * t * speedMod;
        transform.Translate(pos * t * speedMod);
    }

    protected virtual void SetRotation()
    {
        if(vel.x > 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.eulerAngles = new Vector3(0f, 0f, MathHelper.GetAngle(vel.y, -vel.x));
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.eulerAngles = new Vector3(0f, 0f, MathHelper.GetAngle(vel.y, -vel.x));
        }
    }

    public virtual void Fire(Vector2 pos, Vector2 vel, float z, MonoBehaviour shooter)
    {
        t = Time.deltaTime;
        this.pos = pos;
        this.vel = vel;
        SetRotation();
        this.shotBy = shooter;
        zOffset = z;
    }

}
