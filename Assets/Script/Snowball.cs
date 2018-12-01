using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour {

    [SerializeField]
    private Vector3 moveDir = Vector3.zero;

    public Vector3 MoveDir
    {
        get
        {
            return moveDir;
        }

        set
        {
            moveDir = value;
        }
    }

    // Update is called once per frame
    void Update () {
        transform.position += MoveDir;
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
