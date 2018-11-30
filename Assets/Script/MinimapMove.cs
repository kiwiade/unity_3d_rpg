using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMove : MonoBehaviour {

    public GameObject player = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PlayerFollow();
    }

    private void PlayerFollow()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.y += 20;

        GetComponent<Camera>().transform.position = playerPos;
    }
}
