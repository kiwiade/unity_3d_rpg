using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Save : MonoBehaviour
{
    public void saveButton()
    {
        PlayerData.Save();
    }

    public void loadButton()
    {
        PlayerData.Load();
    }
}
