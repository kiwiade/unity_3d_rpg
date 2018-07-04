using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using System.IO;

public static class PlayerData {
    
    private static int playerHP = 0;
    private static int playerMaxHP = 100;
    private static int playerMP = 0;
    private static int playerMaxMP = 100;

    private static int playerLevel = 1;
    private static int playerExp = 0;

    private static int requireExp = 1000;
    private static int playerAtk = 1;
    private static int playerDef = 2;
    private static int playerSpeed = 10;

    private static int Money = 50;

    private static int[] requireExpData = new int[11];

    public static int D_Spell = 10;
    public static float D_SpellTime = 0;
    public static int F_Spell = 10;
    public static float F_SpellTime = 0;

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

        if (playerHP == 0 && playerMP == 0)
        {
            playerHP = playerMaxHP;
            playerMP = playerMaxMP;
        }
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

    public static float getHP()
    {
        return playerHP;
    }

    public static float getMaxHP()
    {
        return playerMaxHP;
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

    public static float getMaxMP()
    {
        return playerMaxMP;
    }

    // EXP

    public static void ExpPlus(int plus)
    {
        playerExp += plus;
        if (playerExp >= requireExp)
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

        // 업했으니 hp, mp 풀로
        playerHP = playerMaxHP;
        playerMP = playerMaxMP;
    }


    public static float getEXP()
    {
        return playerExp;
    }

    public static float getRequireEXP()
    {
        return requireExp;
    }

    public static int getLevel()
    {
        return playerLevel;
    }

    public static float getExpPercent()
    {
        return ((float)playerExp / requireExp) * 100;
    }

    public static int getMoney()
    {
        return Money;
    }

    public static void MoneyPlus(int plus)
    {
        Money += plus;
    }

    public static void MoneyMinus(int minus)
    {
        if (Money >= minus)
            Money -= minus;
    }


    public static float getATK()
    {
        return playerAtk;
    }

    public static float getDEF()
    {
        return playerDef;
    }

    public static float getSPD()
    {
        return playerSpeed;
    }


    // save, load
    public static void Save()
    {
        JObject root = new JObject();
        JObject saveData = new JObject();

        saveData.Add("Level", playerLevel.ToString());
        saveData.Add("Exp", playerExp.ToString());
        saveData.Add("HP", playerHP.ToString());
        saveData.Add("MP", playerMP.ToString());
        saveData.Add("Money", Money.ToString());

        saveData.Add("D_Spell", D_Spell.ToString());
        saveData.Add("D_SpellTime", D_SpellTime.ToString());
        saveData.Add("F_Spell", F_Spell.ToString());
        saveData.Add("F_SpellTime", F_SpellTime.ToString());
        root.Add("SaveData", saveData);

        string savePath = Path.Combine(Application.dataPath, "GameData.json");
        File.WriteAllText(savePath, root.ToString());
    }

    public static void Load()
    {
        string savePath = Path.Combine(Application.dataPath, "GameData.json");
        if (File.Exists(savePath))
        {
            StreamReader read = File.OpenText(savePath);
            string text = read.ReadToEnd();
            JObject root = JObject.Parse(text);
            JObject option = root["SaveData"] as JObject;
            string strLevel = option["Level"].ToString();
            string strExp = option["Exp"].ToString();
            string strHP = option["HP"].ToString();
            string strMP = option["MP"].ToString();

            string strDSpell = option["D_Spell"].ToString();
            string strDSpellTime = option["D_SpellTime"].ToString();
            string strFSpell = option["F_Spell"].ToString();
            string strFSpellTime = option["F_SpellTime"].ToString();

            if (option["Money"] != null)
            {
                string strMoney = option["Money"].ToString();
                Money = int.Parse(strMoney);
            }
            else
                Money = 50;

            playerLevel = int.Parse(strLevel);
            playerExp = int.Parse(strExp);
            playerHP = int.Parse(strHP);
            playerMP = int.Parse(strMP);

            D_Spell = int.Parse(strDSpell);
            D_SpellTime = float.Parse(strDSpellTime);
            F_Spell = int.Parse(strFSpell);
            F_SpellTime = float.Parse(strFSpellTime);

            LevelUpData.levels = LevelUpData.ReadCsv();
            statSetting();

            spellSetting();

            read.Close();
        }
    }

    private static void spellSetting()
    {
        var spellScript = GameObject.FindGameObjectWithTag("SpellCanvas").GetComponent<Spell>();

        spellScript.D_ID = D_Spell;
        spellScript.F_ID = F_Spell;
        spellScript.DTime = D_SpellTime;
        spellScript.FTime = F_SpellTime;

        int skillpoint = 0;
        if (D_Spell == 10)
            skillpoint++;
        if (F_Spell == 10)
            skillpoint++;

        spellScript.skillpoint = skillpoint;
        spellScript.SkillPointText.text = skillpoint.ToString();


        if (D_Spell != 10)
        {
            spellScript.D_Image.GetComponent<Image>().sprite = spellScript.Skill[D_Spell].transform.GetChild(0).GetComponent<Image>().sprite;
            spellScript.D_Image.GetComponent<Image>().color = Color.white;

            spellScript.Skill[D_Spell].GetComponent<Outline>().effectColor = Color.red;
            spellScript.Skill[D_Spell].GetComponent<Outline>().effectDistance = new Vector2(2, 2);
            spellScript.Skill[D_Spell].transform.GetChild(2).gameObject.SetActive(false);
            spellScript.Skill[D_Spell].transform.GetChild(3).gameObject.SetActive(true);
        }
        if (F_Spell != 10)
        {
            spellScript.F_Image.GetComponent<Image>().sprite = spellScript.Skill[F_Spell].transform.GetChild(0).GetComponent<Image>().sprite;
            spellScript.F_Image.GetComponent<Image>().color = Color.white;

            spellScript.Skill[F_Spell].GetComponent<Outline>().effectColor = Color.red;
            spellScript.Skill[F_Spell].GetComponent<Outline>().effectDistance = new Vector2(2, 2);
            spellScript.Skill[F_Spell].transform.GetChild(2).gameObject.SetActive(false);
            spellScript.Skill[F_Spell].transform.GetChild(3).gameObject.SetActive(true);
        }

        if(D_SpellTime > 0)
        {
            spellScript.D_CooldownImage.SetActive(true);
            spellScript.D_CooldownText.SetActive(true);
        }
        if(F_SpellTime > 0)
        {
            spellScript.F_CooldownImage.SetActive(true);
            spellScript.F_CooldownText.SetActive(true);
        }
    }
}
