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
    public bool hit234 = false;
    public bool vulnerable = false;
    //public int Ehp = 2;
    public int BossHp = 30;
    public int FireBallDagame = 10;
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
    public GameObject endtimeUI;
    public float speed;
    public int damage = 15;
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
    public int maxHp = 30;
    public int bosscount = 0;
    public int addEnemyHp = 0;
    public int AddMaxHp = 0;
    float time23 = 0f;
    public GameObject up1;
    public GameObject up2;
    public GameObject up3;




    public static GameManager Instance;

    public GameState currentState = GameState.ClassSlect;

    void Awake()
    {
        heal = 0f;
        endtimeUI.SetActive(false);
       
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
        shown();
    }
    public void shown()
    {
        up1.SetActive(true);
        up2.SetActive(true);
        up3.SetActive(true);
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
                ishunterinScene = false;
                timer.instance.endTimer();
                // Toon game over scherm

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
            endtimeUI.SetActive(true);
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
                    hp += 10;
                    scoreUI.HpUpdate(hp);
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

                time23 = Time.time + 10f;
                bossIsVulnerable = true;
                if (Time.time >= time23)
                {





                    

                    bossIsVulnerable = false;
                    hit234 = false;
                    if (powerOrbCount <= 0 && bossIsVulnerable == false && hit234 == false)
                    {
                        LevelGen.SpawnPowerOrbRandom();
                    }



                }
            }
            if (bossSicels == 5)
            {
                SetState(GameState.GameOver);
                endtimeUI.SetActive(true);
                gameStarted = false;
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

        if (hp <= 0)
        {
            Debug.Log("game over");
            gameStarted = false;
            endtimeUI.SetActive(true);
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
            maxHp = 30;
            hp = 30 + AddHp;
            FireBallDagame = 10 + AddMageDamage;
            speed = 6f + AddSpeed;
            healcouldown = 30f;
            scoreUI.damageTextUpdate(FireBallDagame);
        }
        if (Clas == "Knight")
        {
            maxHp = 40;
            hp = 40 + AddHp;
            damage = 15 + AddDamage;
            speed = 5f + AddSpeed;
            healcouldown = 45f;
            scoreUI.damageTextUpdate(damage);
        }
        scoreUI.speedTextUpdate();

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
        if (Mele == true)
        {
            scoreUI.damageTextUpdate(damage);
        }
        else
        {
            scoreUI.damageTextUpdate(FireBallDagame);

        }
        scoreUI.speedTextUpdate();
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
        if (Mele == true)
        {
            scoreUI.damageTextUpdate(damage);
        }
        else
        {
            scoreUI.damageTextUpdate(FireBallDagame);

        }
        scoreUI.speedTextUpdate();
        switsing = false;
    }
    public void nextlevelfin()
    {
        if (bossLevel == true)
        {
            if (bosscount <= 0 && currentState == GameState.Playing && switsing == false)
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
