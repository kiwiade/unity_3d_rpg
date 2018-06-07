using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Client : MonoBehaviour {

    private WebSocket mWebSocket;
    // Use this for initialization
    void Start () {
        StartConnect();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartConnect()
    {
        mWebSocket = new WebSocket("ws://localhost:4649/Chat");
        //mWebSocket = new WebSocket("ws://192.168.0.21:14649/Chat");
        mWebSocket.OnOpen += (sender, e) =>
        {
            mWebSocket.Send("Unity에서 연결");
        };
        mWebSocket.OnMessage += (sender, e) =>
        {
            //MessageBox.Show(e.Data);
            //MainWindow.mainWindow.recvMsg(e.Data);
            print(e.Data);
        };
        mWebSocket.Connect();
    }
    public void SendMessage(string msg)
    {
        mWebSocket.Send("　 : " + msg);
    }
}
