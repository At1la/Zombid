using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFly : MonoBehaviour
{
    public GameObject Me, CamO;
    public PlayerMovement Pm;
    public float Speed;
    public Vector3 StartPos;

   

    public EnemyMovement Em;
    public bool a;
    // Start is called before the first frame update
    void Start()
    {
        CamO = GameObject.Find("Main Camera");
        Pm = CamO.GetComponent<PlayerMovement>();
        StartPos = Me.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Me.transform.Translate(0,0 , Speed);
        if(Mathf.Abs(Me.transform.position.x - StartPos.x) > 20)
        {
            Destroy(Me);
        }
        if (Mathf.Abs(Me.transform.position.z - StartPos.z) > 20)
        {
            Destroy(Me);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag== "vrag")
        {
            Em = other.GetComponent<EnemyMovement>();
            GiveDamege();
            Destroy(Me);
        }
        if(other.gameObject.tag == "Wall")
        {
            Destroy(Me);
        }
    }

    //Set Damage
    public void GiveDamege()
    {
        switch (Pm.TipeWeapon)
        {
            case 1:
                Em.Hp -= 35;
                break;
            case 2:
                Em.Hp -= 10;
                break;
            case 3:
                Em.Hp -= 70;
                break;
        }
    }
}
