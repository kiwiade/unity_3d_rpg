using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpdate : MonoBehaviour {
    [SerializeField]
    private ProgressBar2 stat_HPbar;
    [SerializeField]
    private ProgressBar2 stat_MPbar;
    [SerializeField]
    private ProgressBar2 stat_EXPbar;

    [Space]
    [SerializeField]
    private Text playerLV;
    [SerializeField]
    private Text HP_Text;
    [SerializeField]
    private Text MP_Text;
    [SerializeField]
    private Text EXP_Text;

    [Space]
    [SerializeField]
    private Text ATK_Text;
    [SerializeField]
    private Text DEF_Text;
    [SerializeField]
    private Text SPD_Text;
    [SerializeField]
    private Text Gold_Text;

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
