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
    
    public Vector3[] waypoints = new Vector3[6];
    private Transform t;
    int level;
    
    int gridx = 0;
    

    string[][] levelData = {new string[]{
        "###############",
        "#.............#",
        "#.##.##.W1..W2..#",
        "#.............#",
        "#..W5...EW0.....#",
        "#.............#",
        "#..W4.......W3..#",
        "#....P........#",
        "#.............#",
        "###############",
    },
      new string[]  {
        "###############",
        "#.............#",
        "#.##.##.......#",
        "#W4........W5...#",
        "#....P........#",
        "#........EW0.W1.#",
        "#.............#",
        "#.#########...#",
        "#W3..........W2.#",
        "###############",


    },
      new string[]
      {
        "###############",
        "#W2.........W1..#",
        "#.##.##...EW0W5.#",
        "#.............#",
        "#...P....###..#",
        "#..###........#",
        "#W3..........W4.#",
        "#.#########...#",
        "#.............#",
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
                        Instantiate(enemyPrefab, position, Quaternion.identity);
                        gridx++;
                        break;
                    case 'W':
                        int index = row[x + 1] - '0';   

                        Vector3 Position = new Vector3(gridx, -y, 0);

                       
                        Instantiate(dotPrefab, position , Quaternion.identity);
                        Instantiate(waypointPrefab, position , Quaternion.identity);
                       
                        Debug.Log("check 1: " + "index: " + index + position);
                       
                            waypoints[index] = position; // it works finaly

                        // Debug.Log("check 2: " + "index: " + index + position);
                        gridx++;
                        break;
                    


                        

                }
                
            }
           
            gridx = 0;
        }
       
    }
}
