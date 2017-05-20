using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {

    public float distFromCamera = 10f;
    public float speed = 8f;
    Vector3 target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        target = Input.mousePosition;
        target.z = distFromCamera;
        target = Camera.main.ScreenToWorldPoint(target);
        transform.position = Vector3.Lerp(transform.position, target, 1.0f - Mathf.Exp(-speed * Time.deltaTime));
    }
}
