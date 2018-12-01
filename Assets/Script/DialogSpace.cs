using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSpace : MonoBehaviour {

    [SerializeField]
    private GameObject SpaceButton = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpaceButton.SetActive(true);
            SpaceButton.GetComponent<Image>().color = Color.white;
            SpaceButton.GetComponent<BlinkButton>().FTime = 0;
            SpaceButton.GetComponent<BlinkButton>().Status = "up";
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
