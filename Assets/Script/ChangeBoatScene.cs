using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeBoatScene : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerData.Save();
            SceneData.setPrevScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("MoveIsland");
        }
    }
}
