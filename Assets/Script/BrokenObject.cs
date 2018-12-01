using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BrokenObject : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<MyCharacter>();
            if (player.isAttack() == true)
            {
                Destroy(gameObject, 0.3f);
            }
        }
    }
}
