using System.Collections;
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
 bool dieded = false;
 public bool vulnerable = false;
 //public int Ehp = 2;
 public int BossHp = 3;
 public int FireBallDagame = 1;
 public int highScore = 0;
 bool switsing = false;
 public GameObject menuPanel;
 public GameObject pausePanel;
 public GameObject gameOverPanel;
 public LevelGenerator LevelGen;


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
                LevelGen.destroyLevel();
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
        if (Input.GetKeyDown(KeyCode.Space))
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
        }
        if(enemyCount > 0 && powerOrbCount <= 0 && vulnerable == false && dieded)
        {
            SetState(GameState.GameOver);
            dieded = true;
        }
        if(enemyCount <= 0 && currentState == GameState.Playing && switsing == false)
        {
            StartCoroutine(nextLevel());
        } 
        
    }
    
    public void AddPoints(int points)
    {
        score = score + points;
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
        }
        
        if(hp <= 0)
        {
            Debug.Log("game over");
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
    public void StartGame() => SetState(GameState.Playing);
    public void PauseGame() => SetState(GameState.Paused);
    public void GameOver() => SetState(GameState.GameOver);
    void LoadGameData()
    {
     score = 0;
     hp = 3;
     level = 0;
     vulnerable = false;
     //Ehp = 2;
     BossHp = 3;
     FireBallDagame = 1;
     enemyCount = 0;
     powerOrbCount = 0;
     dieded = false;
     switsing = false;
    }
    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
    IEnumerator nextLevel()
    {
        switsing = true;
        
        LevelGen.destroyLevel();
        vulnerable = false;
        yield return new WaitForSeconds(0.1f);
        LevelGen.GenerateLevel(1);
        switsing = false;
    }
}
