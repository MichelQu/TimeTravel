using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer_constraint : MonoBehaviour {

    public Transform Limit_transform;
    private float limit_position;
    private float drawer_position;
	// Use this for initialization
	void Start () {
        limit_position = Limit_transform.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        drawer_position = transform.position.x;
        if (drawer_position - limit_position > 0.371f)
            drawer_position = limit_position + 0.371f;
	}
}
