using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenItemdata {

    // 아이템이 만들어질때마다 고유의 ID를 추가로 줌. 모든 아이템이 똑같지는 않으니까
    private int itemId = 0;
    private int uniqueId = 0;

    public int ItemId
    {
        get
        {
            return itemId;
        }

        set
        {
            itemId = value;
        }
    }

    public int UniqueId
    {
        get
        {
            return uniqueId;
        }

        set
        {
            uniqueId = value;
        }
    }
}
