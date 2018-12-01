using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDefault : MonoBehaviour
{
    [SerializeField]
    private Texture2D defaultCursor;
    private readonly CursorMode curMode = CursorMode.Auto;
    [SerializeField]
    private readonly Vector2 hotSpot = new Vector2(30, 0);

    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, curMode);       
    }
}
