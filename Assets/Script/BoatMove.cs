using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoatMove : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * 15.0f);

        if (transform.position.x > -2)
        {
            if(SceneData.getSceneName() == "KekeIsland")
                SceneManager.LoadScene("main");
            else if (SceneData.getSceneName() == "main")
                SceneManager.LoadScene("KekeIsland");
        }
    }
}
