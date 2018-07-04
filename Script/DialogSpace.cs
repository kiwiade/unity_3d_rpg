using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSpace : MonoBehaviour {

    public GameObject SpaceButton = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpaceButton.SetActive(true);
            SpaceButton.GetComponent<Image>().color = Color.white;
            SpaceButton.GetComponent<BlinkButton>().fTime = 0;
            SpaceButton.GetComponent<BlinkButton>().status = "up";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            SpaceButton.SetActive(false);
        }
    }
}
