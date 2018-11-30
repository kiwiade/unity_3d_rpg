using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Save : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void saveButton()
    {
        PlayerData.Save();
    }

    public void loadButton()
    {
        PlayerData.Load();
    }
}
