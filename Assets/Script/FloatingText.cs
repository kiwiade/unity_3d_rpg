using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FloatingText : MonoBehaviour {
    public float DestroyTime = 3f;
	// Use this for initialization
	void Start () {
        Destroy(transform.parent.gameObject, DestroyTime);

        Color alphacolor = transform.GetComponentInChildren<Text>().color;
        alphacolor.a = 0;
        transform.GetComponentInChildren<Text>().DOColor(alphacolor, 3.0f);

        transform.DOMove(transform.position + new Vector3(0, 1, 0), 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
