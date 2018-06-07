using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ItemDatabase {
    public class Item
    {
        public int id = 0;
        public string name;
        public int type;
        public int atk;
        public int def;
        // ID 이름 타입 공격력 방어력
    }

    // IEnumerable<KeyValuePair<int, Item>>
    public static IEnumerable<KeyValuePair<int, Item>> items = new Dictionary<int, Item>();

    public static IEnumerable<KeyValuePair<int, Item>> ReadCsv()
    {
        string[] lines = File.ReadAllLines(Application.dataPath + "/inven.csv");
        return lines.Select(line =>
        {
            string[] data = line.Split(',');
            Item item = new Item();
            item.id = Convert.ToInt32(data[0]);
            item.name = data[1];
            item.type = Convert.ToInt32(data[2]);
            item.atk = Convert.ToInt32(data[3]);
            item.def = Convert.ToInt32(data[4]);

            return new KeyValuePair<int, Item>(item.id, item);
        });
    }
}
