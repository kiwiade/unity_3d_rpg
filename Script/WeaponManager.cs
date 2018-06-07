using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject[] weapon = new GameObject[5];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject makeWeapon(int type)
    {
        GameObject make = Instantiate(weapon[type]);
        return make;
    }
}
