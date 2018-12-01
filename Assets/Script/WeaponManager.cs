using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] weapon;

    public GameObject makeWeapon(int type)
    {
        GameObject make = Instantiate(weapon[type]);
        return make;
    }
}
