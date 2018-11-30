using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayLight : MonoBehaviour {

    private const int maxLightColor = 4;
    private Color[] lightColors = new Color[maxLightColor];
    private Quaternion[] quaternions = new Quaternion[maxLightColor];

    private int nowColor = 0;
    private float fChangeTime = 0;

    private Color newColor = Color.white;
    // Use this for initialization
    void Start () {
        lightColors[0] = new Color(0.372f, 0.231f, 0.658f, 1f);
        lightColors[1] = new Color(0.8f, 0.8f, 0.8f, 1f);
        lightColors[2] = new Color(0.75f, 0.35f, 0.45f, 1f);
        lightColors[3] = new Color(0.35f, 0.35f, 0.35f, 1f);
        GetComponent<Light>().color = lightColors[0];

        quaternions[0].eulerAngles = new Vector3(200, 0, 0);
        quaternions[1].eulerAngles = new Vector3(130, 0, 0);
        quaternions[2].eulerAngles = new Vector3(60, 0, 0);
        quaternions[3].eulerAngles = new Vector3(-20, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        fChangeTime += Time.deltaTime * 0.1f;
        if(fChangeTime >= 1.0f)
        {
            nowColor = (nowColor + 1) % 4;
            fChangeTime = 0;
        }
        int nextColor = (nowColor + 1) % 4;
        newColor = Color.Lerp(lightColors[nowColor], lightColors[nextColor], fChangeTime);
        GetComponent<Light>().color = newColor;

        transform.rotation = Quaternion.Lerp(quaternions[nowColor], quaternions[nextColor], fChangeTime);
    }
}
