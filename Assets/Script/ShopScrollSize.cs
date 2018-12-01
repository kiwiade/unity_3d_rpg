using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScrollSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var shopCanvasScript = GameObject.FindGameObjectWithTag("ShopCanvas").GetComponent<ShopItem>();
        var shopList = shopCanvasScript.mShopList;

        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, (Mathf.CeilToInt((float)shopList.Count/2.0f)) * 210.0f);
    }
}
