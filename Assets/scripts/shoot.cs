using Prime31;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class shoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerMovent g;
    public GameObject go;
    public Vector3 dir2;
    CharacterController2D Controller2D3;
    float speed = 8f;
    public Vector3 velosty;
    ParticleSystem PS;
    BoxCollider2D bc2d;
    bool stop = false;
    SpriteRenderer sr;
    void Start()
    {
        // transform.position = go.transform.position;
        PS = GetComponent<ParticleSystem>();
        bc2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        Controller2D3.onTriggerEnterEvent += onTriggerEnterEvent3;
        PS?.Stop();
        bc2d.enabled = true;
        sr.enabled = true;
    }
    private void Awake()
    {
        Controller2D3 = GetComponent<CharacterController2D>();
       
        //go = GameObject.FindGameObjectWithTag("Player");
        //g = go.GetComponent<PlayerMovent>();



    }

    void onTriggerEnterEvent3(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Wall" || col.gameObject.tag == "hunter" || col.gameObject.tag == "nextlevel")
        {
            
            stop = true;
            sr.enabled = false;
            bc2d.enabled = false;
            PS?.Stop();
            PS?.Play();
            Destroy(this.gameObject, 0.3f);
        }

    }



    private void FixedUpdate()
    {
        if (stop == false)
        {
            velosty = dir2 * speed;
            Controller2D3.move(velosty * Time.deltaTime);
        }
    }

}
