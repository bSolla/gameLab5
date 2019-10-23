using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadSaveManager : MonoBehaviour
{
//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                V A R I A B L E S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    // instance for use anywhere in our scripts--------
    public static LoadSaveManager instance = null;
    // ------------------------------------------------

    const string SAVE_NAME = "saveData";


//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
//                                  M E T H O D S 
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    // singleton pattern check so we only have one instance of the object in the game
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // used when creating new save / new game
    public void Reset()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;

        if (!File.Exists(SAVE_NAME))
        {
            file = File.Create(SAVE_NAME);
        }

        else
        {
            file = File.Open(SAVE_NAME, FileMode.Open);
        }

        SaveData data = new SaveData();

        //data.playtime = Mathf.Round(GameManager.instance.playtime);
        data.numberOfChickens = GameManager.instance.numberOfChickens;
        data.chickenStatusValues = GameManager.instance.GetChickenStatusList();

        binaryFormatter.Serialize(file, data);
        file.Close();
    }


    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file;

        if (!File.Exists(SAVE_NAME))
        {
            file = File.Create(SAVE_NAME);
        }
        else
        {
            file = File.Open(SAVE_NAME, FileMode.Open);
        }

        SaveData data = new SaveData();
        
        data.chickenStatusValues = GameManager.instance.GetChickenStatusList();
        data.numberOfChickens = GameManager.instance.numberOfChickens;
        //data.playtime = Mathf.Round(GameManager.instance.playtime); // add later on

        binaryFormatter.Serialize(file, data);
        file.Close();
    }

    public void DoLoad()            //For button
    {
        Load();
    }
    public bool Load()
    {
        bool exists = File.Exists(SAVE_NAME);

        if (exists)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(SAVE_NAME, FileMode.Open);

            SaveData data = (SaveData)binaryFormatter.Deserialize(file);
            file.Close();

            GameManager.instance.ClearChickenGroup();
            GameManager.instance.numberOfChickens = data.numberOfChickens;
            GameManager.instance.InitializeAndCacheChildObjects();
            GameManager.instance.LoadChickenStatusValues(data.chickenStatusValues);
            //GameManager.instance.playtime = Mathf.Round(data.playtime); // add later on
        }

        return exists;
    }

    public bool saveDataExists()
    {
        return File.Exists(SAVE_NAME);
    }


    // for testing purposes, remove later and integrate into a proper menu
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
}


[Serializable]
public class ChickenStatusValues
{
    public int hunger, thirst, happiness;
    public ChickenStatus.ChickenState currentState;
    public string name;
}


// the variables we want to save 
[Serializable]
class SaveData
{
    //public float playtime; // for later
    public int numberOfChickens;
    public List<ChickenStatusValues> chickenStatusValues;
}