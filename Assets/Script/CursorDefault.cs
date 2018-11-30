using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDefault : MonoBehaviour
{
    public Texture2D defaultCursor;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotSpot = new Vector2(30, 0);

    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, curMode);       
    }
}
