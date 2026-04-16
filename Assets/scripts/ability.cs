using System;
using Unity.VisualScripting;
using UnityEngine;

public class ability : MonoBehaviour
{
    public int id;
    public PlayerMovent pm;
    float time23 = 0f;
    public float canusebailty = 0f;
    public float cooldown;

    bool oncooldown = false;
    public int slot;
    string names;
    int striks = 0;
   public  bool bomscanshoot = true;
    TimeSpan times;
    CircleCollider2D circle;
    SpriteRenderer sr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        circle = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        circle.enabled = true;
    }
    void Start()
    {
        switch (id)
        {
            case 0:
                names = "ShadeStep";
                break;

            case 1:
                names = "StrongStrike";

                break;

            case 2:
                names = "Boms";
                break;
            case 3:
                names = "TitaanKiller";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oncooldown == true)
        {
            cooldown = canusebailty - Time.time;
            times = TimeSpan.FromSeconds(cooldown);
            ScoreUI.Instance.abilityCooldown(times, slot);
            if (Time.time >= canusebailty)
            {
                oncooldown = false;
                ScoreUI.Instance.abliltyTextUpdate(names, slot);
                
            }

        }

        switch (id)
        {
            case 0:

                break;

            case 1:

                if (GameManager.Instance.hit235 == true)
                {
                    GameManager.Instance.extraDamageX = 1;
                    striks = 0;
                    GameManager.Instance.hit235 = false;

                    canusebailty = Time.time + 30f;
                    oncooldown = true;
                }
                break;
            case 2:
                if (Time.time >= canusebailty)
                {

                    bomscanshoot = true;
                }
               
                break;
            case 3:
                break;
        }
    }
    public void startupabilty()
    {
        if (Time.time >= canusebailty)
        {

            ScoreUI.Instance.abliltyTextUpdate(names, slot);
        }
        sr.enabled = false;
        circle.enabled = false;
    }
    public void abilitys()
    {
        switch (id)
        {
            case 0:

                if (Time.time >= canusebailty)
                {
                    pm.Iframs = true;
                    var d = GameManager.Instance.speed;
                    GameManager.Instance.speed = GameManager.Instance.speed / 2;
                    time23 = Time.time;
                    if (time23 >= time23 + 2f)
                    {
                        pm.Iframs = false;
                    }
                    if (time23 >= time23 + 4f)
                    {
                        if (d > GameManager.Instance.speed)
                        {
                            GameManager.Instance.speed = d;
                            GameManager.Instance.addStats();

                        }
                    }

                    canusebailty = Time.time + 20f;
                    oncooldown = true;
                }
                break;
            case 1:
                if (Time.time >= canusebailty)
                {
                    Debug.Log("useAbility");
                    GameManager.Instance.extraDamageX = 2;
                    striks = 1;
                    ScoreUI.Instance.abliltyspecialtext(slot, $"{striks}");

                }


                break;
            case 2:

                if (Time.time >= canusebailty)
                {
                    Debug.Log("useAbility");
                    //pm.shootBom();
                   
                   canusebailty = Time.time + 15f;
                    
                   oncooldown = true;
                }
                break;
            case 3:

                if (Time.time >= canusebailty)
                {
                    Debug.Log("useAbility");
                    GameManager.Instance.orbspawnAmount = 9;

                    canusebailty = Time.time + 120f;
                    oncooldown = true;
                }
                break;
        }


    }
}
