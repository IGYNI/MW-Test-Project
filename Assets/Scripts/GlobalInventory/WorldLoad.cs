using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class WorldLoad : MonoBehaviour
{
    private string PATH; 
    [SerializeField] private string NAMEofInventorySave;
    [SerializeField] private string NAMEofObjectsSave; 
    
    [SerializeField] public GameObject[] Banks;
    [SerializeField] public GameObject[] PetrolsPlant;
    [SerializeField] public GameObject[] OilsRig;

    private List<GameObject> BuildingsAsObject = new List<GameObject>();


    private void Awake() 
    {
        PATH = Application.streamingAssetsPath;
        if(IsFileEmpty())
        {
            Debug.Log("b");
            AddAllStartBuildings();
            LoadInJsonFileStartBuildings();
        }
        else
        {
            Debug.Log("с");
            LoadBuildingsInList();
            LoadInventory();
        } 
        Debug.Log("d");
        
    }


    private void AddBuildingInList(GameObject Building)
    {
        BuildingsAsObject.Add(Building);
    }
    public void AddBuildingInList(GameObject Building, Vector3 pos)
    {
        var TEMPBuildig = Building;
        TEMPBuildig.GetComponent<Transform>().position = pos;
        BuildingsAsObject.Add(TEMPBuildig);
    }



    private bool IsFileEmpty()
    {
        StreamReader sr = new StreamReader(PATH + NAMEofObjectsSave);
        var line = sr.ReadLine();
        if(line == "StartBuildings") return true;
        else return false;
    }

    

    private void AddAllStartBuildings()
    {
        AddBuildingInList(Banks[0], new Vector3(8, -0.47f, -16));
        AddBuildingInList(PetrolsPlant[0], new Vector3(4, -0.743f, -13));
    }

    private void LoadInJsonFileStartBuildings()
    {
        for (int i = 0; i < BuildingsAsObject.Count; i++)
        {
            var build = Instantiate(BuildingsAsObject.ElementAt(i), BuildingsAsObject.ElementAt(i).GetComponent<Transform>().position, Quaternion.identity);
            build.GetComponent<Build>().enabled = false;
        }
    }
    
    private void SaveBuildings()
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (var fs = new FileStream(PATH + NAMEofObjectsSave, FileMode.Open))
        {
            CurrentSaving Save = new CurrentSaving();

            Save.SaveObjects(BuildingsAsObject);

            bf.Serialize(fs, Save.Objects);

        }
        
        
    }

    private void LoadBuildingsInList()
    {
        
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(PATH + NAMEofObjectsSave + "1")) File.Delete(PATH + NAMEofObjectsSave + "1");
        
        File.Copy(PATH + NAMEofObjectsSave, PATH + NAMEofObjectsSave + "1");
        var ObjectList = new List<CurrentSaving.ObjectsToSave>();
        using (var fs = new FileStream(PATH + NAMEofObjectsSave + "1", FileMode.Open))
        {
            ObjectList = (List<CurrentSaving.ObjectsToSave>)bf.Deserialize(fs);
        }
        File.Delete(PATH + NAMEofObjectsSave + "1");

        CurrentSaving Save = new CurrentSaving();
        Save.LoadList(ObjectList);
        Save.LoadObjects();

        
    }

    public void AddBuildingAndSave(GameObject obj)
    {
        AddBuildingInList(obj);
        SaveBuildings();
    }

    public void SaveInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();

        using (var fs = new FileStream(PATH + NAMEofInventorySave, FileMode.Open))
        {
            CurrentSaving Save = new CurrentSaving();
            Save.SaveInventory(new CurrentSaving.InventorySave(Oil: GetComponent<Inventory>().Oil,
                                                               Petrol: GetComponent<Inventory>().Petrol,
                                                               Money: GetComponent<Inventory>().Money));
            bf.Serialize(fs, Save.Inventory);
        }
    }

    public void LoadInventory()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(PATH + NAMEofInventorySave + "1")) File.Delete(PATH + NAMEofInventorySave + "1");
        File.Copy(PATH + NAMEofInventorySave, PATH + NAMEofInventorySave + "1");
        var Inventory = new CurrentSaving.InventorySave();
        using (var fs = new FileStream(PATH + NAMEofInventorySave + "1", FileMode.Open))
        {
            Inventory = (CurrentSaving.InventorySave)bf.Deserialize(fs);
        }
        CurrentSaving Save = new CurrentSaving();
        Save.SaveInventory(Inventory);
        Save.LoadInventory();
    }


}





