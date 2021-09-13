using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class PlayerData : MonoBehaviour
{
    public string playerName;
    public int age;
    public int highScore;
    public static PlayerData Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void SetData()
    {
        string filePath = Application.persistentDataPath + "/Details.txt";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
        BinaryWriter writer = new BinaryWriter(stream);
        formatter.Serialize(stream, playerName);
        formatter.Serialize(stream, age);
        stream.Close();
    }
    public void GetData()
    {
        string filePath = Application.persistentDataPath + "/Details.txt";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open);
        formatter.Deserialize(stream);
        stream.Close();
    }
}
