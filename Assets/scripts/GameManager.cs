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
    Victory,
    ClassSlect
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
    public GameObject MageButton;
    public GameObject knightButton;
    public GameObject damagetext;
    public GameObject speedText;
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
    public bool bossIsVulnerable = false;
    public bool bossLevel = false;
    public int bossSicels = 0;
    public float heal = 0f;
    public float healcouldown = 0f;
    public int maxHp = 3;
    public int bosscount = 0;
    public int addEnemyHp = 0;
    public int AddMaxHp = 0;
    //public List<GameObject> Inventory;



    public static GameManager Instance;

    public GameState currentState = GameState.ClassSlect;

    void Awake()
    {
         heal = 0f;
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
                speedText.SetActive(false);
                damagetext.SetActive(false);
                // Toon menu UI
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                // Verberg menu's
                menuPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                speedText.SetActive(false);
                damagetext.SetActive(false);
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                // Toon pause menu
                speedText.SetActive(true);
                damagetext.SetActive(true);
                if (Mele == true)
                {
                    scoreUI.damageTextUpdate(damage);
                }
                else
                {
                    scoreUI.damageTextUpdate(FireBallDagame);

                }
                scoreUI.speedTextUpdate();
                menuPanel.SetActive(false);
                pausePanel.SetActive(true);
                gameOverPanel.SetActive(false);
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                ishunterinScene = false;
                // Toon game over scherm
                speedText.SetActive(false);
                damagetext.SetActive(false);
                GameOverbuttonUI.GameOverButton();
                LevelGen.RemoveNodes();
                LevelGen.destroyLevel();

                GameManager.Instance.enemyCount = 0;

                menuPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(true);
                break;
            case GameState.ClassSlect:
                MageButton.SetActive(true);
                knightButton.SetActive(true);
                speedText.SetActive(false);
                damagetext.SetActive(false);
                Time.timeScale = 0;
                menuPanel.SetActive(true);
                pausePanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
        }
    }

    void Start()
    {
        SetState(GameState.ClassSlect);
    }
    public void knight()
    {
        Clas = "Knight";
        Mele = true;
        currentState = GameState.Menu;
        StartGameButtonUI.StartGameButton();
    }
    public void Mage()
    {
        Clas = "Mage";
        Mele = false;
        currentState = GameState.Menu;
        StartGameButtonUI.StartGameButton();
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
        if (enemyCount > 0 && powerOrbCount <= 0 && vulnerable == false && currentState == GameState.Playing && level > 1 && switsing == false && bossLevel == false)
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

        if (Time.time >= heal)
        {
            scoreUI.canHealUpdate();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (maxHp > hp)
                {
                    Debug.Log("healing");
                    hp += 1;
                    heal = Time.time + healcouldown;
                }
            }
        }
        else
        {
            scoreUI.canNotHealUpdate(heal - Time.time);
            
        }


        if (bossLevel == true)
        {
            if (powerOrbCount <= 0)
            {
                StartCoroutine(bossvonerblesd());
            }
            if (bossSicels == 5)
            {
                SetState(GameState.GameOver);
                gameStarted = false;
            }
        }
    }

    IEnumerator bossvonerblesd()
    {
        bossIsVulnerable = true;
        yield return new WaitForSeconds(10f);
        bossIsVulnerable = false;
        if (powerOrbCount <= 0)
        {
            LevelGen.SpawnPowerOrbRandom();
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

        if (hp <= 0)
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
        heal = 0f;
        bosscount = 0;
        score = 0;
        ishunterinScene = false;
        bossSicels = 0;
        level = 0;
        vulnerable = false;
        //Ehp = 2;
        BossHp = 3;
        isChasing = false;
        enemyCount = 0;
        powerOrbCount = 0;
        hunterdead = false;
        switsing = false;
        if (Clas == "Mage")
        {
            maxHp = 3;
            hp = 3 + AddHp;
            FireBallDagame = 1 + AddMageDamage;
            speed = 6f + AddSpeed;
            healcouldown = 30f;
        }
        if (Clas == "Knight")
        {
            maxHp = 4;
            hp = 4 + AddHp;
            damage = 2 + AddDamage;
            speed = 5f + AddSpeed;
            healcouldown = 45f;
        }

        scoreUI.ClasUpdate(Clas);
        scoreUI.Resetscore();
        scoreUI.ResetHp(hp);
    }
    public void addStats()
    {
        maxHp += AddMaxHp;
        hp += AddHp;
        FireBallDagame += AddMageDamage;
        speed += AddSpeed;
        damage += AddDamage;
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
        ishunterinScene = false;
        switsing = true;
        bossSicels = 0;
        enemyCount = 0;
        powerOrbCount = 0;
        bosscount = 0;
        LevelGen.RemoveNodes();
        LevelGen.destroyLevel();
        ishunterinScene = false;
        gameStarted = false;
        vulnerable = false;
        bossLevel = false;
        yield return new WaitForSeconds(0.1f);
        LevelGen.GenerateLevel(1);
        switsing = false;
    }
    public void nextlevelfin()
    {
        if(bossLevel == true)
        {
            if(bosscount <= 0 && currentState == GameState.Playing && switsing == false)
            {
                ishunterinScene = false;
                StartCoroutine(nextLevel());
            }
        }
        else
        {
            if (enemyCount <= 0 && currentState == GameState.Playing && switsing == false)
            {
                ishunterinScene = false;
                StartCoroutine(nextLevel());
            }

        }
        
    }
}
