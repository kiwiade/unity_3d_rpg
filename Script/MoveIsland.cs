using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIsland : MonoBehaviour {

    private Vector3 playerPos = Vector3.zero;
    private GameObject player = null;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform.position;

        GameObject playerHPMPbar = GameObject.FindGameObjectWithTag("PlayerHPMP");
        playerHPMPbar.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        Animator ani = player.GetComponent<Animator>();
        ani.SetBool("Static_b", true);
        ani.SetBool("Crouch_b", true);

        player.transform.position = new Vector3(player.transform.position.x, playerPos.y, player.transform.position.z);
	}
}
