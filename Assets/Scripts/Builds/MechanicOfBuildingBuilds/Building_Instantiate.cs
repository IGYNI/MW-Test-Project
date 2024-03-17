using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Instantiate : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToCreate;
    [SerializeField] private int Price;
    public void CreateObject()
    {
        if(Camera.main.GetComponent<Inventory>().Money >= Price)
        {
            Camera.main.GetComponent<Inventory>().Pay(Price);
            Instantiate(ObjectToCreate, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
    }
}
