using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData{
    private readonly static Dictionary<int, InvenItemdata> items = new Dictionary<int, InvenItemdata>();

    // 인벤토리에 쌓일 아이템의 개수, 위치 등을 저장하는 클래스
    public class InvenItemValue
    {
        public int count = 0;
        public int position = 0;
        public InvenItemdata itemData = new InvenItemdata();
    }

    public readonly static List<InvenItemValue> invenItems = new List<InvenItemValue>();
}