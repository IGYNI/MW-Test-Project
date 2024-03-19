using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Build : MonoBehaviour
{

    private Camera GlobalCamera;

    private bool IsAbleToBePutted = true;

    [SerializeField] private Transform ItSelfTransform;
    [SerializeField] private Renderer ItSelfRenderer;

    [SerializeField] private AudioClip Sound;


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
            ItSelfTransform.position = new Vector3(Mathf.RoundToInt(BuildinPositionByRayOutOfCursor.x), ItSelfTransform.localScale.y / 2 - 1, Mathf.RoundToInt(BuildinPositionByRayOutOfCursor.z));
        }
    }

    void OnMouseDown() 
    {
        if(IsAbleToBePutted)
        {
            Camera.main.GetComponent<WorldLoad>().AddBuildingAndSave(gameObject);
            ItSelfRenderer.material.color = Color.white;
            SoundPlay();
            GetComponent<Build>().enabled = false;
        }
         
    }

    private void OnCollisionStay(Collision other) 
    {
        IsAbleToBePutted = false;
        ItSelfRenderer.material.color = Color.red; 
        
    }

    private void OnCollisionExit(Collision other) 
    {
        IsAbleToBePutted = true;
        ItSelfRenderer.material.color = Color.green; 
    }

    private void SoundPlay()
    {
        Camera.main.GetComponent<AudioSource>().clip = Sound;
        Camera.main.GetComponent<AudioSource>().Play();
    }

    
        
    



}
    
