using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDir : MonoBehaviour {

    private GameObject player = null;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if(player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if(distance < 15f)
            {
                transform.LookAt(player.transform.position);
            }
        }
	}
}
