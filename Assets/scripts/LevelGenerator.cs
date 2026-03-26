using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.Rendering.DebugUI.Table;

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
    public Node nodePrefab;
    public List<Node> nodeList;
    private SpriteRenderer TileColor;
    GameObject sd;
    
    public static event Action startgame;
    

    int a = 0;
    private Transform t;
    int Genlevel;
    private int offsetwaypoint = 0;
    int gridx = 0;
    int index3;
    int index4;
    int waypointoffseter;
    Node n;

    string[][] levelData = {new string[]
      {
       "#g#g#g#g#g#g#g#g#g#g#g#g#g#gd#g#g#g#g#g#g#gd#gd#gd#g#g#g#g#g#g#gd#gd",
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
          "#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
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
          "#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#bTyTyTyTyyTyTyTyTyTyTyyTyTyTyTyyTyTyTyTyTyTyyTyTyTyTyTyTyyTy#b#d",
"#d#bTyyTrTrTrTrTrTrTrTrTrTyTyy#b#b#bTyTyTrWr21TrTrTrTrTrTrWr22Ty#b#d",
"#d#bTyWr43TrTrWr44TrTrWr45Er3TrTyyTy#b#b#bTyTyyTrTrTrTrBrTrTrTrTrTy#b#d",
"#d#bTyTrTrTyyTyTyTyTyWr40TrTyTy#b#b#bTyTyTrTrTyTyTyTyTyTrTrTy#b#d",
"#b#bTyTrTrTy#b#bTyTyyTrTrTyTyyTyTyTyTyTyTrTrTyyTy#b#bTyyTrTrTy#b#b",
"#yTyTyyTrTrTy#b#bTyTyTrTrTyTyTyTyTyyTyTyTrTrTyTy#b#bTyTrTrTyTyFyd",
"#rTrTrTrTrTy#b#bTyTyyTrTrTrTrTrBrTrTrTrTrTrTyTyy#b#bTyTrTrTrTrFyd",
"#rTrPrTrTrTyy#b#bTyTyTrTrTrTrTrTrTrTrTrTrTrTyTy#b#bTyTrWr23TrTrFyd",
"#yTyyTyTrTrTy#b#bTyTyTrTrTyTyTyTyTyTyTyTrTrTyTy#b#bTyyTrTrTyTyFyd",
"#b#bTyTrTrTy#b#bTyyTyTrTrTyTyTyyTyTyTyyTyTrTrTyyTy#b#bTyTrTrTy#b#b",
"#d#bTyTrTrTyTyyTyTyTyTrTrTyyTy#b#b#bTyTyTrWr20TyTyTyTyTyTrTrTyy#b#d",
"#d#bTyyWr42TrTrTrTrTrTrWr41TrTyTy#b#b#bTyTyyTrEr2TrWr25TrTrTrTrWr24Ty#b#d",
"#d#bTyTrTrTrTrBrTrTrTrTrTyTy#b#b#bTyTyTrTrTrTrTrTrTrTrTrTy#b#d",
"#d#bTyTyTyTyyTyTyTyTyyTyTyTyTyyTyTyTyTyyTyTyTyTyyTyTyTyTyTyyTyTy#b#d",
"#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d"
      }

    };





    void Start()
    {
       
        //GenerateLevel(); 
    }
    


    public void GenerateLevel(int l)
    {
        
        GameManager.Instance.level++;
        Debug.Log(GameManager.Instance.level);
        gridx = 0;
        if (l == 0)
        {
            Genlevel = 1;// UnityEngine.Random.Range(0, levelData.Length);
        }
        else
        {
            if(GameManager.Instance.level < 4)
            {
            Genlevel = GameManager.Instance.level - 1; // UnityEngine.Random.Range(0, levelData.Length - 1);

            }
            else
            {
              Genlevel = UnityEngine.Random.Range(0, levelData.Length);
            }
        }

        for (int y = 0; y < levelData[Genlevel].Length; y++)
        {
            string line = levelData[Genlevel][y];

            string row = line;
            for (int x = 0; x < row.Length; x++)
            {
                
                char tile = row[x];
                Vector3 position = new Vector3(gridx, -y, 0);
                Vector3 nodePos = new Vector3(gridx + 0.5f, -y + -0.5f ,0);

                switch (tile)
                {

                    case '#':
                        var wall = Instantiate(wallPrefab, position, Quaternion.identity, this.transform);
                        TileColor = wall.GetComponent<SpriteRenderer>();
                        mapCollerSet(row, x);
                        gridx++;
                        a = 0;
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
                                enemyOffsetSet(position, enemyPrefab, 1, false, y);
                                break;
                            case 2:
                                enemyOffsetSet(position,  enemy2Prefab, 2, true,y);
                                GameManager.Instance.ishunterinScene = true;
                                break;
                            case 3:
                                enemyOffsetSet(position,   enemyPrefab, 3, true, y);
                                break;
                            case 4:
                                enemyOffsetSet(position,   enemyPrefab, 4, false, y);                                
                                break;
                        }
                        
                        gridx++;
                        break;
                    case 'W':



                        
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
                        mapCollerSet(row, x);
                        if(tile != 'E')
                        {
                            n = Instantiate(nodePrefab, position, Quaternion.identity, this.transform);
                            nodeList.Add(n);
                        }



                        a = 0;
                    }
                }
            }

            gridx = 0;
        }
        ConnectNodes();
        startgame.Invoke();
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
    void mapCollerSet(string row, int x)
    {
        index3 = x + 1;
        index4 = x + 2;
        // Debug.Log($"index 1: {index3}|| index 2: {index4} || row.Leagth: {row.Length} ");
        if (index3 < row.Length && index4 <= row.Length)
        {
            if (index4 == row.Length)
            {
                a = -1;
            }
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
    void enemyOffsetSet(Vector3 position, GameObject enemy, int whichEnemy, bool chashing, int y)
    {
        
            sd = Instantiate(enemy, position, Quaternion.identity, this.transform);
            way = sd.GetComponent<WayPoints>();
            n = Instantiate(nodePrefab, position, Quaternion.identity, this.transform);
            nodeList.Add(n);
            way.currentNode = n;
            
            way.whichEnemy = whichEnemy;
            way.chase = false;
            GameManager.Instance.enemycountAdd(1);
        
       /* else
        {
            if(whichEnemy == 2) 
            {
                var chasingE = Instantiate(enemy, position, Quaternion.identity, this.transform);
                way = chasingE.GetComponent<WayPoints>();
                way.chase = true;
            }
            if(whichEnemy == 3)
            {
                var chasdogE = Instantiate(enemy, position, Quaternion.identity, this.transform);
                way = chasdogE.GetComponent<WayPoints>();
                way.chaseDog = true;

            }
            
               
                way.whichEnemy = whichEnemy;
                GameManager.Instance.enemycountAdd(1);
        }*/
    }
    public void RemoveNodes()
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            
            
                nodeList.RemoveAt(i);
                i--;
            
        }
    }
    public void ConnectNodes()
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            for (int j = i + 1; j < nodeList.Count; j++)
            {
                Vector2 a = nodeList[i].transform.position;
                Vector2 b = nodeList[j].transform.position;

                float dx = Mathf.Abs(a.x - b.x);
                float dy = Mathf.Abs(a.y - b.y);

                // Only allow straight connections (no diagonals)
                bool isHorizontal = dy < 0.1f && dx <= 1.0f;
                bool isVertical = dx < 0.1f && dy <= 1.0f;

                if (isHorizontal || isVertical)
                {
                    nodeList[i].connections.Add(nodeList[j]);
                    nodeList[j].connections.Add(nodeList[i]);
                }
            }
        }
    }

}