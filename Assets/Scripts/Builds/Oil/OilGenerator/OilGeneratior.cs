using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilGeneratior : MonoBehaviour
{
    //Patameters of Generator
    [SerializeField] private int GenerateOilPerTime;
    [SerializeField] private float GenerateOilTimer;
    //Image
    [SerializeField] private Image OilRigProgressBarFull;

    private float timer;


    void Update() 
    {
        if(GetComponent<Build>().enabled == false)
        {
            if((timer += Time.deltaTime) >= GenerateOilTimer)
            {
                Camera.main.GetComponent<Inventory>().AddOil(GenerateOilPerTime);
                timer = 0;
            }    
            else
                OilRigProgressBarFull.fillAmount = timer / GenerateOilTimer;
        }
        
    }
    
}