[System.Serializable]
public class CurrentSaving : MonoBehaviour
{
    [System.Serializable]
    public struct ObjectsToSave
    {
        public string name; 
        public float x, y, z;

        public ObjectsToSave(string name, float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.name = name;
        }
    }

    
    
    public List<ObjectsToSave> Objects = new List<ObjectsToSave>(); 
    public void SaveObjects(List<GameObject> ListOfBuildings)
    {
        foreach(var obj in ListOfBuildings)
        {
            string nm = obj.name;
            float _x =  obj.GetComponent<Transform>().position.x;
            float _y =  obj.GetComponent<Transform>().position.y;
            float _z =  obj.GetComponent<Transform>().position.z;
            Objects.Add(new ObjectsToSave(name: nm,
                                        x: _x,
                                        y: _y,
                                        z: _z));
        }
    }

    public void LoadList(List<ObjectsToSave> newList)
    {
        Objects = newList;
    }

    
    public void LoadObjects()
    {
        foreach(var obj in Objects)
        {
            
            if(obj.name.Contains("Bank"))
            {
                int lvl = 0;
                if(obj.name.Contains("1lvl")) lvl = 0;
                if(obj.name.Contains("2lvl")) lvl = 1;
                if(obj.name.Contains("3lvl")) lvl = 2;
                Camera.main.GetComponent<WorldLoad>().AddBuildingInList(Camera.main.GetComponent<WorldLoad>().Banks[lvl], new Vector3(obj.x, obj.y, obj.z));
                var _currentObj = Instantiate(Camera.main.GetComponent<WorldLoad>().Banks[lvl], new Vector3(obj.x, obj.y, obj.z), Quaternion.identity);
                _currentObj.GetComponent<Build>().enabled = false;
            }
            if(obj.name.Contains("OilRig"))
            {
                int lvl = 0;
                if(obj.name.Contains("1lvl")) lvl = 0;
                if(obj.name.Contains("2lvl")) lvl = 1;
                if(obj.name.Contains("3lvl")) lvl = 2;
                Camera.main.GetComponent<WorldLoad>().AddBuildingInList(Camera.main.GetComponent<WorldLoad>().OilsRig[lvl], new Vector3(obj.x, obj.y, obj.z));
                var _currentObj = Instantiate(Camera.main.GetComponent<WorldLoad>().OilsRig[lvl], new Vector3(obj.x, obj.y, obj.z), Quaternion.identity);
                _currentObj.GetComponent<Build>().enabled = false;
            }
            if(obj.name.Contains("PetrolPlant"))
            {
                int lvl = 0;
                if(obj.name.Contains("1lvl")) lvl = 0;
                if(obj.name.Contains("2lvl")) lvl = 1;
                if(obj.name.Contains("3lvl")) lvl = 2;
                Camera.main.GetComponent<WorldLoad>().AddBuildingInList(Camera.main.GetComponent<WorldLoad>().PetrolsPlant[lvl], new Vector3(obj.x, obj.y, obj.z));
                var _currentObj = Instantiate(Camera.main.GetComponent<WorldLoad>().PetrolsPlant[lvl], new Vector3(obj.x, obj.y, obj.z), Quaternion.identity);
                _currentObj.GetComponent<Build>().enabled = false;
            }
        }
    }

    [System.Serializable]
    public struct InventorySave
    {
        public int Oil, Petrol, Money;

        public InventorySave(int Oil, int Petrol, int Money)
        {
            this.Oil = Oil;
            this.Petrol = Petrol;
            this.Money = Money;
        }
    }

    public InventorySave Inventory;

    public void SaveInventory(InventorySave newInventory)
    {
        Inventory = newInventory;
        //Debug.Log("Сох");
    }
    public void LoadInventory()
    {
        Camera.main.GetComponent<Inventory>().Oil = Inventory.Oil;
        Camera.main.GetComponent<Inventory>().Petrol = Inventory.Petrol;
        Camera.main.GetComponent<Inventory>().Money = Inventory.Money;
        //Debug.Log("Заг");
        Camera.main.GetComponent<Inventory>().OilUIText.text = Convert.ToString(Inventory.Oil);
        Camera.main.GetComponent<Inventory>().PetrolUIText.text = Convert.ToString(Inventory.Petrol);
        Camera.main.GetComponent<Inventory>().MoneyUIText.text = Convert.ToString(Inventory.Money);
        
    }

}
