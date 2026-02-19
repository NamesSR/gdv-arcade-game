using System;
using System.Linq;
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

    public Vector3[] waypoints = new Vector3[12];
    
    private Transform t;
    int level;
    private int offsetwaypoint = 0;
    int gridx = 0;
    

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
        "###############",
        "#......W25...W24..#",
        "#.##.##..B....#",
        "#W14........W15...#",
        "#....P.....W23.W22#",
        "#B.......E1W10.W11.#",
        "#.....E2W20.....W21#",
        "#.#########...#",
        "#W13......B...W12.#",
        "###############",


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

        level = UnityEngine.Random.Range(0, levelData.Length);
      

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
                        Instantiate(wallPrefab, position, Quaternion.identity);
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
                        int esindex = row[x + 1] - '0';
                        switch(esindex){
                            case 1:
                                Instantiate(enemyPrefab, position, Quaternion.identity);
                                break;
                            case 2:
                                Instantiate(enemy2Prefab, position, Quaternion.identity);
                                break;
                        }
                        
                        gridx++;
                        break;
                    case 'W':
                        int SIndex = row[x + 1] - '0';
                        switch(SIndex){ 
                            case 1:
                                offsetwaypoint = 0;
                                break;
                            case 2:
                                offsetwaypoint = 6;
                                break;

                        }
                    

                        int index = row[x + 2] - '0';

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
                        // + PowerOrb Amount
                        gridx++;
                        break;





                }
                
            }
           
            gridx = 0;
        }
       
    }
}
