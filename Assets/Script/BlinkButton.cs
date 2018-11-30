using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkButton : MonoBehaviour {

    private Color buttonColor = Color.white;
    public float fTime = 0;
    public string status = "up";
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        fTime += Time.deltaTime * 2.0f;

        if (status == "down")
            buttonColor.a = Mathf.Lerp(1, 0, fTime);
        else
            buttonColor.a = Mathf.Lerp(0, 1, fTime);

        if (fTime > 1.0f)
        {
            fTime = 0;
            if (status == "down")
                status = "up";
            else
                status = "down";
        }

        GetComponent<Image>().color = buttonColor;
	}
}
