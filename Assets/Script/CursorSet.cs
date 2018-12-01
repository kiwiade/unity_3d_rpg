using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSet : MonoBehaviour {

    [SerializeField]
    private Texture2D defaultCursor;
    [SerializeField]
    private Texture2D chatCursor;
    private readonly CursorMode curMode = CursorMode.Auto;
    private readonly Vector2 hotSpot = Vector2.zero;

    private GameObject player = null;
    // Use this for initialization
    void Start () {
        Cursor.SetCursor(defaultCursor, hotSpot, curMode);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseEnter()
    {
        if(gameObject.tag == "NPC")
        {
            if (player != null)
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance < 15f)
                {
                    Cursor.SetCursor(chatCursor, hotSpot, curMode);
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (gameObject.tag == "NPC")
        {
            if (player != null)
            {
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance < 15f)
                {
                    Cursor.SetCursor(chatCursor, hotSpot, curMode);
                }
            }
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, curMode);
    }
}
