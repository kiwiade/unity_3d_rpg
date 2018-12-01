using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeHouseScene : MonoBehaviour {

    public void ChangeScene()
    {
        PlayerData.Save();

        SceneManager.LoadScene("House");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            ChangeScene();
    }
}
