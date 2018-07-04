using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour {

    [HideInInspector]
    public Vector3 moveDir = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += moveDir;
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
