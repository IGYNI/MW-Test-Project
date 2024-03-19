using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankSystem : MonoBehaviour
{
    //Values
    [SerializeField] private int ToExchangePetrol;
    [SerializeField] private int ToExchangeMoney;
    [SerializeField] private float ToExchangeTime;
    [SerializeField] private int RandomRangeStart;
    [SerializeField] private int RandomRangeEnd;
    //UI
    [SerializeField] private Image MoneyIconTimer;
    [SerializeField] private Text MoneySubmitText;
    [SerializeField] private Text MoneyMultiplierText;

    private float Multiplier;
    private float timer;

    private float faketimer;

    private void Awake() 
    {
        faketimer = ToExchangeTime;
        ToRandomMultiplier();
    }
    private void Update()
    {
        MoneySubmitText.text = "GET\n" + Camera.main.GetComponent<Inventory>().Petrol * Multiplier + "$";
        
        faketimer =- Time.deltaTime;
        if((timer += Time.deltaTime) >= ToExchangeTime)
        {
            timer = 0;
            ToRandomMultiplier();
            
            MoneyIconTimer.fillAmount = 1;
        }
        else MoneyIconTimer.fillAmount = 1 - timer / ToExchangeTime;
    }

    private void ToRandomMultiplier()
    {
        Multiplier = UnityEngine.Random.Range(RandomRangeStart, RandomRangeEnd);
        MoneyMultiplierText.text = Multiplier + "X";
        if (Multiplier > 0) MoneyMultiplierText.color = Color.green;
        else MoneyMultiplierText.color = Color.red;
    }

    private void TimerUpdate()
    {
        timer = 0;
        ToRandomMultiplier();
        MoneySubmitText.text = "GET\n" + Camera.main.GetComponent<Inventory>().Petrol * Multiplier + "$";
        MoneyIconTimer.fillAmount = 1;
    }
    public void SubmitDeal()
    {
        Camera.main.GetComponent<Inventory>().AddMoney(Convert.ToInt32(Camera.main.GetComponent<Inventory>().Petrol * Multiplier));
        Camera.main.GetComponent<Inventory>().AddPetrol(Camera.main.GetComponent<Inventory>().Petrol * -1);
        TimerUpdate();
        
    }


}
