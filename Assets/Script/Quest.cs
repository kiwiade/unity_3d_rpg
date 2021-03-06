﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : Singleton<Quest>{
    public class QuestInfo
    {
        public int ID = 0;
        public string contentText = "";
        public int currentNum = 0;
        public int goalNum = 0;
    }

    private Dictionary<int, QuestInfo> questList = new Dictionary<int, QuestInfo>();

    public Dictionary<int, QuestInfo> QuestList
    {
        get { return questList; }
    }

    public Quest()
    {
        QuestInfo firstQuest = new QuestInfo();
        firstQuest.ID = 0;
        firstQuest.contentText = "슬라임 10마리 잡기";
        firstQuest.currentNum = 0;
        firstQuest.goalNum = 10;

        QuestInfo secondQuest = new QuestInfo();
        secondQuest.ID = 1;
        secondQuest.contentText = "야생토끼 5마리 잡기";
        secondQuest.currentNum = 0;
        secondQuest.goalNum = 5;

        QuestList[0] = firstQuest;
        QuestList[1] = secondQuest;
    }
}
