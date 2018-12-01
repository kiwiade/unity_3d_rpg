using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCanvas : MonoBehaviour {

    [SerializeField]
    private GameObject QuestBackground;
    [SerializeField]
    private Text content;
    [SerializeField]
    private Text content2;

    // Use this for initialization
    void Start () {
        Invoke("readQuest", 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            if(QuestBackground.activeSelf == false)
            {
                QuestBackground.SetActive(true);
            }
            else
            {
                QuestBackground.SetActive(false);
            }
        }
	}

    public void addQuest(int num)
    {
        foreach(int i in PlayerData.myquestNum)
        {
            if (i == num)
            {
                return;
            }
        }
        PlayerData.myquestNum.Add(num);

        readQuest();
    }

    public void readQuest()
    {
        content.text = "";
        content2.text = "";
        foreach (int questnum in PlayerData.myquestNum)
        {
            content.text += Quest.Instance.QuestList[questnum].contentText + "\n";
            content2.text += Quest.Instance.QuestList[questnum].currentNum.ToString() + " / "
                + Quest.Instance.QuestList[questnum].goalNum.ToString() + "\n";
        }
    }
}
