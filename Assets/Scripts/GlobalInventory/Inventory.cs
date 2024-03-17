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
    [SerializeField] private Text MoneyUIText;
    [SerializeField] private Text PetrolUIText;
    [SerializeField] private Text OilUIText;

    //Add Values to resources in UI
    public void AddMoney(int value)
    {
        Money += value;
        MoneyUIText.text = Convert.ToString(Money);
    }

    public void AddPetrol(int value)
    {
        Petrol += value;
        PetrolUIText.text = Convert.ToString(Petrol);
    }

    public void AddOil(int value)
    {
        Oil += value;
        OilUIText.text = Convert.ToString(Oil);
    }

    public void Pay(int Price)
    {
        Money -= Price;
        MoneyUIText.text = Convert.ToString(Money);
    }

    
    


}
