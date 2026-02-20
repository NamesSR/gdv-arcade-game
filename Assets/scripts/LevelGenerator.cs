using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    
    public GameObject wallPrefab;
    public GameObject dotPrefab;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject waypointPrefab;
    public GameObject enemy2Prefab;
    public GameObject PowerOrbPrefab;
    public GameObject groundTilePrefab;
    private SpriteRenderer TileColor;
    public Vector3[] waypoints = new Vector3[12];
    
    private Transform t;
    int level;
    private int offsetwaypoint = 0;
    int gridx = 0;
    int index3;
    int index4;

    string[][] levelData = {new string[]{
        "###############",
        "#...B.........#",
        "#.##.##.W11..W12..#",
        "#.............#",
        "#..W15...E1W10.....#",
        "#....B........#",
        "#..W14.......W13..#",
        "#....P........#",
        "#...........B.#",
        "###############",
    },
      new string[]  {
        "#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b",
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
        "###############",
        "#W12...B.....W11..#",
        "#.##.##...E1W10W15.#",
        "#.............#",
        "#B..P....###..#",
        "#..###........#",
        "#W13..........W14.#",
        "#.#########...#",
        "#....B........#",
        "###############"
      }
    };

    
    void Start()
    {
       
        GenerateLevel(); 
    }


    void GenerateLevel()
    {

        level = 1;// UnityEngine.Random.Range(0, levelData.Length);
      

        for (int y = 0; y < levelData[level].Length; y++)
        {
            string line = levelData[level][y];
           
            string row = line;
            for (int x = 0; x < row.Length; x++)
            {
                char tile = row[x];
                Vector3 position = new Vector3(gridx  , -y, 0);

                switch (tile)
                {

                    case '#':
                       var wall = Instantiate(wallPrefab, position, Quaternion.identity);
                        TileColor = wall.GetComponent<SpriteRenderer>();
                        index3 = x + 1;
                        index4 = x + 2;
                        if (index3<= row.Length && index4 <= row.Length)
                        {
                            switch (row[x + 1])
                            {

                                case 'g':
                                    if (row[x] + 2 == 'd')
                                    {

                                    }
                                    else
                                    {
                                        TileColor.color = new Color(0f, 255f, 0f, 255f);
                                    }
                                    break;
                                case 'b':
                                    TileColor.color = new Color(0f, 0f, 0f, 255f);
                                    break;
                                case 'y':
                                    if (row[x] + 2 == 'd')
                                    {

                                    }
                                    else
                                    {

                                    }
                                    break;
                                case 'r':
                                    TileColor.color = new Color(255f, 0f, 0f, 255f);
                                    break;
                            }
                        }
                        gridx++;
                        break;
                    case '.':
                        Instantiate(dotPrefab, position, Quaternion.identity);
                        
                        gridx++;
                        break;
                    case 'P':
                        Instantiate(playerPrefab, position, Quaternion.identity);
                        gridx++;
                        break;
                    case 'E':
                        int esindex = row[x + 2] - '0';
                        switch(esindex){
                            case 1:
                                Instantiate(enemyPrefab, position, Quaternion.identity);
                                GameManager.Instance.enemycountAdd(1);
                                break;
                            case 2:
                                Instantiate(enemy2Prefab, position, Quaternion.identity);
                                GameManager.Instance.enemycountAdd(1);
                                break;
                        }
                        
                        gridx++;
                        break;
                    case 'W':
                        int SIndex = row[x + 2] - '0';
                        switch(SIndex){ 
                            case 1:
                                offsetwaypoint = 0;
                                break;
                            case 2:
                                offsetwaypoint = 6;
                                break;

                        }
                    

                        int index = row[x + 3] - '0';

                        Vector3 Position = new Vector3(gridx, -y, 0);
                        
                        Instantiate(dotPrefab, position, Quaternion.identity);
                        Instantiate(waypointPrefab, position, Quaternion.identity);

                        Debug.Log("check 1: " + "index: " + (index + offsetwaypoint)  + position);
                        
                            waypoints[index + offsetwaypoint] = position; // it works finaly
                                
                            
                        
                        // Debug.Log("check 2: " + "index: " + index + position);
                        gridx++;
                        break;
                    case 'B':
                        Instantiate(PowerOrbPrefab, position, Quaternion.identity);
                        GameManager.Instance.powerOrbCountAdd(1);
                        gridx++;
                        break;
        
                }
                if (tile != '#')
                {
                    if (!char.IsLower(tile) && !char.IsNumber(tile))
                    {
                        var groundTiles = Instantiate(groundTilePrefab, position, Quaternion.identity);
                        TileColor = groundTiles.GetComponent<SpriteRenderer>();

                        index3 = x + 1;
                        index4 = x + 2;
                        if (index3 <= row.Length && index4 <= row.Length)
                        {
                            switch (row[x + 1])
                            {

                                case 'g':
                                    if (row[x] + 2 == 'd')
                                    {

                                    }
                                    else
                                    {
                                        TileColor.color = new Color(0f, 255f, 0f, 255f);
                                    }
                                    break;
                                case 'b':
                                    TileColor.color = new Color(0f, 0f, 0f, 0f);
                                    break;
                                case 'y':
                                    if (row[x] + 2 == 'd')
                                    {

                                    }
                                    else
                                    {

                                    }
                                    break;
                                case 'r':
                                    TileColor.color = new Color(255f, 0f, 0f, 255f);
                                    break;

                            }
                        }
                    }
                }
            }
           
            gridx = 0;
        }
       
    }
}
