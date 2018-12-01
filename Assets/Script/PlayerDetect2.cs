using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect2 : MonoBehaviour {

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
