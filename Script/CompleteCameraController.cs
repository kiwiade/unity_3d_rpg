using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class CompleteCameraController : MonoBehaviour
{

    //public GameObject player;       //Public variable to store a reference to the player game object

    private Vector3 offset;         //Private variable to store the offset distance between the player and camera
    public GameObject target;
    float radius = 11f, angleX = -90f, angleY = -45f;
    private bool bClicked = false;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
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
        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue, 0.5f);

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
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        //transform.position = player.transform.position + offset;

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


        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //var HPbar = GameObject.FindGameObjectWithTag("Player").GetComponent<HPbar>().getHPbar();
            //var MPbar = GameObject.FindGameObjectWithTag("Player").GetComponent<HPbar>().getMPbar();
            //HPbar.transform.localScale = HPbar.transform.localScale + new Vector3(0.01f, 0.01f) * Input.GetAxis("Mouse ScrollWheel");
            //MPbar.transform.localScale = MPbar.transform.localScale + new Vector3(0.01f, 0.01f) * Input.GetAxis("Mouse ScrollWheel");
        }
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