using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpdate : MonoBehaviour {
    public ProgressBar2 stat_HPbar;
    public ProgressBar2 stat_MPbar;
    public ProgressBar2 stat_EXPbar;

    [Space]
    public Text playerLV;
    public Text HP_Text;
    public Text MP_Text;
    public Text EXP_Text;

    [Space]
    public Text ATK_Text;
    public Text DEF_Text;
    public Text SPD_Text;
    public Text Gold_Text;
    // Use this for initialization
    void Start () {
        
    }

    private void OnEnable()
    {
        statUpdate();
    }

    // Update is called once per frame
    void Update () {
        statUpdate();
    }

    public void statUpdate()
    {
        playerLV.text = PlayerData.getLevel().ToString();

        stat_HPbar.currentPercent = PlayerData.getHPpercent();
        stat_MPbar.currentPercent = PlayerData.getMPpercent();
        stat_EXPbar.currentPercent = PlayerData.getExpPercent();

        HP_Text.text = PlayerData.getHP().ToString() + " / " + PlayerData.getMaxHP().ToString();
        MP_Text.text = PlayerData.getMP().ToString() + " / " + PlayerData.getMaxMP().ToString();
        EXP_Text.text = PlayerData.getEXP().ToString() + " / " + PlayerData.getRequireEXP().ToString();

        ATK_Text.text = PlayerData.getATK().ToString();
        DEF_Text.text = PlayerData.getDEF().ToString();
        SPD_Text.text = PlayerData.getSPD().ToString();
        Gold_Text.text = PlayerData.getMoney().ToString();
    }
}
