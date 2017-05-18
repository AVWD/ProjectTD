using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {

    LineRenderer lineRenderer;
    //public Transform laserHit;
    public Transform target;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        //laserHit.position = hit.point;
        if(hit.collider != null)
        {
            //Debug.DrawLine(transform.position, hit.point);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }

        transform.rotation = Quaternion.LookRotation(transform.forward, target.position - transform.position);
    }
}
