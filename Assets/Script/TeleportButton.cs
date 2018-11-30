using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Teleport(int id)
    {
        PlayerData.Save();

        if (id == 0)
            SceneManager.LoadScene("Dungeon");
        else if (id == 1)
            SceneManager.LoadScene("main");
        else if (id == 2)
            SceneManager.LoadScene("KekeIsland");
    }
}
