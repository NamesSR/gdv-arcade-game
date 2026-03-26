using NUnit.Framework.Constraints;
using Prime31;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovent : MonoBehaviour
{
    public float speed = 8f;
    public int value = 10;
    public float groundDamping = 20f;
    public float AttackRange = 0.5f;
    public bool CanShoot = true;
    public Vector3 dir;
    public Vector3 dir2;
    public float angle;
    public GameObject FireBallPrefab;
    public LayerMask enemyLayers;
    public Transform AttackPoint;
    public Transform sword;
    public int damage = 1;
   
    private Vector3 velocity;
    private Vector3 velocity2;
    
    private RaycastHit2D _lastControllerColliderHit;
    private CharacterController2D Controller2D;
    float nextAttackTime = 0f;
    public float AttackRate = 2f;
    private SpriteRenderer Coller;
    public  WayPoints hunter;
    public WayPoints enemy1;
    public Color mainColer;
    Transform transform23;
    bool Iframs = false;
    shoot shoot;
    



    private void Awake()
    {
        Controller2D = GetComponent<CharacterController2D>();
        Controller2D.onControllerCollidedEvent += onControllerCollider;
        Controller2D.onTriggerEnterEvent += onTriggerEnterEvent;
        Controller2D.onTriggerStayEvent += onTriggerStayEvent;
        LevelGenerator.startgame += loaddateforenemy;

        AttackPoint = GameObject.FindGameObjectWithTag("attackPoint").transform;
        Coller = GetComponent<SpriteRenderer>();
        Coller.color = mainColer;
    }
    void loaddateforenemy()
    {
        if (GameManager.Instance.enemyCount > 0 && enemy1 == null)
        {
            enemy1 = GameObject.FindGameObjectWithTag("Enemy").GetComponent<WayPoints>();
        }
        if (GameManager.Instance.enemyCount > 1 && hunter == null)
        {
            hunter = GameObject.FindGameObjectWithTag("hunter").GetComponent<WayPoints>();
        }
    }
    private void OnDestroy()
    {
        hunter = null;
        enemy1 = null;
    }
    void onTriggerStayEvent(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            
            
                if (Iframs == false)
                {
                 

                    GameManager.Instance.TakeDamage(1);
                nockback(20f);
                StartCoroutine(Iframsv2());

                }
            
                
        }
        else if (col.gameObject.tag == "hunter")
        {
            if (Iframs == false)
            {
                GameManager.Instance.TakeDamage(1);
                nockback(20f);
                StartCoroutine(Iframsv2());

            }
        }
        else if (col.gameObject.tag == "EnemyAttack")
        {
            if (Iframs == false)
            {
                GameManager.Instance.TakeDamage(1);
                nockback(15f);
                StartCoroutine(Iframsv2());

            }
        }
        else if (col.gameObject.tag == "boss")
        {
            if (Iframs == false)
            {


                GameManager.Instance.TakeDamage(1);
                nockback(20f);
                StartCoroutine(Iframsv2());

            }

        }
    }
    void onControllerCollider(RaycastHit2D hit)
    {
        // bail out on plain old ground hits cause they arent very interesting
        if (hit.normal.y == 1f)
            return;

        // logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
       // Debug.Log( "flags: " + Controller2D.collisionState + ", hit.normal: " + hit.normal );
    }
   

    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
        if (col.gameObject.tag == "Enemy")
        {


            if (Iframs == false)
            {
                GameManager.Instance.TakeDamage(1);
                nockback(20f);
                StartCoroutine(Iframsv2());

            }


        }
        else if (col.gameObject.tag == "hunter")
        {
            if (Iframs == false)
            {
                GameManager.Instance.TakeDamage(1);
                nockback(20f);
                StartCoroutine(Iframsv2());

            }
        }
        else if(col.gameObject.tag == "EnemyAttack")
        {
            if (Iframs == false)
            {
                GameManager.Instance.TakeDamage(1);
                nockback(15f);
                StartCoroutine(Iframsv2());

            }
        }
        else if (col.gameObject.tag == "boss")
        {
            if (Iframs == false)
            {


                GameManager.Instance.TakeDamage(1);
                nockback(20f);
                StartCoroutine(Iframsv2());

            }

        }
        if (col.CompareTag("nextlevel")) 
        {
            GameManager.Instance.nextlevelfin();


        }
       

        Debug.Log("trigger");
        

        if (col.CompareTag("PowerOrb"))
        {
            if (GameManager.Instance.vulnerable == false)
            {
                Destroy(col.gameObject);
                StartCoroutine(PowerOrbs());
                GameManager.Instance.powerOrbCountAdd(-1);

            }
        }
    }


    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    void Update()
    {
        
        if (GameManager.Instance.Mele == true)
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetMouseButton(0))
                {
                    

                    Attack();
                    nextAttackTime = Time.time + 1f / AttackRate;
                }
            }
        }
        else
        {
            ShootFireBall();
        }
       


    }

    private void FixedUpdate()
    {
        
        float x = Input.GetAxisRaw("Horizontal");

        
        float y = Input.GetAxisRaw("Vertical");
        var smoothedMovementFactor = groundDamping; // how fast do we change direction?
         

        velocity.x = Mathf.Lerp(velocity.x, x * GameManager.Instance.speed, Time.deltaTime * smoothedMovementFactor);


        velocity.y = Mathf.Lerp(velocity.y, y * GameManager.Instance.speed, Time.deltaTime * smoothedMovementFactor);
       
        if(new Vector3(x,y,0).magnitude > 0.01f)
        {
            rotations(x, y);
        }
        



        Controller2D.move(velocity * Time.deltaTime);
        velocity = Controller2D.velocity;//.normalized;
    }
   void rotations(float x, float y)
    {
        dir = new Vector3(x, y, 0);
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float currentZ = (transform.eulerAngles = new Vector3(0, 0, angle)).z;
        float snappedZ = Mathf.Round(currentZ / 90f) * 90f;
        transform.rotation = Quaternion.Euler(0, 0, snappedZ);
        // Debug.Log($"{currentZ}:{snappedZ}:{angle}");
    }

    IEnumerator PowerOrbs()
    {

        GameManager.Instance.vulnerable = true;
        yield return new WaitForSeconds(3);
        GameManager.Instance.vulnerable = false;

    }
    void ShootFireBall()
    {
        if (CanShoot == true)
        {
            if (Input.GetMouseButton(0))
            {
                dir2 = dir;
                GameObject projectile = Instantiate(FireBallPrefab);
                transform23 = projectile.transform;
                transform23.position = transform.position;
                shoot = projectile.GetComponent<shoot>();
                shoot.dir2 = dir;

                CanShoot = false;
                StartCoroutine(AttackCoolDown());
            }
        }
    }
    IEnumerator AttackCoolDown()
    {

        
        yield return new WaitForSeconds(1f);
        CanShoot = true;

    }
    IEnumerator Iframsv2()
    {
        Iframs = true;
        Coller.color = Color.red;
        yield return new WaitForSeconds(1f);
        Iframs = false;
        Coller.color = mainColer;
    }
    void Attack()
    {
        
        
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);
        sword.Rotate(0, 0, -90f);
        foreach (Collider2D enemy in HitEnemies)
        {
            Debug.Log("invonerble");
            if (GameManager.Instance.vulnerable == true && (enemy.tag == "Enemy" || enemy.tag == "hunter"))
            {
                Debug.Log("vunerable");
                enemy.GetComponent<WayPoints>().takeDamage(GameManager.Instance.damage, 30f);
            }
            if(GameManager.Instance.bossIsVulnerable == true && enemy.tag == "boss")
            {
                Debug.Log("vunerable");
                enemy.GetComponent<Boss>().takedamage();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
            
        
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    void nockback(float knockBack)
    {
        
            velocity2.x = Mathf.Lerp(velocity2.x, enemy1.dir.x * knockBack, Time.deltaTime * 20f);
            velocity2.y = Mathf.Lerp(velocity2.y, enemy1.dir.y * knockBack, Time.deltaTime * 20f);


            Controller2D.move(velocity2 * Time.deltaTime);
            velocity = Controller2D.velocity;
        
       
    }
}
