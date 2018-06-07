using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour {

    public GameObject monster = null;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float monsterCount = monsters.Length;

        if (monsterCount < 1)
        {
            Instantiate(monster);
        }
    }
}
