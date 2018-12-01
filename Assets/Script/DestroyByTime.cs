using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

    private float destroyTime = 1.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);		
	}	
}
