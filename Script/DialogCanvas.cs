using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogCanvas : MonoBehaviour {

    public class NPCChatdata
    {
        public int ID = 0;
        public int ChatNum = 0;
        public string text = "";
        public int Quest = 0;
    }

    public GameObject chatDialogBackground;
    public GameObject chatDialogText;
    public GameObject acceptButton;
    public GameObject refuseButton;

    [HideInInspector]
    public int QuestNum = 0;

    public Dictionary<int, List<NPCChatdata>> npcChat = new Dictionary<int, List<NPCChatdata>>();
    // Use this for initialization
    void Start () {
        readNPCText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 해상도가 바뀔때마다 불림
    private void OnGUI()
    {
        // 캔버스의 크기를 받아옴
        var canvasRect = GetComponent<RectTransform>();

        // dialog의 시작위치는 0,0으로함. 그전에 미리 pivot을 0,0으로 설정해줘야함. 
        Vector2 dialogPos = new Vector2(0, 0);
        chatDialogBackground.transform.position = dialogPos;

        // dialog의 크기는 x는 캔버스의 크기만큼, y는 1/3만큼 차도록함.
        Vector2 dialogSize = new Vector2(canvasRect.sizeDelta.x * 1.0f, canvasRect.sizeDelta.y * 0.333f);
        var dialogRect = chatDialogBackground.GetComponent<RectTransform>();
        dialogRect.sizeDelta = dialogSize;


        // dialog text의 사이즈를 맞춰줌. text는 pivot을 0.5, 0.5로 하여 가운데 중심으로 줄어들도록
        var chatTextTrans = chatDialogText.GetComponent<RectTransform>();
        Vector2 textSize = new Vector2(dialogRect.sizeDelta.x * 0.9f, dialogRect.sizeDelta.y * 0.9f);
        chatTextTrans.sizeDelta = textSize;
    }

    public void readNPCText()
    {
        string fileName = Application.dataPath;
        fileName = Path.Combine(fileName, "NPC_Dialog/Dialog.csv");
        if (File.Exists(fileName) == false)
            return;

        List<NPCChatdata> npcDataList = new List<NPCChatdata>();
        int nowChatID = 0;

        FileStream fStream = new FileStream(fileName, FileMode.Open);
        if(fStream != null)
        {
            StreamReader streamReader = new StreamReader(fStream);
            string npcText = streamReader.ReadToEnd();
            string[] lines = npcText.Split("\r\n".ToCharArray());

            foreach(string line in lines)
            {
                if (line.Length > 0)
                {
                    string[] data = line.Split(',');

                    NPCChatdata chatData = new NPCChatdata();
                    chatData.ID = int.Parse(data[0]);
                    chatData.ChatNum = int.Parse(data[1]);
                    chatData.text = data[2];
                    chatData.Quest = int.Parse(data[3]);

                    if (nowChatID != chatData.ChatNum)
                    {
                        npcChat[nowChatID] = npcDataList;
                        nowChatID = chatData.ChatNum;
                        npcDataList = new List<NPCChatdata>();
                    }
                    npcDataList.Add(chatData);
                }
            }
            npcChat[nowChatID] = npcDataList;

            streamReader.Close();
            fStream.Close();
        }
    }


    public void Accept()
    {
        acceptButton.SetActive(false);
        refuseButton.SetActive(false);
        chatDialogBackground.SetActive(false);
        GameObject.FindGameObjectWithTag("QuestCanvas").GetComponent<QuestCanvas>().addQuest(QuestNum);
    }

    public void Refuse()
    {
        acceptButton.SetActive(false);
        refuseButton.SetActive(false);
        chatDialogBackground.SetActive(false);
    }
}
