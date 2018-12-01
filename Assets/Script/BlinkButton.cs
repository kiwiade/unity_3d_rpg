using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkButton : MonoBehaviour {

    private Color buttonColor = Color.white;
    private float fTime = 0;
    [SerializeField]
    private string status = "up";

    public float FTime
    {
        get
        {
            return fTime;
        }

        set
        {
            fTime = value;
        }
    }

    public string Status
    {
        get
        {
            return status;
        }

        set
        {
            status = value;
        }
    }

    // Update is called once per frame
    void Update () {
        FTime += Time.deltaTime * 2.0f;

        if (Status == "down")
            buttonColor.a = Mathf.Lerp(1, 0, FTime);
        else
            buttonColor.a = Mathf.Lerp(0, 1, FTime);

        if (FTime > 1.0f)
        {
            FTime = 0;
            if (Status == "down")
                Status = "up";
            else
                Status = "down";
        }

        GetComponent<Image>().color = buttonColor;
	}
}
