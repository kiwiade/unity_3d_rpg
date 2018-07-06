using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCChat : MonoBehaviour {

    public int npcID = 0;
    public int chatNum = 0;
    public int questNum = 0;
    private List<DialogCanvas.NPCChatdata> chatList = null;
    public int nowChatIndex = 0;
    private bool bShowText = false;

    private GameObject player = null;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if(chatList == null)
            {
                chatList = GameObject.FindGameObjectWithTag("DialogCanvas").GetComponent<DialogCanvas>().npcChat[chatNum];
            }


            if (bShowText == true)
            {
                GameObject dialogcanvas = GameObject.FindGameObjectWithTag("DialogCanvas");
                if (chatList.Count == nowChatIndex)
                {
                    bShowText = false;
                    nowChatIndex = 0;

                    if (chatNum == 0)
                    {
                        dialogcanvas.GetComponent<DialogCanvas>().chatDialogBackground.SetActive(false);
                        // 상점오픈
                        GameObject.FindGameObjectWithTag("ShopCanvas").GetComponent<ShopItem>().Open_PopupShop();
                    }
                }
                else
                {
                    Text text = dialogcanvas.GetComponent<DialogCanvas>().chatDialogText.GetComponent<Text>();
                    text.text = chatList[nowChatIndex].text;

                    if (chatList[nowChatIndex].Quest == 1)
                    {
                        // 퀘스트 수락 거절창 띄우기
                        dialogcanvas.GetComponent<DialogCanvas>().acceptButton.SetActive(true);
                        dialogcanvas.GetComponent<DialogCanvas>().refuseButton.SetActive(true);
                        dialogcanvas.GetComponent<DialogCanvas>().QuestNum = questNum;

                        if(questNum == 1)
                        {
                            npcID = 2;
                            chatNum = 2;
                            questNum = 0;

                            chatList = null;
                        }
                        else if(questNum == 0)
                        {
                            npcID = 1;
                            chatNum = 1;
                            questNum = 1;

                            chatList = null;
                        }
                    }
                    nowChatIndex++;
                }                
            }
            else
            {
                if (player != null)
                {
                    float distance = Vector3.Distance(player.transform.position, transform.position);
                    if (distance < 15f)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1.0f);
                        RaycastHit[] hits = Physics.RaycastAll(ray);
                        foreach (RaycastHit hit in hits)
                        {
                            if (hit.collider.gameObject == gameObject)
                            {
                                GameObject dialogcanvas = GameObject.FindGameObjectWithTag("DialogCanvas");
                                dialogcanvas.GetComponent<DialogCanvas>().chatDialogBackground.SetActive(true);

                                Text text = dialogcanvas.GetComponent<DialogCanvas>().chatDialogText.GetComponent<Text>();
                                text.text = chatList[nowChatIndex].text;
                                bShowText = true;
                                nowChatIndex++;
                            }
                        }
                    }
                }
            }
        }
	}
}
