using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MouseFollower : MonoBehaviour {

    [Header("Settings")]
    [Range(1, 10)]
    public float moveSpeed = 2;
    public bool RequiresClick = true;

    Rigidbody2D rb;
    Vector2 pos;
    Vector2 mousePos;
    Vector2 direction;

    // TEMP net testing
    public NetManager netMgr;
    [System.Serializable]
    public class PlayerMovePacket
    {
        public int uid;
        public string evt;
        public Vector2 pos; 

        public static PlayerMovePacket FromPacket(string json)
        {
            return JsonUtility.FromJson<PlayerMovePacket>(json);
        }
        public string ToPacket()
        {
            return JsonUtility.ToJson(this);
        }
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        mousePos = transform.position;
    }

    void Update()
    {
        // Parent's position
        pos = transform.position;
        // Target's position in world space
        if (RequiresClick)
        {
            if(Input.GetMouseButton(0))
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(netMgr != null)
                {
                    string msg = (new PlayerMovePacket() { evt = "move", pos = mousePos, uid = 0 }).ToPacket();
                    netMgr.Send("move", msg);
                }
            }

        } else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
            
        // Direction to target as a unit vector (scaled down to 1 so it can be multiplied)
        direction = (mousePos - pos).normalized;

        if ((mousePos - pos).SqrMagnitude() > (moveSpeed * moveSpeed * Time.deltaTime))
        {
            // Direction * speed * (per fraction of a second)
            rb.transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
