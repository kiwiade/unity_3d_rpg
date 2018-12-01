using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var ani = GetComponent<Animator>();
        ani.SetInteger("WeaponType_int", 0);
        ani.SetInteger("MeleeType_int", 1);
    }
}
