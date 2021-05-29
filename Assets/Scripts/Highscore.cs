using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : Singleton<Highscore>
{
    [SerializeField] private Text _Highscore;
    [SerializeField] private Text _Currscore;
    
    private int currScore = 0;
    
    private Data _data = new Data();

    private void Awake()
    {
        if (!loadData())
        {
            ResetData();
        }
        UpdateTextfields();
    }
    public void ResetData()
    {
        _data.Highscore = 0;
        saveData();
    }

    public int getCurrScore()
    {
        return currScore;
    }

    public void DisableCurrScore()
    {
        setCurrScore(0);
        _Currscore.gameObject.SetActive(false);
    }
    public void setCurrScore(int currScore)
    {
        if (!_Currscore.gameObject.activeSelf)
        {
            _Currscore.gameObject.SetActive(true);
        }
        this.currScore = currScore;
        if (this.currScore > _data.Highscore)
        {
            _data.Highscore = this.currScore;
            saveData();
        }
        UpdateTextfields();
    }

    private void UpdateTextfields()
    {
        _Highscore.text = "Highscore: " + _data.Highscore;
        _Currscore.text = "Current score: " + currScore;
    }
    public bool loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/Data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Data.dat", FileMode.Open);
            this._data = (Data) bf.Deserialize(file);
            file.Close();
            return true;
        }
        return false;
    }
    public void saveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Data.dat");
        bf.Serialize(file, this._data);
        file.Close();
    }
}
[Serializable]
public class Data
{
    [SerializeField] private int highscore;
    public int Highscore
    {
        get => highscore;
        set => highscore = value;
    }
}
