using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver,
    Victory
}

public class GameManager : MonoBehaviour
{
 public int score = 0;
 public int hp = 3;
 public int enemyCount;
 public int powerOrbCount;
 public int level = 0;

 public bool vulnerable = false;
 //public int Ehp = 2;
 public int BossHp = 3;
 public int FireBallDagame = 1;
 public int highScore = 0;
 public bool Mele = false;
 private string Clas = "Mage";
 public bool switsing = false;
 public GameObject menuPanel;
 public GameObject pausePanel;
 public GameObject gameOverPanel;
 public LevelGenerator LevelGen;
 public ScoreUI scoreUI;
 public buttonUI GameOverbuttonUI;
 public buttonUI StartGameButtonUI;
 public float speed;
 public int damage = 2;
    public int AddHp = 0;
    public int AddDamage = 0;
    public int AddMageDamage = 0;
    public float AddSpeed = 0f;
    public bool hunterdead = false;
    public bool isChasing = false;
    public bool gameStarted = false;
    public bool ishunterinScene = false;
    //public List<GameObject> Inventory;
   


    public static GameManager Instance;

  public GameState currentState = GameState.Menu;

   void Awake()
{
    if (Instance == null)
    {
        Instance = this;
       DontDestroyOnLoad(gameObject);
        LoadHighScore();
        
    }
    else
    {
       Destroy(gameObject);
    }
}

    public void SetState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.Menu:
                Time.timeScale = 1;
                StartGameButtonUI.StartGameButton();
                menuPanel.SetActive(true);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                // Toon menu UI
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                // Verberg menu's
                menuPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                // Toon pause menu
                menuPanel.SetActive(false);
                pausePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                // Toon game over scherm
                GameOverbuttonUI.GameOverButton();
                LevelGen.RemoveNodes();
                LevelGen.destroyLevel();
                
                GameManager.Instance.enemyCount = 0;
                menuPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(true);
                break;
        }
    }

   void Start()
    {
        SetState(GameState.Menu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
                SetState(GameState.Paused);
            else if (currentState == GameState.Paused)
                SetState(GameState.Playing);
        }
      /*  if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentState == GameState.Menu)
            {
                SetState(GameState.Playing);
                LoadGameData();
                LevelGen.GenerateLevel(0);
            }
            if (currentState == GameState.GameOver)
            {
                
                SetState(GameState.Menu);
            }
        }*/
        if(enemyCount > 0 && powerOrbCount <= 0 && vulnerable == false && currentState == GameState.Playing && level > 1 && switsing == false)
        {
            SetState(GameState.GameOver);
            gameStarted = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Mele == true)
            {
                Mele = false;
                Clas = "Mage";
                scoreUI.ClasUpdate(Clas);
            }
            else
            {
                Mele = true;
                Clas = "Knight";
                scoreUI.ClasUpdate(Clas);
            }

        }
    }
    
    public void AddPoints(int points)
    {
        score += points;
        scoreUI.ScoreUpdate(score);
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
        }
        //  Debug.Log("Score: " + score);
    }
    public void TakeDamage(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            scoreUI.HpUpdate(hp);
        }
        
        if(hp <= 0)
        {
            Debug.Log("game over");
            gameStarted = false;
            SetState(GameState.GameOver);
        }
    }
    public void enemycountAdd(int count)
    {
        enemyCount += count;
    }
    public void powerOrbCountAdd(int count)
    {
        powerOrbCount += count;
    }
    public void StartGame()
    {
        SetState(GameState.Playing);
        
        LoadGameData();
        LevelGen.GenerateLevel(0);
    }
    public void PauseGame() => SetState(GameState.Paused);
    public void GameOver() => SetState(GameState.GameOver);
    public void Menu() => SetState(GameState.Menu);
    void LoadGameData()
    {
     score = 0;
     
     level = 0;
     vulnerable = false;
     //Ehp = 2;
     BossHp = 3;
        isChasing = false;
        enemyCount = 0;
     powerOrbCount = 0;
        hunterdead = false;
        switsing = false;
     if(Clas == "Mage")
        {
            hp = 3 + AddHp;
            FireBallDagame = 1 + AddMageDamage;
            speed = 6f + AddSpeed;
        }
     if(Clas == "Knight")
        {
            hp = 4 + AddHp;
            damage = 2 + AddDamage;
            speed = 5f + AddSpeed;
        }
     
     scoreUI.ClasUpdate(Clas);
     scoreUI.Resetscore();
     scoreUI.ResetHp(hp);
    }
    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    public IEnumerator nextLevel()
    {
        
        switsing = true;

        enemyCount = 0;
        powerOrbCount = 0;
        LevelGen.RemoveNodes();
        LevelGen.destroyLevel();
        ishunterinScene = false;
        gameStarted = false;
        vulnerable = false;
        yield return new WaitForSeconds(0.1f);
        LevelGen.GenerateLevel(1);
        switsing = false;
    }
    public void nextlevelfin()
    {
        
        if (enemyCount <= 0 && currentState == GameState.Playing && switsing == false)
        {
            StartCoroutine(nextLevel());
        }
    }
}
