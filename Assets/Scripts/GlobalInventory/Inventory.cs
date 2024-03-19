using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Amount of resources 
    public int Money;
    public int Petrol;
    public int Oil;
    //Text to show resources
    [SerializeField] public Text MoneyUIText;
    [SerializeField] public Text PetrolUIText;
    [SerializeField] public Text OilUIText;

    
    public void AddMoney(int value)
    {
        Money += value;
        MoneyUIText.text = Convert.ToString(Money);
        GetComponent<WorldLoad>().SaveInventory();
    }

    public void AddPetrol(int value)
    {
        Petrol += value;
        PetrolUIText.text = Convert.ToString(Petrol);
        GetComponent<WorldLoad>().SaveInventory();
    }

    public void AddOil(int value)
    {
        Oil += value;
        OilUIText.text = Convert.ToString(Oil);
        GetComponent<WorldLoad>().SaveInventory();
    }

    public void Pay(int Price)
    {
        Money -= Price;
        MoneyUIText.text = Convert.ToString(Money);
        GetComponent<WorldLoad>().SaveInventory();
    }

    
    


}
