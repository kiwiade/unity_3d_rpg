using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerData {
    
    private static int playerHP = 100;
    private static int playerMaxHP = 100;
    private static int playerMP = 100;
    private static int playerMaxMP = 100;
    private static int playerLevel = 1;
    private static int playerExp = 0;
    private static int requireExp = 1000;
    private static int playerAtk = 1;
    private static int playerDef = 2;

    private static int[] requireExpData = new int[11];
    // 스탯세팅
    static PlayerData()
    {
        LevelUpData.levels = LevelUpData.ReadCsv();
        //IEnumerable<LevelUpData.LevelUp> levels = LevelUpData.ReadCsv();
        statSetting();
    }

    private static void statSetting()
    {
        List<LevelUpData.LevelUp> lv = LevelUpData.levels.ToList();

        requireExp = lv[playerLevel - 1].exp;
        playerAtk = lv[playerLevel - 1].atk;
        playerDef = lv[playerLevel - 1].def;
        playerMaxHP = lv[playerLevel - 1].hp;

        playerHP = playerMaxHP;
        playerMP = playerMaxMP;
    }

    // HP
    public static void HPminus(int minus)
    {
        if (playerHP > 0)
            playerHP -= minus;

        if (playerHP < 0)
        {
            // 플레이어 사망
        }
    }

    public static void HPplus(int plus)
    {
        playerHP += plus;
        if (playerHP > playerMaxHP)
            playerHP = playerMaxHP;
    }

    public static float getHPpercent()
    {
        return ((float)playerHP / playerMaxHP) * 100;
    }

    // MP
    public static void MPminus(int minus)
    {
        if (playerMP > 0)
            playerMP -= minus;

        if (playerMP < 0)
            playerMP = 0;
    }

    public static void MPplus(int plus)
    {
        playerMP += plus;
        if (playerMP > playerMaxMP)
            playerMP = playerMaxMP;
    }

    public static float getMPpercent()
    {
        return ((float)playerMP / playerMaxMP) * 100;
    }

    public static float getMP()
    {
        return playerMP;
    }

    // EXP

    public static void ExpPlus(int plus)
    {
        playerExp += plus;
        if (playerExp > requireExp)
            LevelUp(playerExp - requireExp);
    }

    public static void LevelUp(int remainExp)
    {
        // 레벨업
        if(playerLevel < 10)
            playerLevel++;

        // 캔버스에 레벨수치 올림
        GameObject.FindGameObjectWithTag("PlayerHPMP").transform.GetChild(4).GetComponent<Text>().text = playerLevel.ToString();

        // 레벨업 이펙트 넣기

        // 업하고 초과한 경험치 넣어줌
        playerExp = remainExp;
        statSetting();
    }

    public static int getLevel()
    {
        return playerLevel;
    }

    public static float getExpPercent()
    {
        return ((float)playerExp / requireExp) * 100;
    }
}
