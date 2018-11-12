using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- XML Saving namespaces---//
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

/*
[XmlRoot("Game Data")]
public class GameData
{
    //---All game data able to be saved to a file---//
    public Vector3 playerPos;
    public int score;
    public Quaternion playerRot;
    public void Save(string filePath)
    {
        // Create a tool for converting class into XML
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        // Generate a file stream and open file stream to path
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            // Save file to path
            serializer.Serialize(stream, this);
        }
    }

    public void Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            // Create a tool for converting XML into class
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            // Generate a file stream with the mode 'Open' to read file
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                GameData data = serializer.Deserialize(stream) as GameData;
                //---Load the game data values here---//
                playerPos = data.playerPos;
                score = data.score;
                playerRot = data.playerRot;
            }
        }
    }
}*/

[Serializable]
public class GameData
{
    public Vector3 playerPos;
    public int score;
    public Quaternion playerRot;
}

public class GameSave : MonoBehaviour
{
    #region Singleton
    public static GameSave Instance = null;
    private void Awake()
    {
        Instance = this;
        Load();
    }
    public static GameData GetData()
    {
        return Instance.data;
    }
    #endregion

    public GameData data = new GameData();
    public string fileName = "GameSave";

    public void Save()
    {
        string fullPath = Application.dataPath + "/Data/" + fileName + ".json";
        //data.Save(fullPath);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(fullPath, json);

        print("Saved to path: " + fullPath);
        // C:/Users/anshuman.meghnad/Documents/advanced_programming/AssetsGameSave.xml
    }

    public void Load()
    {
        string fullPath = Application.dataPath + "/Data/" + fileName + ".json";
        //data.Load(fullPath);
        string json = File.ReadAllText(fullPath);
        data = JsonUtility.FromJson<GameData>(json);

        print("Loaded from path: " + fullPath);
        // C:/Users/anshuman.meghnad/Documents/advanced_programming/AssetsGameSave.xml
    }

    public static bool Exists()
    {
        string fullPath = Application.dataPath + "/Data/" + Instance.fileName + ".json";
        return File.Exists(fullPath);
    }
}
