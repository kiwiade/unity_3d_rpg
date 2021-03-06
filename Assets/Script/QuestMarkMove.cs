﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarkMove : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);

        Quaternion currentRot = transform.rotation;
        Vector3 currentEuler = currentRot.eulerAngles;
        currentEuler.x = 0;

        Quaternion newRot = new Quaternion();
        newRot.eulerAngles = currentEuler;
        transform.rotation = newRot;
    }
}
