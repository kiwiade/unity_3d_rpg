using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.parent.GetComponent<Monster2>().PlayerDetect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.parent.GetComponent<Monster2>().BasicPattern();
        }
    }
}
