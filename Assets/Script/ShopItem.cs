using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    public class ShopItemData
    {
        public int itemID = 0;
        public string itemName = "";
        public ItemCategory itemCategory;
        public int cost = 0;
    }
    public class ShopItemData2 : MonoBehaviour
    {
        public int itemID = 0;
        public string itemName = "";
        public ItemCategory itemCategory;
        public int cost = 0;
    }
    public enum ItemCategory
    {
        Weapon,
        Armor,
        Potion
    }

    public Dictionary<int, ShopItemData> mShopList = new Dictionary<int, ShopItemData>();

    [SerializeField]
    private GameObject shopback;
    [SerializeField]
    private GameObject tooltip;
    [SerializeField]
    private Image SelectImage;
    [SerializeField]
    private Sprite[] itemicon;

    [Space]
    [SerializeField]
    private GameObject shopback2;
    [SerializeField]
    private GameObject Shopcard;
    [SerializeField]
    private GameObject CompleteWindow;
    [SerializeField]
    private GameObject FailWindow;

    [Space]
    [SerializeField]
    private GameObject mainInventory;

    private int selectItemID = 0;

    private void Awake()
    {
        LoadShopItemList();
    }

    // Use this for initialization
    void Start () {
        Shop_Load(itemicon);
	}
	
	// Update is called once per frame
	void Update () {
        if (tooltip.activeSelf == true)
        {
            tooltip.transform.position = Input.mousePosition + new Vector3(100, 80);
        }
    }

    public void Open_TooltipShop()
    {
        if (shopback.activeSelf == true)
        {
            shopback.SetActive(false);
            tooltip.SetActive(false);
            mainInventory.SetActive(false);
        }
        else
        {
            shopback.SetActive(true);
            mainInventory.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().Gooooold.text = PlayerData.getMoney().ToString();
        }
    }

    public void Open_PopupShop()
    {
        if (shopback2.activeSelf == true)
        {
            shopback2.SetActive(false);
            mainInventory.SetActive(false);
        }
        else
        {
            shopback2.SetActive(true);
            mainInventory.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().Gooooold.text = PlayerData.getMoney().ToString();
        }
    }

    public void Shop_Load(Sprite[] sp)
    {
        for (int i = 0; i < itemicon.Length; i++)
        {
            GameObject item = new GameObject();
            item.AddComponent<RectTransform>();
            Image itemImage = item.AddComponent<Image>();
            itemImage.sprite = sp[i];

            var trigger = item.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => {
                OnPointerEnterDelegate((PointerEventData)data);
                });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => {
                OnPointerExitDelegate((PointerEventData)data);
            });
            trigger.triggers.Add(entry);

            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => {
                OnPointerClickDelegate((PointerEventData)data);
            });
            trigger.triggers.Add(entry);

            item.transform.SetParent(shopback.transform);

            int x = (i%2 == 0) ? -70 : 70;
            item.transform.localPosition = new Vector3(x, 130 - (i/2)*130);

            var itemData = item.AddComponent<ShopItemData2>();
            itemData.itemID = mShopList[i + 1].itemID;
            itemData.cost = mShopList[i + 1].cost;
            itemData.itemName = mShopList[i + 1].itemName;
            itemData.itemCategory = mShopList[i + 1].itemCategory;

            item.name = mShopList[i+1].itemName;
        }
    }
    public void OnPointerEnterDelegate(PointerEventData data)
    {
        string name = data.pointerCurrentRaycast.gameObject.name;
        Sprite img = data.pointerCurrentRaycast.gameObject.GetComponent<Image>().sprite;

        var tip = tooltip.GetComponent<ItemTooltip>();
        tip.itemname.text = name;
        tip.thumbnail.sprite = img;
        tip.price.text = data.pointerCurrentRaycast.gameObject.GetComponent<ShopItemData2>().cost.ToString();

        tooltip.SetActive(true);

        print("들어옴");
    }
    public void OnPointerExitDelegate(PointerEventData data)
    {
        tooltip.SetActive(false);
        print("나감");
    }
    public void OnPointerClickDelegate(PointerEventData data)
    {
        Vector3 imagePos = data.pointerCurrentRaycast.gameObject.transform.position;
        SelectImage.transform.position = imagePos;
        var gameobj = data.pointerCurrentRaycast.gameObject;
        var shopData = gameobj.GetComponent<ShopItemData2>();
        selectItemID = shopData.itemID;
    }

    public void OnBuyItem()
    {
        if (selectItemID != 0 && selectItemID <= mShopList.Count)
        {
            if (PlayerData.getMoney() >= mShopList[selectItemID].cost)
            {
                PlayerData.MoneyMinus(mShopList[selectItemID].cost);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().Gooooold.text = PlayerData.getMoney().ToString();
                GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().addItemToInventory(110 + selectItemID);

                print("구매가 완료되었습니다. 현재골드 : " + PlayerData.getMoney() + ", 아이템의 가격 : " + mShopList[selectItemID].cost);
            }
            else
            {
                print("못삼. 현재골드 : " + PlayerData.getMoney() + ", 아이템의 가격 : " + mShopList[selectItemID].cost);
            }
        }
    }

    public void CloseButton()
    {
        shopback.SetActive(false);
        tooltip.SetActive(false);
        shopback2.SetActive(false);
        mainInventory.SetActive(false);
    }

    public void LoadShopItemList()
    {
        string dataPath = Application.dataPath;
        dataPath = Path.Combine(dataPath, "ShopList.csv");

        if(File.Exists(dataPath) == false)
            return;

        FileStream fStream = new FileStream(dataPath, FileMode.Open);
        if (fStream != null)
        {
            StreamReader streamReader = new StreamReader(fStream);
            string list = streamReader.ReadToEnd();

            string[] lines = list.Split("\r\n".ToCharArray());


            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    string[] data = line.Split(',');

                    ShopItemData iData = new ShopItemData();
                    iData.itemID = int.Parse(data[0]);
                    iData.itemName = data[1];
                    int category = int.Parse(data[2]);
                    if (category == 0)
                        iData.itemCategory = ItemCategory.Weapon;
                    else if (category == 1)
                        iData.itemCategory = ItemCategory.Armor;
                    else if (category == 2)
                        iData.itemCategory = ItemCategory.Potion;
                    iData.cost = int.Parse(data[3]);

                    mShopList[iData.itemID] = iData;
                }
            }

            streamReader.Close();
            fStream.Close();
        }
    }
}
