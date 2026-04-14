using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI upgrade1;
    public TextMeshProUGUI upgrade2;
    public TextMeshProUGUI upgrade3;
    public PowerUp23 setupgrade1;
    public PowerUp23 setupgrade2;
    public PowerUp23 setupgrade3;
    

    void Start()
    {
        //getUpgrades();
        
    }
    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            setupgrade1.speed = 0;
            setupgrade2.speed = 0;
            setupgrade3.speed = 0;
            setupgrade1.damage = 0;
            setupgrade2.damage = 0;
            setupgrade3.damage = 0;
            setupgrade1.hp = 0;
            setupgrade2.hp = 0;
            setupgrade3.hp = 0;
            getUpgrades();

        }
    }
    void getUpgrades()
    {
        upgrade1.text = getvalue(setupgrade1);
        upgrade2.text = getvalue(setupgrade2);
        upgrade3.text = getvalue(setupgrade3);
        

    }
    public string getvalue(PowerUp23 up)
    {
        
        string value = "";
        string AddValue = "";
        int lastrando = 900;
        int rando = UnityEngine.Random.Range(0, 6);
        int rando2;
        if(rando == 5)
        {
            for(int i = 0; i < 2; i++)
            {
                rando2 = UnityEngine.Random.Range(0, 3);
                
                
                if (rando2 == 0)
                {
                    if(lastrando != rando2)
                    {
                     AddValue += " Speed: +1";
                        
                      up.speed = 1;
                    }
                    else
                    {

                        AddValue = " Speed: +2";
                        up.speed = 2;
                    }

                }

                if (rando2 == 1)
                {
                    if (lastrando != rando2)
                    {
                     AddValue += " Damage: +5";
                       up.damage = 5;

                    }
                    else
                    {
                        AddValue = " Damage: +10";
                        up.damage = 10;
                    }
                }
                if (rando2 == 2)
                {
                    if (lastrando != rando2)
                    {
                     AddValue += " Hp: +10";
                      up.hp = 10;
                    }
                    else
                    {
                        AddValue = " Hp: +20";
                        up.hp = 20;
                    }
                }
                lastrando = rando2;
            }
        }
        else
        {
            rando = UnityEngine.Random.Range(0, 2);
            if (rando == 0)
            {
                AddValue = "Speed: +1";
                up.speed = 1;
            }

            if (rando == 1)
            {
                AddValue = "Damage: +5";
                up.damage = 5;
            }
            if (rando == 2)
            {
                AddValue = "Hp: +10";
                up.hp = 10;
            }
        }
        value = AddValue;
        
        return value;
    }
    
}
