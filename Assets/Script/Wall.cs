using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Wall : MonoBehaviour {

    private bool mbAlpha = false;

    public Material trans;
    public Material ogMat;

    private void UseTransparentMaterial(Transform thing)
    {
        Renderer thingsRenderer;
        thingsRenderer = thing.GetComponent<MeshRenderer>();
        thingsRenderer.material = trans;

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void UseNormalMaterial(Transform thing)
    {
        Renderer thingsRenderer;
        thingsRenderer = thing.GetComponent<Renderer>();
        thingsRenderer.sharedMaterial = ogMat;

        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    // Use this for initialization
    void Start () {
        mbAlpha = false;
        ogMat = transform.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        mbAlpha = false;
    }

    private void LateUpdate()
    {
        if (mbAlpha == false)
        {
            UseNormalMaterial(transform);
            //MeshRenderer ren = GetComponent<MeshRenderer>();
            //ren.material.DOFade(1f, 1f);
        }
    }

    public void setAlpha(bool bAlpha)
    {
        if (mbAlpha == bAlpha)
            return;
        mbAlpha = bAlpha;

        if (bAlpha == true)
        {
            if(ogMat == null)
                ogMat = transform.GetComponent<Renderer>().material;

            UseTransparentMaterial(transform);
            //MeshRenderer ren = GetComponent<MeshRenderer>();
            //ren.material.DOFade(0f, 1f);
        }
    }
}
