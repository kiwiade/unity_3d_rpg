using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texturetosprite : MonoBehaviour {

    public RenderTexture texture = null;
    private Sprite sp = null;

	// Use this for initialization
	void Start () {
        Texture2D tex = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
        RenderTexture.active = texture;
        tex.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        tex.Apply();
        sp = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        GetComponent<SpriteRenderer>().sprite = sp;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
