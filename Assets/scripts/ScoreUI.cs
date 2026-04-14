using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI ClasUI;
    public TextMeshProUGUI HealText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI timeEndText;
    public TextMeshProUGUI ability1text;
    public TextMeshProUGUI ability2text;
    private TimeSpan timdeed;
    public static ScoreUI Instance;
    string sd23;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "0";
    }
    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
            
    }
    public void ScoreUpdate(int score)
    {
        scoreText.text = $"SCORE: {score}";
        
    }
   public void Resetscore()
    {
        scoreText.text = "SCORE: 0";
    }
    public void HpUpdate(int hp)
    {
        hpText.text = $"Hp: {hp}";
    }
    public void ResetHp(int hp)
    {
        hpText.text = $"Hp: {hp}";
    }
    public void ClasUpdate(string Clas)
    {
        ClasUI.text = $"Class: {Clas}";
    }
  public void canHealUpdate()
    {
        HealText.text = "Heal: Ready";
    }
    public void canNotHealUpdate(float couldown)
    {
        timdeed = TimeSpan.FromSeconds(couldown);
        HealText.text = $"Heal: {timdeed.ToString("ss")}";
    }
    public void speedTextUpdate()
    {
        SpeedText.text = $"Speed: {GameManager.Instance.speed}";
    }
    public void damageTextUpdate(int damage)
    {
        damageText.text = $"Damage: {damage}";
    }
    public void endtimeAndFloor(string time, int floor)
    {
        timeEndText.text = $"Died At: {floor} Time: {time}";
    }
    public void abliltyTextUpdate(string name, int slot)
    {
        if (slot == 1)
        {
            ability1text.text = $"{name}: Ready ";
        }
        else if (slot == 2)
        {
            ability2text.text = $"{name}: Ready ";
        }
            
    }
    public void abliltyspecialtext( int slot, string swe)
    {
        if (slot == 1)
        {
            ability1text.text = swe;
        }
        else if(slot == 2)
        {
            ability2text.text = swe;
        }
    }
    public void abilityCooldown(TimeSpan cooldown, int slot)
    {
        if(slot == 1)
        {
            
            //sd23 = cooldown.ToString("mm':'ss");
            ability1text.text = $"{cooldown.ToString("mm':'ss")}";
        }
        else if(slot == 2)
        {
            //sd23 = cooldown.ToString("mm':'ss");
            ability2text.text = $"{cooldown.ToString("mm':'ss")}"; ;
        }
    }
}

