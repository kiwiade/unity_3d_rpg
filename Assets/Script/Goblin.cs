using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetBool("Death_b", true);
        Destroy(gameObject, 2.0f);
    }
}
