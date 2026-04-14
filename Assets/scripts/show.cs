using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class show : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PowerUpManager pum;
    public PowerUp23 pu23;
    public TextMeshProUGUI upgrade;
    void Start()
    {
        

    }
    private void Awake()
    {
        upgrade.text = pum.getvalue(pu23);
    }
   
}
