using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour {

    public GameObject mapCamera = null;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.M))
        {
            mapActive();
        }
	}

    private void mapActive()
    {
        if(mapCamera.activeSelf == true)
        {
            mapCamera.SetActive(false);
        }
        else if(mapCamera.activeSelf == false)
        {
            mapCamera.SetActive(true);
        }
    }
}
