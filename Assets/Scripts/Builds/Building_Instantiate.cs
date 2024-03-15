using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Instantiate : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToCreate;
    public void CreateObject()
    {
        Instantiate(ObjectToCreate, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
