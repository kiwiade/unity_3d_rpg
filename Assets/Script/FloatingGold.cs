using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FloatingGold : MonoBehaviour {
    public float DestroyTime = 2f;

    private float fTime = 0;
    // Use this for initialization
    void Start()
    {
        Destroy(transform.parent.gameObject, DestroyTime);

        //Color alphaimage = transform.GetComponentInChildren<Image>().color;
        //alphaimage.a = 0;
        //transform.GetComponentInChildren<Image>().DOColor(alphaimage, 2.0f);

        //Color alphacolor = transform.GetComponentInChildren<Text>().color;
        //alphacolor.a = 0;
        //transform.GetComponentInChildren<Text>().DOColor(alphacolor, 2.0f);

        //transform.DOMove(transform.position + new Vector3(0, 1, 0), 2.0f);
    }

    // Update is called once per frame
    void Update () {
        fTime += Time.deltaTime * 5.0f;
        if (fTime < 1.0f)
            transform.Translate(Vector3.up * Time.deltaTime * 5.0f);
        else
            transform.Translate(Vector3.down * Time.deltaTime * 5.0f);

        if (fTime > 2.0f)
        {
            transform.GetComponentInChildren<Image>().color = Color.clear;
            transform.GetComponentInChildren<Text>().color = Color.clear;
        }
    }
}
