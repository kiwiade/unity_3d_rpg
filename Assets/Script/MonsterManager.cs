using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MonsterInfo
{
    public string MonsterName = "";
    public GameObject[] MonsterPrefab = null;
    public Transform respawnSpace = null;
}

public class MonsterManager : MonoBehaviour {
    public MonsterInfo[] monsterlist = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "Dungeon")
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            float monsterCount = monsters.Length;

            if (monsterCount < 1)
            {
                Instantiate(monsterlist[0].MonsterPrefab[0], monsterlist[0].respawnSpace.position, Quaternion.identity, transform);
            }
        }
        else if(SceneManager.GetActiveScene().name == "KekeIsland")
        {
            GameObject[] rabbits = GameObject.FindGameObjectsWithTag("Monster2");
            float rabbitCount = rabbits.Length;

            if(rabbitCount < 8)
            {
                int rabbitnum = Random.Range(0, monsterlist[0].MonsterPrefab.Length);
                Vector3 responPos = monsterlist[0].respawnSpace.position + new Vector3(Random.Range(0, 15.0f), 0, Random.Range(0, 20.0f));
                Instantiate(monsterlist[0].MonsterPrefab[rabbitnum], responPos, Quaternion.identity, transform);
            }


            GameObject[] slimes = GameObject.FindGameObjectsWithTag("Monster3");
            float slimeCount = slimes.Length;

            if (slimeCount < 10)
            {
                int slimenum = Random.Range(0, monsterlist[1].MonsterPrefab.Length);
                Vector3 responPos = monsterlist[1].respawnSpace.position + new Vector3(Random.Range(0, 15.0f), 0, Random.Range(0, 20.0f));
                Instantiate(monsterlist[1].MonsterPrefab[slimenum], responPos, Quaternion.identity, transform);
            }
        }
    }
}
