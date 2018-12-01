﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeDungeonScene : MonoBehaviour {

    public void ChangeScene()
    {
        PlayerData.Save();

        SceneManager.LoadScene("DunGeon");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            ChangeScene();
    }
}
