using Prime31;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class shoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    PlayerMovent g;
   public GameObject go;
    Vector3 dir2;
    CharacterController2D Controller2D3;
    float speed = 8f;
    public Vector3 velosty;
    void Start()
    {
        transform.position = go.transform.position;
        dir2 = g.dir;
        Controller2D3.onTriggerEnterEvent += onTriggerEnterEvent3;

    }
    private void Awake()
    {
        Controller2D3 = GetComponent<CharacterController2D>();
        go = GameObject.FindGameObjectWithTag("Player");
        g = go.GetComponent<PlayerMovent>();
       
        

    }
   
    void onTriggerEnterEvent3(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
           
        }
      
    }


    
    private void FixedUpdate()
    {
        velosty = dir2 * speed;
        Controller2D3.move(velosty * Time.deltaTime);
    }
    
}
