using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject[] weapon;

    public GameObject makeWeapon(int type)
    {
        GameObject make = Instantiate(weapon[type]);
        return make;
    }
}
