using System.Collections;
using System.Collections.Generic;
using System.IO;
using script;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private GameObject enemyObject1; 
    [SerializeField] private GameObject enemyObject2;
    [SerializeField] private Transform spawnLocation; 
    [SerializeField] private float gameSpeed = 1.0f;
    
    private List<GameObject> enemyList = new List<GameObject>();
    private Coroutine enemySpawn;
    [SerializeField] private float enemy2Offset = 0.5f;
    
    [Header("GamePlay")]
    private bool isGameStart = false;
    
    [Header("Save")]
    private string saveFilePath;
    private SaveData saveData;

    [Header("UI")] 
    [SerializeField] private GameObject gameStartUI;
    [SerializeField] private GameObject gameOverUI;
    
    [Header("Score")]
    [SerializeField] private GameObject scoreObject;
    private TextMeshProUGUI scoreHigh;
    private TextMeshProUGUI scoreCur;
    private int highScore;
    private int curScore = 0;
    
    void Start()
    {
        saveFilePath = Application.persistentDataPath + "/savefile.json";
        LoadData();
        highScore = saveData.highScore;
        
        scoreHigh = scoreObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreCur = scoreObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Time.timeScale = 0;

        scoreHigh.text = highScore.ToString();
        gameOverUI.SetActive(false);
        gameStartUI.SetActive(true);
    }
    
    void Update()
    {
        if (isGameStart)
        {
            gameSpeed += Time.deltaTime / 20;
            
            curScore += (int)(gameSpeed * Time.deltaTime * 200);
            scoreCur.text = curScore.ToString();
        }

        if (!isGameStart && Input.anyKey)
        {
            newGame();
        }
    }
    
    private IEnumerator spawnEnemy()
    {
        while (true)
        {
            float spawnInterval = 3.0f / gameSpeed;
            
            int randomValue = Random.Range(0, 2);
            GameObject dmpEnemy;
            if(randomValue == 0) dmpEnemy = Instantiate(enemyObject1, spawnLocation.position, spawnLocation.rotation);
            else dmpEnemy = Instantiate(enemyObject2, new Vector2(spawnLocation.position.x, spawnLocation.position.y + enemy2Offset), spawnLocation.rotation);
            enemyList.Add(dmpEnemy);
            
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    private void SaveData()
    {
        saveData = new SaveData();
        saveData.highScore = highScore;
        
        string json = JsonUtility.ToJson(saveData);
        
        File.WriteAllText(saveFilePath, json);
    }

    private void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
            saveData.highScore = 0;
            SaveData(); 
        }
    }

    public void newGame()
    {
        GameObject.Find("Background").transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        Time.timeScale = 1f;
        gameSpeed = 1.0f;
        enemySpawn = StartCoroutine(spawnEnemy());
        isGameStart = true;
        if (curScore > highScore)
        {
            highScore = curScore;
            scoreHigh.text = highScore.ToString();
            SaveData();
        }
        curScore = 0;
        gameOverUI.SetActive(false);
        gameStartUI.SetActive(false);
    }

    public void gameOver()
    {
        foreach (var e in enemyList)  Destroy(e);
        StopCoroutine(enemySpawn);

        Time.timeScale = 0f;
        isGameStart = false;

        gameOverUI.SetActive(true);
    }

    public float GameSpeed
    {
        get => gameSpeed;
        set => gameSpeed = value;
    }

    public bool IsGameStart
    {
        get => isGameStart;
        set => isGameStart = value;
    }
}
