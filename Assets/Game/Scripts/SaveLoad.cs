using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public static bool[] openLevels = new bool[10];
    public static int[] stars = new int[10];
    public static int highScore;
    public static int checkMarksCount;
    public static int bonusBallCount;

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGame.gd");
        SaveData data = new SaveData();
        data.openLevels = openLevels;
        data.stars = stars;
        data.highScore = highScore;
        data.checkMarksCount = checkMarksCount;
        data.bonusBallCount = bonusBallCount;
        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGame.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            openLevels = data.openLevels;
            stars = data.stars;
            highScore = data.highScore;
            checkMarksCount = data.checkMarksCount;
            bonusBallCount = data.bonusBallCount;
            file.Close();
        }
    }
}

[Serializable]
class SaveData //можно записывать разные виды данных
{
    public bool[] openLevels = new bool[10];
    public int[] stars = new int[10];
    public int highScore;
    public int checkMarksCount;
    public int bonusBallCount;
}
