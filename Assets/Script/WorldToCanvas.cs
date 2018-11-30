using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldToCanvas{

	public static Vector2 toCanvas(GameObject canvas, Vector3 worldPos)
    {
        Camera cam = Camera.main;
        var viewPos = cam.WorldToViewportPoint(worldPos);
        var canRect = canvas.GetComponent<RectTransform>();

        Vector2 canvasPos = new Vector2(viewPos.x * canRect.sizeDelta.x, viewPos.y * canRect.sizeDelta.y);
        //Vector2 canvasPos = new Vector2(viewPos.x, viewPos.y);

        return canvasPos;
    }
}
