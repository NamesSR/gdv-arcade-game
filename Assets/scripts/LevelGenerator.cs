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
        "#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b",
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
        "#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b",
        "#bWg12...Bg.....Wg11..#b",
        "#b.#b#b.#b#b...Eg1Wg10Wg15.#b",
        "#b.............#b",
        "#bBg..Pg....#b#b#b..#b",
        "#b..#b#b#b........#b",
        "#bWg13..........Wg14.#b",
        "#b.#b#b#b#b#b#b#b#b#b...#b",
        "#b....Bg........#b",
        "#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b"
      }
    };

    
    void Start()
    {
       
        //GenerateLevel(); 
    }


    public void GenerateLevel(int l)
    {
        GameManager.Instance.level++;
        gridx = 0;
        if (l == 0) 
        { 
            level = 1;// UnityEngine.Random.Range(0, levelData.Length);
        }
        else
        {
            level = UnityEngine.Random.Range(0, levelData.Length);
        }


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
                       var wall = Instantiate(wallPrefab, position, Quaternion.identity, this.transform);
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
                        Instantiate(dotPrefab, position, Quaternion.identity, this.transform);
                        
                        gridx++;
                        break;
                    case 'P':
                        Instantiate(playerPrefab, position, Quaternion.identity, this.transform);
                        gridx++;
                        break;
                    case 'E':
                        int esindex = row[x + 2] - '0';
                        switch(esindex){
                            case 1:
                                Instantiate(enemyPrefab, position, Quaternion.identity, this.transform);
                                GameManager.Instance.enemycountAdd(1);
                                break;
                            case 2:
                                Instantiate(enemy2Prefab, position, Quaternion.identity, this.transform);
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
                        
                        Instantiate(dotPrefab, position, Quaternion.identity, this.transform);
                        Instantiate(waypointPrefab, position, Quaternion.identity, this.transform);

                        Debug.Log("check 1: " + "index: " + (index + offsetwaypoint)  + position);
                        
                            waypoints[index + offsetwaypoint] = position; // it works finaly
                                
                            
                        
                        // Debug.Log("check 2: " + "index: " + index + position);
                        gridx++;
                        break;
                    case 'B':
                        Instantiate(PowerOrbPrefab, position, Quaternion.identity, this.transform);
                        GameManager.Instance.powerOrbCountAdd(1);
                        gridx++;
                        break;
        
                }
                if (tile != '#')
                {
                    if (!char.IsLower(tile) && !char.IsNumber(tile))
                    {
                        var groundTiles = Instantiate(groundTilePrefab, position, Quaternion.identity, this.transform);
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
    public void destroyLevel()
    {
        
        foreach (Transform t in this.transform)
        {
            if(t != null)
            {
             Destroy(t.gameObject);

            }
        }
    }
}
