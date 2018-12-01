using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class CompleteCameraController : MonoBehaviour
{
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    public GameObject target;
    float radius = 11f, angleX = -90f, angleY = -45f;
    private bool bClicked = false;

    // Use this for initialization
    void Start()
    {
        TitleScene.LoadOption();
        PlayerData.Load();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            bClicked = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            bClicked = false;
        }

        RaycastHit[] hits;
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(ScreenPos);

        hits = Physics.RaycastAll(ray, Vector3.Distance(transform.position, target.transform.position));
        foreach(RaycastHit hit in hits)
        {
            if(hit.transform.tag == "Wall")
            {
                var wall = hit.transform.GetComponent<Wall>();
                wall.setAlpha(true);
            }
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        radius -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 100f;

        if (bClicked == true)
        {
            angleX -= Input.GetAxis("Mouse X") * Time.deltaTime * 2.0f;
            angleY -= Input.GetAxis("Mouse Y") * Time.deltaTime * 2.0f;
        }

        float x = radius * Mathf.Cos(angleX) * Mathf.Sin(angleY);
        float z = radius * Mathf.Sin(angleX) * Mathf.Sin(angleY);
        float y = radius * Mathf.Cos(angleY);
        transform.position = new Vector3(x + target.transform.position.x,
                                         y + target.transform.position.y,
                                         z + target.transform.position.z);
        transform.LookAt(target.transform.position);
    }

    public void CameraLeft()
    {
        angleX -= 0.02f;
    }

    public void CameraRight()
    {
        angleX += 0.02f;
    }
}