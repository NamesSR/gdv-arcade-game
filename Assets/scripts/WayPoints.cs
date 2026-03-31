using JetBrains.Annotations;
using Prime31;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WayPoints : MonoBehaviour
{


    public int offset = 0;
    public int MaxOffset = 6;
    public bool chase = false;
    public bool isChasing = false;
    public float speed = 3f;
    public float chaseRange = 5f;
    public int whichEnemy;
    public bool chaseDog = false;
    Transform HunterTransform;
    //WayPoints HunterWaypoints;
    PlayerMovent playerMovent;
    LevelGenerator lg;
    public Node currentNode;
   public List<Node> path = new List<Node>();
    //  public GameObject sho;
    public Vector3 dir;

    bool houddistance;
    public bool Iframs = false;

    GameObject levlg;
    CharacterController2D Controller2D2;
    Transform player;

    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 velocity2;
    public int enemyHp = 2;
    private SpriteRenderer enemyColler;
    public Color mainColer;
    shoot shoot;
    Vector3 dir234;
    Transform shootransform;
    float nextAttackTime = 0f;
    public float AttackRate = 2f;
    ParticleSystem ps;
    public GameObject FireBallEnemyprefab;
   
    BoxCollider2D bc;
    SpriteRenderer sr;
    

    public enum StateMachine
    {
        patrol,
        Chasings,
        attacking,
        maddog,
        dogchases,
        dogchasesPlayer,
        pause

    };
    public StateMachine currentState;
    
    private void Start()
    {
       
        currentState = StateMachine.patrol;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
       

        //  currentNode = ANodeStartManager.instance.FindNearestNode(transform.position);

    }
    private void Awake()
    {
        lg = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        ps = GetComponent<ParticleSystem>();
        // s = sho.GetComponent<shoot>();
        Controller2D2 = GetComponent<CharacterController2D>();
        Controller2D2.onControllerCollidedEvent += onControllerCollider2;
        Controller2D2.onTriggerEnterEvent += onTriggerEnterEvent2;
        //enemyHp //.= GameManager.Instance.enemyHealth();
        enemyColler = GetComponent<SpriteRenderer>();
        LevelGenerator.startgame += loaddateforenemy;
        var s = ps.main;
        sr.enabled = true;
        bc.enabled = true;
        ps?.Stop();
        s.startColor = mainColer;
        enemyHp += GameManager.Instance.addEnemyHp;


    }
    void onControllerCollider2(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
        // Debug.Log( "flags: " + Controller2D.collisionState + ", hit.normal: " + hit.normal );
    }
    void loaddateforenemy()
    {
        playerMovent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovent>();
        // currentNode = ANodeStartManager.instance.FindNearestNode(transform.position);

        if (whichEnemy == 3)
        {

            if (HunterTransform == null && GameManager.Instance.ishunterinScene == true)
            {
                HunterTransform = GameObject.FindGameObjectWithTag("hunter").transform;

            }


        }
        GameManager.Instance.gameStarted = true;
    }
    private void OnDestroy()
    {
        HunterTransform = null;
        playerMovent = null;
    }
    void onTriggerEnterEvent2(Collider2D col)
    {
        Debug.Log("hit");
        if (col.gameObject.tag == "Attack")
        {

            if (GameManager.Instance.vulnerable == true)
            {

                Debug.Log($"check 1: s.dir2: {playerMovent.dir2} dir: {dir}");
                //dir = playerMovent.dir2;
                Debug.Log($"check 2: s.dir2: {playerMovent.dir2} dir: {dir}");
                takeDamage(GameManager.Instance.FireBallDagame, 15f);
            }
        }



    }
    void patrol()
    {

        if (path.Count == 0)
        {

            path = ANodeStartManager.instance.GeneratePath(currentNode, ANodeStartManager.instance.NodesInScene()[Random.Range(0, ANodeStartManager.instance.NodesInScene().Length)]);
        }
    }
    void Chasing()
    {
        if (path.Count == 0)
        {

            path = ANodeStartManager.instance.GeneratePath(currentNode, ANodeStartManager.instance.FindNearestNode(player.position));


        }
    }
    void attacking()
    {
        shoot23();
    }
    void dogchases()
    {
        if (path.Count == 0)
        {

            path = ANodeStartManager.instance.GeneratePath(currentNode, ANodeStartManager.instance.FindNearestNode(HunterTransform.position));


        }

    }

    public void CreatePath()
    {

        if (path.Count > 0)
        {
            int r = 0;

            dir = (path[r].transform.position - transform.position).normalized;
            velocity2 = dir * speed;
            Controller2D2.move(velocity2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, path[r].transform.position) < 0.1f)
            {
                currentNode = path[r];
                path.RemoveAt(r);
            }
        }
    }

    IEnumerator IFramsV()
    {

        yield return new WaitForSeconds(0.1f);
        Iframs = false;
    }
    private void Update()
    {
        if (GameManager.Instance.vulnerable == true)
        {
            enemyColler.color = Color.blue;

        }
        else
        {
            enemyColler.color = mainColer;
        }
        if (GameManager.Instance.gameStarted == true && enemyHp > 0)
        {
            switch (currentState)
            {
                case StateMachine.patrol:
                    patrol();
                    if (whichEnemy == 2)
                    {
                        GameManager.Instance.isChasing = false;
                    }
                    break;
                case StateMachine.Chasings:
                    Chasing();
                    if (whichEnemy == 2)
                    {
                        GameManager.Instance.isChasing = true;
                    }
                    break;
                case StateMachine.attacking:
                    attacking();
                    break;
                case StateMachine.dogchases:
                    dogchases();
                    break;
                case StateMachine.dogchasesPlayer:
                    Chasing();
                    break;
                case StateMachine.maddog:
                    Chasing();
                    break;
                case StateMachine.pause:
                    break;
            }
            CreatePath();
        }
    }

    private void FixedUpdate()
    {
        if (whichEnemy == 3)
        {
            Debug.Log(currentState);
        }
        if (GameManager.Instance.gameStarted == true)
        {

            bool PlayerSeen = Vector3.Distance(transform.position, player.position) < chaseRange;
            if (whichEnemy == 3 && HunterTransform != null)
            {
                houddistance = Vector3.Distance(transform.position, HunterTransform.position) > 2f;
            }
            if (whichEnemy != 3 && PlayerSeen == false && currentState != StateMachine.patrol)
            {
                currentState = StateMachine.patrol;
                path.Clear();
            }
            else if (whichEnemy == 2 && PlayerSeen == true && currentState != StateMachine.Chasings)
            {
                currentState = StateMachine.Chasings;
                path.Clear();
            }
            else if (PlayerSeen == true && whichEnemy == 4 && currentState != StateMachine.attacking)
            {
                currentState = StateMachine.attacking;
                path.Clear();
            }
            else if (whichEnemy == 3 && currentState != StateMachine.maddog && GameManager.Instance.hunterdead == true)
            {
                currentState = StateMachine.maddog;
                path.Clear();
            }
            else if (whichEnemy == 3 && GameManager.Instance.isChasing == true && currentState != StateMachine.dogchasesPlayer && GameManager.Instance.hunterdead == false)
            {


                currentState = StateMachine.dogchasesPlayer;
                path.Clear();

            }
            else if (houddistance == true && whichEnemy == 3 && GameManager.Instance.isChasing == false && currentState != StateMachine.dogchases && GameManager.Instance.hunterdead == false)
            {


                currentState = StateMachine.dogchases;
                path.Clear();

            }
            else if (houddistance == false && whichEnemy == 3 && GameManager.Instance.isChasing == false && currentState != StateMachine.pause && GameManager.Instance.hunterdead == false)
            {


                currentState = StateMachine.pause;


            }




        }
    }


    public void takeDamage(int Damage, float knockback)
    {
        if (Iframs == false)
        {
            enemyHp -= Damage;
            Debug.Log($"check 1: s.dir2: {playerMovent.dir2} dir: {dir}");

            Debug.Log($"check 2: s.dir2: {playerMovent.dir2} dir: {dir}");
            for (int i = 0; i < 3; i++)
            {
                velocity2.x = Mathf.Lerp(velocity2.x, playerMovent.dir2.x * knockback, Time.deltaTime * 20f);
                velocity2.y = Mathf.Lerp(velocity2.y, playerMovent.dir2.y * knockback, Time.deltaTime * 20f);
                Controller2D2.move(velocity2 * Time.deltaTime);
                velocity2 = Controller2D2.velocity;
            }





            Iframs = true;
            StartCoroutine(IFramsV());
        }



        if (enemyHp <= 0)
        {
            sr.enabled = false;
            bc.enabled = false;
            ps?.Stop();
            ps?.Play();
            Destroy(this.gameObject, 0.3f);
            GameManager.Instance.AddPoints(50);
            GameManager.Instance.enemyCount--;
            if (whichEnemy == 2)
            {
                GameManager.Instance.hunterdead = true;
            }
        }

    }

    void shoot23()
    {
        if (Time.time >= nextAttackTime)
        {
            dir234 = (player.position - transform.position).normalized;
            var FireBallEnemy = Instantiate(FireBallEnemyprefab);
            shootransform = FireBallEnemy.transform;
            shootransform.position = transform.position;
            shoot = FireBallEnemy.GetComponent<shoot>();
            shoot.dir2 = dir234;
            nextAttackTime = Time.time + 1 / AttackRate;
        }

    }

}