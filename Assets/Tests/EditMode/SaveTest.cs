using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using script;
using UnityEngine;
using UnityEngine.TestTools;

public class SaveTest
{
    private string saveFilePath;
    private SaveData saveData;
    
    [SetUp]
    public void SetUp()
    {
        saveFilePath = Application.persistentDataPath + "/savefile.json";
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }
    
    

    [Test]
    public void SaveData_Correctly()
    {
        // Arrange
        saveData = new SaveData { highScore = 100 };

        // Act
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFilePath, json);

        // Assert
        Assert.IsTrue(File.Exists(saveFilePath));
        string loadedJson = File.ReadAllText(saveFilePath);
        SaveData loadedSaveData = JsonUtility.FromJson<SaveData>(loadedJson);
        Assert.AreEqual(saveData.highScore, loadedSaveData.highScore);
    }

    [Test]
    public void LoadData_Correctly()
    {
        // Arrange
        saveData = new SaveData { highScore = 200 };
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFilePath, json);

        // Act
        string loadedJson = File.ReadAllText(saveFilePath);
        SaveData loadedSaveData = JsonUtility.FromJson<SaveData>(loadedJson);

        // Assert
        Assert.AreEqual(200, loadedSaveData.highScore);
    }
}
