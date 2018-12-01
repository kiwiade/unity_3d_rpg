using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour {

    [SerializeField]
    private Image icon = null;
    public Text title = null;
    public Text price = null;
    public int itemID = 0;

    public void BuyButton()
    {
        var shopCanvasScript = GameObject.FindGameObjectWithTag("ShopCanvas").GetComponent<ShopItem>();
        var shopList = shopCanvasScript.mShopList;

        if (itemID != 0 && itemID <= shopList.Count)
        {
            if (PlayerData.getMoney() >= shopList[itemID].cost)
            {
                PlayerData.MoneyMinus(shopList[itemID].cost);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().Gooooold.text = PlayerData.getMoney().ToString();
                GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().addItemToInventory(120 + itemID);

                shopCanvasScript.CompleteWindow.SetActive(true);
                print("구매가 완료되었습니다. 현재골드 : " + PlayerData.getMoney() + ", 아이템의 가격 : " + shopList[itemID].cost);
            }
            else
            {
                shopCanvasScript.FailWindow.SetActive(true);
                print("못삼. 현재골드 : " + PlayerData.getMoney() + ", 아이템의 가격 : " + shopList[itemID].cost);
            }
        }
    }
}
