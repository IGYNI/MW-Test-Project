using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float Borders;
    [SerializeField] private float Speed;


    private Vector3 Mouse;

    void Update()
    {
        Mouse = Input.mousePosition;
        //Debug.Log("X: " + Mouse.x);
        //Debug.Log("Y: " + Mouse.y);
        if (Mouse.x > Screen.width - Borders) Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Speed, Camera.main.transform.position.y, Camera.main.transform.position.z - Speed);
        if (Mouse.x < Borders) Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - Speed, Camera.main.transform.position.y, Camera.main.transform.position.z + Speed);
        if (Mouse.y > Screen.height - Borders) Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Speed / 2, Camera.main.transform.position.y, Camera.main.transform.position.z + Speed);
        if (Mouse.y < Borders) Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - Speed / 2 , Camera.main.transform.position.y, Camera.main.transform.position.z - Speed);
    }
}
