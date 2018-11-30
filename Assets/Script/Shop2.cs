using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop2 : MonoBehaviour {

    public GameObject shopcard;
    public GameObject scrollviewContent;

	// Use this for initialization
	void Start () {
        var shopCanvasScript = GameObject.FindGameObjectWithTag("ShopCanvas").GetComponent<ShopItem>();
        var shopList = shopCanvasScript.mShopList;
        for (int i = 0; i < shopList.Count; i++)
        {
            var card = Instantiate(shopcard, scrollviewContent.transform);

            int x = (i % 2 == 0) ? -160 : 160;
            card.transform.localPosition = new Vector3(x, -60 -(i / 2) * 200);

            var cardScript = card.GetComponent<ShopCard>();
            cardScript.icon.sprite = shopCanvasScript.itemicon[i];
            cardScript.title.text = shopList[i + 1].itemName;
            cardScript.price.text = shopList[i + 1].cost.ToString();
            cardScript.itemID = shopList[i + 1].itemID;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
