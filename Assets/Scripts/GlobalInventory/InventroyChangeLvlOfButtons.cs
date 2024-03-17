using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventroyChangeLvlOfButtons : MonoBehaviour
{
    [SerializeField] private GameObject[] ButtonsToBuy;
    
    private int _currentLvl;
    private void Awake() 
    {
        _currentLvl = 0;
    }
    

    public void ChangeLvlOfButtons(int lvl)
    {
        ButtonsToBuy[_currentLvl].SetActive(false);
        _currentLvl = lvl--;
        ButtonsToBuy[_currentLvl].SetActive(true);
    }
    
    public void NextLvlOfButtons()
    {
        ButtonsToBuy[_currentLvl].SetActive(false);
        if (_currentLvl != 2)_currentLvl++;
        else _currentLvl = 0;
        //Debug.Log(_currentLvl);
        ButtonsToBuy[_currentLvl].SetActive(true);
    }

    public void BackLvlOfButtons()
    {
        ButtonsToBuy[_currentLvl].SetActive(false);
        if (_currentLvl != 0)_currentLvl--;
        else _currentLvl = 2;
        //Debug.Log(_currentLvl);
        ButtonsToBuy[_currentLvl].SetActive(true);
    }


    
}
