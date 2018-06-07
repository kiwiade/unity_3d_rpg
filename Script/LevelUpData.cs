using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelUpData{

    public class LevelUp
    {
        public int level = 0;
        public int exp = 0;
        public int atk = 0;
        public int def = 0;
        public int hp = 0;
        //Level Exp Atk Def HP
    }
    // foreach로 돌아가지는 모든것들 list, stack, queue 이런것들의 상위. 
    // 코루틴처럼 return을 만나도 끝이안나고 모든 함수가 끝나야 끝남
    public static IEnumerable<LevelUp> levels = new List<LevelUp>();
	
    public static IEnumerable<LevelUp> ReadCsv()
    {
        string[] lines = File.ReadAllLines(Application.dataPath + "/LevelUp.csv");
        return lines.Select(line =>
        {
            string[] data = line.Split(',');
            LevelUp up = new LevelUp();
            up.level = Convert.ToInt32(data[0]);
            up.exp = Convert.ToInt32(data[1]); 
            up.atk = Convert.ToInt32(data[2]);
            up.def = Convert.ToInt32(data[3]);
            up.hp = Convert.ToInt32(data[4]);
            return up;
        });
    }
}
