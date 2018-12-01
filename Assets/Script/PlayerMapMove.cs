using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapMove : MonoBehaviour {

    [SerializeField]
    private GameObject player = null;
    Vector3 playerPos = Vector3.zero;

	// Use this for initialization
	void Start () {
        playerPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerPos == player.transform.position)
            return;

        transform.position += new Vector3(
            (player.transform.position.x - playerPos.x) * 0.038f,
            (player.transform.position.y - playerPos.y) * 0.038f,
            (player.transform.position.z - playerPos.z) * 0.038f
            );

        playerPos = player.transform.position;
    }
}
