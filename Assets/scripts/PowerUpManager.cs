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
    public int whitchupgrade = 0;

    void Start()
    {
        getUpgrades();
        whitchupgrade = 0;
    }
    private void Awake()
    {
        Boss.Upragades += getUpgrades;
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
        upgrade1.text = getvalue();
        upgrade2.text = getvalue();
        upgrade3.text = getvalue();
        whitchupgrade = 0;

    }
    string getvalue()
    {
        whitchupgrade++;
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
                        
                        switch (whitchupgrade)
                        {
                            case 1:
                                setupgrade1.speed = 1;
                                break;
                                case 2:
                                setupgrade2.speed = 1; 
                             break;
                                case 3:
                                setupgrade3.speed = 1;
                                break;
                        }
                    }
                    else
                    {

                        AddValue = " Speed: +2";
                        switch (whitchupgrade)
                        {
                            case 1:
                                setupgrade1.speed = 2;
                                break;
                            case 2:
                                setupgrade2.speed = 2;
                                break;
                            case 3:
                                setupgrade3.speed = 2;
                                break;
                        }
                    }

                }

                if (rando2 == 1)
                {
                    if (lastrando != rando2)
                    {
                     AddValue += " Damage: +1";
                        switch (whitchupgrade)
                        {
                            case 1:
                                setupgrade1.damage = 1;
                                break;
                            case 2:
                                setupgrade2.damage = 1;
                                break;
                            case 3:
                                setupgrade3.damage = 1;
                                break;
                        }

                    }
                    else
                    {
                        AddValue = " Damage: +2";
                        switch (whitchupgrade)
                        {
                            case 1:
                                setupgrade1.damage = 2;
                                break;
                            case 2:
                                setupgrade2.damage = 2;
                                break;
                            case 3:
                                setupgrade3.damage = 2;
                                break;
                        }
                    }
                }
                if (rando2 == 2)
                {
                    if (lastrando != rando2)
                    {
                     AddValue += " Hp: +1";
                        switch (whitchupgrade)
                        {
                            case 1:
                                setupgrade1.hp = 1;
                                break;
                            case 2:
                                setupgrade2.hp = 1;
                                break;
                            case 3:
                                setupgrade3.hp = 1;
                                break;
                        }
                    }
                    else
                    {
                        AddValue = " Hp: +2";
                        switch (whitchupgrade)
                        {
                            case 1:
                                setupgrade1.hp = 2;
                                break;
                            case 2:
                                setupgrade2.hp = 2;
                                break;
                            case 3:
                                setupgrade3.hp = 2;
                                break;
                        }
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
                switch (whitchupgrade)
                {
                    case 1:
                        setupgrade1.speed = 1;
                        break;
                    case 2:
                        setupgrade2.speed = 1;
                        break;
                    case 3:
                        setupgrade3.speed = 1;
                        break;
                }
            }

            if (rando == 1)
            {
                AddValue = "Damage: +1";
                switch (whitchupgrade)
                {
                    case 1:
                        setupgrade1.damage = 1;
                        break;
                    case 2:
                        setupgrade2.damage = 1;
                        break;
                    case 3:
                        setupgrade3.damage = 1;
                        break;
                }
            }
            if (rando == 2)
            {
                AddValue = "Hp: +1";
                switch (whitchupgrade)
                {
                    case 1:
                        setupgrade1.hp = 1;
                        break;
                    case 2:
                        setupgrade2.hp = 1;
                        break;
                    case 3:
                        setupgrade3.hp = 1;
                        break;
                }
            }
        }
        value = AddValue;
        
        return value;
    }
    
}
