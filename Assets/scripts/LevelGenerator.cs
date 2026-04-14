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
   
    public GameObject enemy2Prefab;
    public GameObject PowerOrbPrefab;
    public GameObject groundTilePrefab;
    public GameObject NextlevelTilePrefab;
    public GameObject BossPrefab;
    public Node nodePrefab;
    public GameObject abiltyprefab;
    public List<Node> nodeList;
    private SpriteRenderer TileColor;
    GameObject sd;
    
    List<int> shop = new List<int> { 0, 1, 2, 3};

    public static event Action startgame;


    int a = 0;
    private Transform t;
    int Genlevel;
    
    int gridx = 0;
    int index3;
    int index4;
    int waypointoffseter;
    Node n;

    string[][] levelData = {new string[]
      {
       "k#g#g#g#g#g#g#g#g#g#g#g#g#g#gd#g#g#g#g#g#g#gd#gd#gd#g#g#g#g#g#g#gd#gd",
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
          ",#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
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
          ",#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
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
      },
      new string[]
      {
          ",#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#bTyTyTyTyTyTyTyTyTyTyTy#bTyTyTy#bTyTyTy#bTyTyTyTyTyByTy#b#d",
"#d#bTyTyEy1TyTyTyTyTyyTyTyTy#bTyTyTy#bTyTyyTy#bTyTyTyTyTyyTyTy#b#d",
"#d#bTyTyyTyTy#bTyTy#b#bTyTy#bTyTyyTy#bTyTy#b#bTyTy#b#b#b#b#b#b#d",
"#d#bTyTy#b#b#bTyTyTy#bTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTy#b#d",
"#b#bTyTyTyTyTyTyTyTy#bTyTyTyTyTyTyTyTyTyTyTyTyEy2TyTyyTyTyTy#b#b",
"#yTyTyTyTyTyTyTyByTy#bTyTy#bTyTyTyy#bTyTyTyTyTyTy#bTyTyTyTyTyFyd",
"#yTyTyTyTy#bTyTyTyTy#bTyTy#bTyTyTy#b#b#b#bTyTyTy#bTyTyTyTyTyFyd",
"#yPyTyTyTy#b#bTyTy#b#bTyTy#b#b#b#b#bTyTy#b#b#b#b#bTyTyTyTyTyFyd",
"#yTyTyTyTyTyTyTyTyTyyTyTyTyTyTyTyTyTyTyTyy#bTyTyTyTyTyTyTyyTyTyFyd",
"#b#bTyTyTyTyTyTyTyTyTyTyTyTyTyTyyTyTyTyTy#bTyTyTyTyTyTyTyTy#b#b",
"#d#b#b#b#bTyTy#b#b#b#bTyTy#b#b#b#b#bTyTy#bTyTy#b#b#b#bTyTy#b#d",
"#d#bTyyTy#bTyTy#bTyTy#bTyTy#bTyTyTy#bTyTyTyTyTy#bTyTyyTyTyTy#b#d",
"#d#bTyTyTyTyTyyTyTyTyEy4TyTy#bTyByTyTyTyTyTyTyTyTyTyTyEy3TyTy#b#d",
"#d#bTyTyTyTyTyTyTyTyTyTyTy#bTyyTyTyTyTyTyTyyTyTyTyTyTyTyTyTy#b#d",
"#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d#d",

      },
      new string[]
      { "q#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#bTyTyTyTyTyTy#b#bTyTy#b#y#y#y#y#y#bTyTyTyTyTyTyTyTyTyTy#b#d",
"#d#bTyTyTyy#bByTyTyTyTyTy#b#yy#yUy#y#y#bTyTyTyyTy#b#b#bTyTyyTy#b#d",
"#d#bTy#b#b#bTyTyTyTyyTyTy#b#y#y#y#y#y#bTyTyTyTyTyTy#bTyTyTy#b#d",
"#d#bTyTyTyTyTyTyTy#b#b#b#bTyTyTyTyTyy#b#b#b#bTyTyTyTyTyTyTy#b#d",
"#d#bTy#b#b#bTyTyTy#bTyTyTyTyTyTyTyTyTyTyTy#bTyTyTyTyyTy#b#b#b#d",
"#b#bTy#bTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTy#b#bTyBy#bTy#b#b",
"#yTyTyTyTyTyTyy#bTyTyTyTyTyTyTyTyTyTyTyyTyTyTyTyTyTyTyTyTyTyTyFyd",
"#yPyTyTyTyTyTy#bTy#bTyTyTyyTyTyTyTyTyTyTyTy#bTyTyyTyTyTyTyTyTyFyd",
"#yTyTyTyTy#b#b#bTy#b#bTyTyTy#b#b#bTyTyTy#b#bTyTyTyTy#b#bTyTyFyd",
"#b#bTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTy#bTy#b#b",
"#d#bTy#bTyTyTyTyTyTy#bTyTyTy#b#bTyTyy#bTyTyTyTyTy#bTyTyTyTy#b#d",
"#d#bTy#b#b#bTyTyTy#b#bTyTyTyTy#bTyTy#b#bTyTy#b#b#bTyTyTyyTy#b#d",
"#d#bTyTyTyyTyTyTyTyTy#bTyTyTyTy#bTyTy#b#bTyTy#bTyTyTyTyTyTy#b#d",
"#d#bTyTyTyTy#bTyTyTy#b#bTyTyTyByTyTyTyTyTyTyTyyTyTyTy#b#bTy#b#d",
"#d#bTyTyTy#b#b#bTyTyTyyTyTyTyTyTy#b#bTyTyTyTyTyTyTyTy#b#bTy#b#d",
"#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",


    }, new string[] {",#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#bTyTyTyTy#b#bTyTyTy#bTyTyTyTy#bTyTyyTyTyTy#b#b#b#bTyTyTy#b#d",
"#d#bTyEy1TyyTy#b#bTyByTy#bTyTyTyTy#bTyTyTyTyTyTy#b#bTyTyEy2Ty#b#d",
"#d#bTyTyTyTy#b#bTyTyTy#bTyTyTyTyy#bTyTyTy#bTyTy#b#bTyTyy#bTy#b#d",
"#d#bTyyTyTyTyTyTyTyTyTy#b#bTyTy#b#b#bTyTy#bTyTyTyTyTyTy#bTy#b#d",
"#d#bTyTyTyTyTyTyTyTyTy#bTyTyTyTyTy#bTyTy#b#bTyyTyTyTy#b#bTy#b#d",
"#b#b#b#b#b#bTyTy#b#b#b#bTyTyTyTyTy#b#b#b#bTyTyTyTyTyTy#b#b#b#b",
"#yTyTyTyTyTyTyyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyTyFyd",
"#yPyTyTyTyTyTyTyTyTyTyTyyTyTyTyTyTyTyEy3TyTyTyTyTyTyTyTyTyTyTyFyd",
"#yTyTyTyyTyTyTyTyTyTyTyTyTyTyTyTyTyyTyTyTyTyTyTyTyTyyTyTyTyyTyTyFyd",
"#b#b#b#b#b#bTyTy#b#b#b#bTyTyTyTyTy#b#b#b#bTyTyTyTyTyTy#b#b#b#b",
"#d#bTyTyTyTyTyTyTyTyTy#bTyTyTyTyTy#bTyTy#b#bTyTyTyTy#b#bTy#b#d",
"#d#bTyyTyTyTyTyTyTyTyTyy#b#bTyTy#b#b#bTyTy#bTyTyTyTyTyTy#bTy#b#d",
"#d#bTyTyTyTy#b#bTyTyTy#bTyTyTyTy#bTyTyTyy#bTyTy#b#bTyTy#bTy#b#d",
"#d#bTyByTyyTy#b#bTyEy4Ty#bTyTyyTyTy#bTyByTyTyTyTy#b#bTyTyyTyTy#b#d",
"#d#bTyTyTyTy#b#bTyTyTy#bTyTyTyTy#bTyTyTyTyTy#b#b#b#bTyTyTy#b#d",
"#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
 },
      new string[] { ",#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#b#b#bTyTyTyTyTyTyTyTyyTy#b#b#b#b#bTyTyTyTyTyTyTyyTyTyTy#b#b#d",
"#d#b#bTyTyTyTyTyTyTyTyTyTyTy#b#b#bTyyTyTyTyTyTyTyTyTyByTyTy#b#d",
"#d#bTyTyTyTyyTyTyTyTyTyTyTyTyTy#bTyTyTyTy#b#b#b#bTyTyTyTyyTy#b#d",
"#d#bTyTyTyTy#b#b#b#b#bTyTyEy1Ty#bTyTyTy#b#b#b#b#b#b#b#b#b#b#b#d",
"#d#bTyTyTyTy#bTyTyTy#bTyTyTyTy#bTyTyTyTy#bTyTyTyTyTyTyTyTy#b#d",
"#b#b#bTyTy#b#bTyTyTyTyTyTyTy#b#bTyTyTyTyTyyTyTyTyTyTyTyTyTyy#b#b",
"#yTyTyTyTyTy#bTyByTyTyTyTyy#b#b#bTyTyTyTyTyTyTyTyTyTyTyTyTyTyFyd",
"#yPyTyTyTyTy#bTyTyTyTyTyTyTy#b#bTyTyTyTyTyTyTyTyTyTyEy2TyTyTyFyd",
"#yTyTyyTyTyTy#bTyTyy#b#bTyTyTyTy#bTyyTyTyTy#bTyTyTyTyyTyTyTyTyTyFyd",
"#b#b#bTyTy#b#b#b#b#bTyTyTyTyTy#bTyTyTy#b#b#b#b#bTyTyTyTyTy#b#b",
"#d#bTyTyTyTyTyTyy#bTyTyTyTyTy#b#bTyTyTy#bTyyTyTy#b#bTyTyTy#b#b#d",
"#d#bTyTyTyTyTyTy#bTyTyTy#b#b#bTyTyTyTy#bTyByTy#bTyTyTyTyTy#b#d",
"#d#b#bTyTy#b#b#b#bTyTyTyy#b#bTyTyTyTyTy#bTyTyTyTyTyTyTyTyTy#b#d",
"#d#bTyTyTyyTyEy4Ty#bTyTyTyTyTyTyTyTyTy#b#bTyEy3TyTyTyTyTyTyy#b#b#d",
"#d#bTyTyTyTyTyTy#bTyTyTyTyTyTyTyyTy#b#b#bTyTyTyy#bTyTyTy#b#b#b#d",
"#d#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#b#d",
}, new string[]
      {
       "k#g#g#g#g#g#g#g#g#g#g#g#g#g#gd#g#g#g#g#g#g#gd#gd#gd#g#g#g#g#g#g#gd#gd",
"#gTgTgTg#gdTgTgTg#gdTgTgTgTg#gdTgTg#gdTgTgTgTg#gdTgTgTg#gdTgTgTgTg#gd",
"#gTgTg#gd#gd#gdTgTgTg#gdTgTgTgTgTg#gdTgTgTg#gdTgTgTgTgTgTg#gdTgTgTg#g",
"#g#gdTgTg#gdTgTgTgTgTgTgTg#gdTgTgTgTgTg#gdTgTgTgTgTg#gdTgTgTg#gdTy#y",
"#gTgTgTgTgTgTgTg#gdTgTg#gd#gd#gdTgTyTyTyyTyTyTgTg#gdTgTgTgTgTgTyTy#y",
"#gTgTgTgTgTg#gdTgTg#gdTgTg#gdTgTyKyTyTyTyTyTyTgTgTgTgTg#gdTyTyTyy#y",
"#yTyyTyTg#gdTgTgTgTgTgTgTyTyTyTyTyyTyTyTyyTyTyTyTyTyTyTgTgTyTyTyFyd",
"#yTyTyyTyTgTgTgTgTgTyTyTyTyyTyTyyTyTyTyyTyTyTyyKyTyyTyTyTyTyTyTyyTyyFyd",
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
            Genlevel = 7;// UnityEngine.Random.Range(0, levelData.Length);
        }
        else
        { 
            if (GameManager.Instance.level < 7)
            {
                Genlevel = GameManager.Instance.level - 1; // UnityEngine.Random.Range(0, levelData.Length - 1);

            }
            else
            {
                Genlevel = UnityEngine.Random.Range(1, levelData.Length);
            }
        }

        for (int y = 0; y < levelData[Genlevel].Length; y++)
        {
            string line = levelData[Genlevel][y];

            string row = line;
            for (int x = 0; x < row.Length; x++)
            {
                if(y == 0 && x == 0)
                {
                    if (row[0] == 'q')
                    {
                        GameManager.Instance.bossLevel = true;
                        GameManager.Instance.addEnemyHp += 10;
                    }
                    else if(row[0] == 'k')
                    {
                        GameManager.Instance.saveFlool = true;
                    }
                    x++;
                }

                char tile = row[x];
                Vector3 position = new Vector3(gridx, -y, 0);
                Vector3 nodePos = new Vector3(gridx + 0.5f, -y + -0.5f, 0);

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
                                enemyOffsetSet(position, enemyPrefab, 1, false, y, new Color32(255, 0, 0, 255));
                                break;
                            case 2:
                                enemyOffsetSet(position, enemy2Prefab, 2, true, y, new Color32(255, 5, 107, 255));
                                GameManager.Instance.ishunterinScene = true;
                                break;
                            case 3:
                                enemyOffsetSet(position, enemyPrefab, 3, true, y, new Color32(198, 87, 64, 255));
                                break;
                            case 4:
                                enemyOffsetSet(position, enemyPrefab, 4, false, y, new Color32(34, 201, 156, 255));
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
                    case 'U':
                        Instantiate(BossPrefab, position, Quaternion.identity, this.transform);
                        GameManager.Instance.bosscount++;
                        gridx++;
                        break;
                    case 'K':
                        var s = Instantiate(abiltyprefab, position, Quaternion.identity, this.transform);
                        var b = s.GetComponent<ability>();
                        int sd = UnityEngine.Random.Range(1, shop.Count);
                        shop.Remove(sd);
                        b.id = sd;
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
                        if (tile != 'E' && tile != 'U')
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
        shop.Clear();
        shop = new List<int> { 0, 1, 2, 3 };
        startgame.Invoke();
    }
    public void destroyLevel()
    {

        foreach (Transform t in this.transform)
        {
            if (t != null && t.gameObject.tag != "abilityinUse")
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
    void enemyOffsetSet(Vector3 position, GameObject enemy, int whichEnemy, bool chashing, int y, Color32 maincoller)
    {

        sd = Instantiate(enemy, position, Quaternion.identity, this.transform);
        way = sd.GetComponent<WayPoints>();
        n = Instantiate(nodePrefab, position, Quaternion.identity, this.transform);
        nodeList.Add(n);
        way.currentNode = n;
        way.mainColer = maincoller;

        way.whichEnemy = whichEnemy;
        way.chase = false;
        GameManager.Instance.enemycountAdd(1);

    }
    public void RemoveNodes()
    {
        for (int i = 0; i < nodeList.Count; i++)
        {


            nodeList.RemoveAt(i);
            i--;

        }
    }
    public void SpawnPowerOrbRandom(int spawnamount)
    {
        for (int i = 0; i < spawnamount; i++)
        {


            Node randoSpawn = nodeList[UnityEngine.Random.Range(0, nodeList.Count)];
            Instantiate(PowerOrbPrefab, randoSpawn.transform.position, Quaternion.identity, this.transform);
            GameManager.Instance.powerOrbCountAdd(1);
        }
        if(GameManager.Instance.orbspawnAmount > 3)
        {
            GameManager.Instance.orbspawnAmount = 3;
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