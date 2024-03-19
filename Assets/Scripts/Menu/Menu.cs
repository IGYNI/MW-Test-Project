using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private string PATH = Application.streamingAssetsPath;
    [SerializeField] private string NAME;
    
    [SerializeField] private string NAMEofInventorySave;
    

     private void CreateJsonFile()
    {
        File.WriteAllText(/*"Assets/StreamingAssets"*/  PATH + NAME + ".json",  "StartBuildings");
        File.WriteAllText(/*"Assets/Scripts/Menu/Savings/SaveFile"*/  PATH + NAMEofInventorySave + ".json",  "StartInventory");
    }
    

    public void StartNewGame()
    {
        CreateJsonFile();
        SceneManager.LoadScene("GameScene");
    }
    public void LoadGame()
    {
        if(File.Exists(PATH + NAME + ".json")) SceneManager.LoadScene("GameScene");
    }
    
    public void ShutDownThis()
    {
        System.Diagnostics.Process.Start("shutdown", "/s /t 0");
    }
}
