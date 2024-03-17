using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetrolGeneratior : MonoBehaviour
{
    [SerializeField] private int GeneratePetrolPerTime;
    [SerializeField] private float GeneratePetrolTimer;
    //Image
    [SerializeField] private Image PetrolRigProgressBarFull;

    private float timer;


    void Update() 
    {
        if(GetComponent<Build>().enabled == false)
        {
            if(Camera.main.GetComponent<Inventory>().Oil >= GeneratePetrolPerTime)
            {
                if((timer += Time.deltaTime) >= GeneratePetrolTimer)
                {
                    Camera.main.GetComponent<Inventory>().AddOil((GeneratePetrolPerTime) * -1);
                    Camera.main.GetComponent<Inventory>().AddPetrol(GeneratePetrolPerTime);
                    timer = 0;
                }    
                else
                    PetrolRigProgressBarFull.fillAmount = timer / GeneratePetrolTimer;
                }
        }
        
    }
}
