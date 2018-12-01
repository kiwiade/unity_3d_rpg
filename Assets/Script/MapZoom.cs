using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapZoom : MonoBehaviour {

    private readonly Vector3 basicPos = new Vector3(-17.63f, 8.256765f, -181.773f);
    private bool clickcheck = false;
    private Vector3 mousePos = Vector3.zero;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (GetComponent<Camera>().orthographicSize > 1.0f)
                GetComponent<Camera>().orthographicSize -= 0.1f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(GetComponent<Camera>().orthographicSize < 4.2f)
                GetComponent<Camera>().orthographicSize += 0.1f;

            if (GetComponent<Camera>().orthographicSize == 4.2f)
                transform.position = basicPos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            clickcheck = true;
            mousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickcheck = false;
            mousePos = Vector3.zero;
        }

        if(clickcheck)
        {
            Vector3 diff = Input.mousePosition - mousePos;
            diff.z = diff.y;
            diff.y = 0;

            transform.position += (diff * 0.03f);

            mousePos = Input.mousePosition;
        }
    }
}
