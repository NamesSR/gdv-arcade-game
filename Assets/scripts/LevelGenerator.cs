using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject dotPrefab;
    public GameObject playerPrefab;
    int s;

    string[][] levelData = {new string[]{
        "###############",
        "#.............#",
        "#.##.##.......#",
        "#.............#",
        "#.............#",
        "#.............#",
        "#.............#",
        "#....P........#",
        "#.............#",
        "###############",
    },
      new string[]  {
        "###############",
        "#.............#",
        "#.##.##.......#",
        "#.............#",
        "#....P........#",
        "#.............#",
        "#.............#",
        "#.#########...#",
        "#.............#",
        "###############",


    },
      new string[]
      {
        "###############",
        "#.............#",
        "#.##.##.......#",
        "#.............#",
        "#...P....###..#",
        "#..###........#",
        "#.............#",
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
       s = Random.Range(0, levelData.Length);
      

        for (int y = 0; y < levelData[s].Length; y++)
        {
            string line = levelData[s][y];
           
            string row = line;
            for (int x = 0; x < row.Length; x++)
            {
                char tile = row[x];
                Vector3 position = new Vector3(x, -y, 0);

                switch (tile)
                {

                    case '#':
                        Instantiate(wallPrefab, position, Quaternion.identity);
                        break;
                    case '.':
                        Instantiate(dotPrefab, position, Quaternion.identity);
                        break;
                    case 'P':
                        Instantiate(playerPrefab, position, Quaternion.identity);
                        break;
                    
       

                }
            }


        }
       
    }
}
