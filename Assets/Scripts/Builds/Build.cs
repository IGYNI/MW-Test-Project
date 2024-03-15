using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{

    private Camera GlobalCamera;

    private bool IsAbleToBePutted = true;

    [SerializeField] private Transform ItSelfTransform;
    [SerializeField] private Renderer ItSelfRenderer;


    void Start()
    {
        ItSelfRenderer.material.color = Color.green; 
        GlobalCamera = Camera.main;
    }

    
    void Update()
    {
        var GroundPlane = new Plane(Vector3.up, Vector3.zero);
        var Ray = GlobalCamera.ScreenPointToRay(Input.mousePosition);
        if(GroundPlane.Raycast(Ray, out float pos))
        {
            Vector3 BuildinPositionByRayOutOfCursor = Ray.GetPoint(pos);
            ItSelfTransform.position = new Vector3(Mathf.RoundToInt(BuildinPositionByRayOutOfCursor.x), ItSelfTransform.localScale.y / 2, Mathf.RoundToInt(BuildinPositionByRayOutOfCursor.z));
        }
    }

    void OnMouseDown() 
    {
        if(IsAbleToBePutted)
        {
            GetComponent<Build>().enabled = false;
            ItSelfRenderer.material.color = Color.white;
        }
         
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(!GetComponent<Build>().enabled)
        {
            IsAbleToBePutted = false;
            ItSelfRenderer.material.color = Color.red; 
        }
        
    }

    private void OnCollisionExit(Collision other) 
    {
        if(!GetComponent<Build>().enabled)
        {
            IsAbleToBePutted = true;
            ItSelfRenderer.material.color = Color.green; 
        }
    }




}
    
