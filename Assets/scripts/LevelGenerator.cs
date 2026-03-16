using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    
    public GameObject wallPrefab;
    // public GameObject dotPrefab;
    WayPoints way;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject waypointPrefab;
    public GameObject enemy2Prefab;
    public GameObject PowerOrbPrefab;
    public GameObject groundTilePrefab;
    public GameObject NextlevelTilePrefab;
    private SpriteRenderer TileColor;
    public Vector3[] waypoints = new Vector3[24];
    int a = 0;
    private Transform t;
    int Genlevel;
    private int offsetwaypoint = 0;
    int gridx = 0;
    int index3;
    int index4;
    int waypointoffseter;

    string[][] levelData = {new string[]{
        "6#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b",
        "#b...Bg.........#b",
        "#b.#b#b.#b#b.Wg11..Wg12..#b",
        "#b.............#b",
        "#b..Wg15...Eg1Wg10.....#b",
        "#b....Bg........#b",
        "#b..Wg14.......Wg13..#b",
        "#b....Pg........#b",
        "#b...........Bg.#b",
        "#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b",
    },
      new string[]  {
        "6#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b",
        "#b.g.g.g.g.g.gWg25.g.g.gWg24.g.g#b",
        "#b.g#b#b.g#b#b.g.gBg.g.g.g.g#b",
        "#bWg14.g.g.g.g.g.g.g.gWg15.g.g.g#b",
        "#b.g.g.g.gPg.g.g.g.g.gWg23.gWg22#b",
        "#bBg.g.g.g.g.g.g.gEg1Wg10.gWg11.g#b",
        "#b.g.g.g.g.gEg2Wg20.g.g.g.g.gWg21#b",
        "#b.g#b#b#b#b#b#b#b#b#b.g.g.g#b",
        "#bWg13.g.g.g.g.g.gBg.g.g.gWg12.g#b",
        "#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b",


    },
      new string[]
      {
        "6#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b",
        "#bWg12...Bg.....Wg11..#b",
        "#b.#b#b.#b#b...Eg1Wg10Wg15.#b",
        "#b.............#b",
        "#bBg..Pg....#b#b#b..#b",
        "#b..#b#b#b........#b",
        "#bWg13..........Wg14.#b",
        "#b.#b#b#b#b#b#b#b#b#b...#b",
        "#b....Bg........#b",
        "#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b"
      },
      new string[] 
      {
       "0#g#g#g#g#g#g#g#g#g#g#g#g#g#gd#g#g#g#g#g#g#gd#gd#gd#g#g#g#g#g#g#gd#gd",
"#gTgTgTg#gdTgTgTg#gdTgTgTgTg#gdTgTg#gdTgTgTgTg#gdTgTgTg#gdTgTgTgTg#gd",
"#gTgTg#gd#gd#gdTgTgTg#gdTgTgTgTgTg#gdTgTgTg#gdTgTgTgTgTgTg#gdTgTgTg#g",
"#g#gdTgTg#gdTgTgTgTgTgTgTg#gdTgTgTgTgTg#gdTgTgTgTgTg#gdTgTgTg#gdTy#y",
"#gTgTgTgTgTgTgTg#gdTgTg#gd#gd#gdTgTyTyTyyTyTyTgTg#gdTgTgTgTgTgTyTy#y",
"#gTgTgTgTgTg#gdTgTg#gdTgTg#gdTgTyTyTyTyTyTyTyTgTgTgTgTg#gdTyTyTyy#y",
"#yTyyTyTg#gdTgTgTgTgTgTgTyTyTyTyTyyTyTyTyyTyTyTyTyTyTyTgTgTyTyTyFyd",
"#yTyTyyTyTgTgTgTgTgTyTyTyTyyTyTyyTyTyTyyTyTyTyyTyTyyTyTyTyTyTyTyyTyyFyd",
"#yPyTyTyTyTyyTgTgTyTyTyyTyTyTyTyTyTyyTyTyTyTyTyTyTyyTyTyyTyTyyTyTyyFyd",
"#yTyTyTyyTyTyTyTyTyTyyTyTyTyyTyTyyTgTgTgTgTgTyTyyTyTyTyyTyTyTyTyyTyyFyd",
"#yTyyTyTyTyTyyTyTyyTyTyTyyTyTyTyTgTgTgTgTg#gdTgTyTyTyTyTgTgTyTyTyFyd",
"#g#gdTgTyTyyTyTyTyyTyTyTyTgTg#gdTgTg#gdTgTgTgTgTgTgTgTgTg#gdTyTyTyy#y",
"#gTgTgTgTyTyTyTyTyTg#gdTgTgTgTg#gd#gd#gdTgTgTgTgTg#gdTgTgTgTgTyTy#y",
"#gTg#gdTgTgTgTyTyyTgTgTgTgTgTgTgTg#gdTgTg#gdTgTgTgTgTgTgTgTgTgTy#y",
"#g#gd#gd#gdTgTgTgTgTgTg#gdTgTgTg#gdTgTgTgTgTgTgTg#gdTgTgTg#gdTg#gdTg#g",
"#gTg#gdTgTgTg#gdTg#gdTgTgTg#gdTgTgTgTg#gdTgTgTg#gd#gd#gdTg#gdTgTgTgTg#g",
"#g#g#g#g#g#g#g#g#g#g#g#gd#gd#gd#g#g#g#g#g#g#g#g#gd#g#g#g#g#g#gd#g#g"
      },
      new string[]
      {
          "6#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#bTyTyTyTyyTyTyTyyTyTyByTyTyTyyWy13TyTyTyTyWy12TyTyTyTyTyTyTyTy#b#d",
"#d#bTyTyTyyTy#bTyTyTyTyyTy#bTyTyTyTyTyy#bTyTyyTyTyTy#bTyyTyTyTy#b#d",
"#d#bTyyTyTy#b#b#bTyTyTy#b#b#bTyTyyTy#b#b#bTyTyTy#b#b#bTyTyyTy#b#d",
"#d#bTyTyTyTy#bTyTyyTyTyTy#bTyTyyTyTyTy#bTyTyTyyTyTy#bTyTyTyTy#b#d",
"#b#bTyyTyTyTyyTyTyTyTyTyTyTyyTyTyTyTyTyTyTyyTyTyTyyTyTyTyTyTyTy#b#b",
"#yTyTyTyTyTyTyTyTyTyyTyTyTyTyTyTyTyyTyTyTyTyyTyTyTyTyTyTyyTyTyTyFyd",
"#rPrTrTrTrTrTrTrTrTrWr15TrTrTrTrWr14TrBrTrTrWr11TrTrTrTrWr10Er1TrTrTrFyd",
"#rTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrTrFyd",
"#yTyTyTyyTyTyTyTyTyyTyTyTyTyTyTyyTyTyTyyTyTyTyTyTyTyyTyTyTyyTyTyTyyFyd",
"#b#bTyTyTyTyyTyTyTyTyTyTyyTyTyTyTyTyyTyTyTyTyyTyTyTyTyTyTyTyTy#b#b",
"#d#bTyTyyTyTy#bTyTyTyyTyTy#bTyTyyTyTyTy#bTyTyTyTyyTy#bTyTyTyyTy#b#d",
"#d#bTyTyTy#b#b#bTyTyTy#b#b#bTyTyTy#b#b#bTyTyTy#b#b#bTyTyTy#b#d",
"#d#bTyyTyTyTyy#bTyTyTyTyyTy#bTyTyyTyTyyTy#bTyTyTyyTyTy#bTyTyyByTyy#b#d",
"#d#bTyTyTyTyTyTyTyyTyTyTyTyTyTyTyTyTyyTyTyyTyTyTyTyyTyTyTyTyTy#b#d",
"#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d"

      },
      new string[]
      {
          "6#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d,",
"#d#bTyTyTyTyyTyTyTyTyTyTyyTyTyTyTyyTyTyTyTyTyTyyTyTyTyTyTyTyyTy#b#d,",
"#d#bTyyTrTrTrTrTrTrTrTrTrTyTyy#b#b#bTyTyTrWr21TrTrTrTrTrTrWr22Ty#b#d,",
"#d#bTyWr13TrTrWr14TrTrWr15Er1TrTyyTy#b#b#bTyTyyTrTrTrTrBrTrTrTrTrTy#b#d,",
"#d#bTyTrTrTyyTyTyTyTyWr10TrTyTy#b#b#bTyTyTrTrTyTyTyTyTyTrTrTy#b#d,",
"#b#bTyTrTrTy#b#bTyTyyTrTrTyTyyTyTyTyTyTyTrTrTyyTy#b#bTyyTrTrTy#b#b,",
"#yTyTyyTrTrTy#b#bTyTyTrTrTyTyTyTyTyyTyTyTrTrTyTy#b#bTyTrTrTyTyFyd,",
"#rTrTrTrTrTy#b#bTyTyyTrTrTrTrTrBrTrTrTrTrTrTyTyy#b#bTyTrTrTrTrFyd,",
"#rTrPrTrTrTyy#b#bTyTyTrTrTrTrTrTrTrTrTrTrTrTyTy#b#bTyTrWr23TrTrFyd,",
"#yTyyTyTrTrTy#b#bTyTyTrTrTyTyTyTyTyTyTyTrTrTyTy#b#bTyyTrTrTyTyFyd,",
"#b#bTyTrTrTy#b#bTyyTyTrTrTyTyTyyTyTyTyyTyTrTrTyyTy#b#bTyTrTrTy#b#b,",
"#d#bTyTrTrTyTyyTyTyTyTrTrTyyTy#b#b#bTyTyTrWr20TyTyTyTyTyTrTrTyy#b#d,",
"#d#bTyyWr12TrTrTrTrTrTrWr11TrTyTy#b#b#bTyTyyTrEr2TrWr25TrTrTrTrWr24Ty#b#d,",
"#d#bTyTrTrTrTrBrTrTrTrTrTyTy#b#b#bTyTyTrTrTrTrTrTrTrTrTrTy#b#d,",
"#d#bTyTyTyTyyTyTyTyTyyTyTyTyTyyTyTyTyTyyTyTyTyTyyTyTyTyTyTyyTyTy#b#d,",
"#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d,",
"#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d,"
      }
     
    };

    
    void Start()
    {
       
        //GenerateLevel(); 
    }


    public void GenerateLevel(int l)
    {
        Debug.Log(2 + GameManager.Instance.level);
        GameManager.Instance.level++;
        gridx = 0;
        if (l == 0)
        {
            Genlevel = 3;// UnityEngine.Random.Range(0, levelData.Length);
        }
        else
        {
            if(GameManager.Instance.level < 4)
            {
            Genlevel = 2 + GameManager.Instance.level; // UnityEngine.Random.Range(0, levelData.Length - 1);

            }
            else
            {
              Genlevel = UnityEngine.Random.Range(0, levelData.Length - 1);
            }
        }

        for (int y = 0; y < levelData[Genlevel].Length; y++)
        {
            string line = levelData[Genlevel][y];

            string row = line;
            for (int x = 0; x < row.Length; x++)
            {
                if(x == 0 && y == 0)
                {
                     waypointoffseter = row[0] - '0';
                    Debug.Log(waypointoffseter);
                    x++;
                }
                char tile = row[x];
                Vector3 position = new Vector3(gridx, -y, 0);

                switch (tile)
                {

                    case '#':
                        var wall = Instantiate(wallPrefab, position, Quaternion.identity, this.transform);
                        TileColor = wall.GetComponent<SpriteRenderer>();
                        index3 = x + 1;
                        index4 = x + 2;
                       // Debug.Log($"index 1: {index3}|| index 2: {index4} || row.Leagth: {row.Length} ");
                        if (index3 <= row.Length && index4 <= row.Length)
                            if (index4 == row.Length)
                            {
                                a = -1; 
                            }
                        {
                            switch (row[x + 1])
                            {

                                case 'g':
                                    if (row[x + 2 + a] == 'd')
                                    {
                                        TileColor.color = new Color32(39, 102, 50, 255);
                                    }
                                    else
                                    {
                                        TileColor.color = new Color32(74, 155, 86, 255);
                                    }
                                    break;
                                case 'b':
                                    TileColor.color = new Color32(47, 47, 47, 255);
                                    break;
                                case 'y':
                                    if (row[x + 2 + a] == 'd')
                                    {
                                        TileColor.color = new Color32(68, 68, 68, 255);
                                    }
                                    else if (row[x + 2 + a]  == 'y')
                                    {
                                        TileColor.color = new Color32(102, 102, 102, 255);
                                    }

                                    else
                                    {
                                        TileColor.color = new Color32(136, 136, 136, 255);
                                    }
                                    break;
                                case 'r':
                                    TileColor.color = new Color32(173, 56, 54, 255);
                                    break;
                            }
                        }
                        gridx++;
                        a = 0;
                        break;
                    case '.':
                       

                        gridx++;
                        break;
                    case 'P':
                        Instantiate(playerPrefab, position, Quaternion.identity, this.transform);
                        gridx++;
                        break;
                    case 'E':
                        int esindex = row[x + 2] - '0';
                        switch (esindex)
                        {
                            case 1:
                                var E1 = Instantiate(enemyPrefab, position, Quaternion.identity, this.transform);
                                way = E1.GetComponent<WayPoints>();
                                 way.offset = 0;
                                way.MaxOffset = waypointoffseter;
                                GameManager.Instance.enemycountAdd(1);
                                break;
                            case 2:
                                var E2 = Instantiate(enemy2Prefab, position, Quaternion.identity, this.transform);
                                way = E2.GetComponent<WayPoints>();
                                way.offset = waypointoffseter;
                                way.MaxOffset = waypointoffseter;
                                GameManager.Instance.enemycountAdd(1);
                                break;
                            case 3:
                                var E3 = Instantiate(enemyPrefab, position, Quaternion.identity, this.transform);
                                way = E3.GetComponent<WayPoints>();
                                way.offset = waypointoffseter * 2;
                                way.MaxOffset = waypointoffseter;
                                GameManager.Instance.enemycountAdd(1);
                                break;
                            case 4:
                                var E4 = Instantiate(enemy2Prefab, position, Quaternion.identity, this.transform);
                                way = E4.GetComponent<WayPoints>();
                                way.offset = waypointoffseter * 3;
                                way.MaxOffset = waypointoffseter;
                                GameManager.Instance.enemycountAdd(1);
                                break;
                        }

                        gridx++;
                        break;
                    case 'W':
                        int SIndex = row[x + 2] - '0';
                        switch (SIndex)
                        {
                            case 1:
                                offsetwaypoint = 0;
                                break;
                            case 2:
                                offsetwaypoint = waypointoffseter;
                                break;
                            case 3:
                                offsetwaypoint = waypointoffseter * 2;
                                break;
                            case 4:
                                offsetwaypoint = waypointoffseter * 3;
                                break;

                        }


                        int index = row[x + 3] - '0';

                        Vector3 Position = new Vector3(gridx, -y, 0);

                        //Instantiate(dotPrefab, position, Quaternion.identity, this.transform);
                        Instantiate(waypointPrefab, position, Quaternion.identity, this.transform);

                       // Debug.Log("check 1: " + "index: " + (index + offsetwaypoint) + position);

                        waypoints[index + offsetwaypoint] = position; // it works finaly



                        // Debug.Log("check 2: " + "index: " + index + position);
                        gridx++;
                        break;
                    case 'B':
                        Instantiate(PowerOrbPrefab, position, Quaternion.identity, this.transform);
                        GameManager.Instance.powerOrbCountAdd(1);
                        gridx++;
                        break;
                    case 'F':
                        Instantiate(NextlevelTilePrefab, position, Quaternion.identity, this.transform);
                        gridx++;
                        break;
                    case 'T':
                        
                        gridx++;
                        break;

                     }
                if (tile != '#' && tile != 'F')
                {
                    if (!char.IsLower(tile) && !char.IsNumber(tile))
                    {
                        var groundTiles = Instantiate(groundTilePrefab, position, Quaternion.identity, this.transform);
                        TileColor = groundTiles.GetComponent<SpriteRenderer>();

                        index3 = x + 1;
                        index4 = x + 2;
                        if (index3 < row.Length && index4 < row.Length)
                        {
                            if (index4 == row.Length)
                            {
                                a = -1;
                            }
                            switch (row[x + 1])
                            {

                                case 'g':
                                    if (row[x + 2 + a]  == 'd')
                                    {
                                        TileColor.color = new Color32(39, 102, 50, 255);
                                    }
                                    else
                                    {
                                        TileColor.color = new Color32(74, 155, 86, 255);
                                    }
                                    break;
                                case 'b':
                                    TileColor.color = new Color32(47, 47, 47, 255);
                                    break;
                                case 'y':
                                    if (row[x + 2 + a]  == 'd')
                                    {
                                        TileColor.color = new Color32(68, 68, 68, 255);
                                    }
                                    else if (row[x + 2 + a] == 'y')
                                    {
                                        TileColor.color = new Color32(102, 102, 102, 255);
                                    }

                                    else
                                    {
                                        TileColor.color = new Color32(136, 136, 136, 255);
                                    }
                                    break;
                                case 'r':
                                    TileColor.color = new Color32(173, 56, 54, 255);
                                    break;

                            }
                            
                        }
                        a = 0;
                    }
                }
            }

            gridx = 0;
        }

    }
    public void destroyLevel()
    {

        foreach (Transform t in this.transform)
        {
            if (t != null)
            {
                Destroy(t.gameObject);

            }
        }
    }
    
}