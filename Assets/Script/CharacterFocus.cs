using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFocus : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //GameObject Player = GameObject.FindGameObjectWithTag("Player");
        //Vector3 CameraPos = new Vector3(Player.transform.position.x, Player.transform.position.y + 6.0f, Player.transform.position.z + 10.0f);
        //transform.position = CameraPos;
        transform.position = player.transform.position + offset;

        const int minFoV = 20;
        const int maxFoV = 80;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // forward
        {
            Camera.main.fieldOfView++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // back
        {
            Camera.main.fieldOfView--;
        }
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFoV, maxFoV);
    }
}
