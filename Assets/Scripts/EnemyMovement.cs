using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public GameObject PlayerObj;
    [HideInInspector] public PlayerMovement Pm;
    [HideInInspector] public GameObject CamO;

    public GameObject Me;
    public NavMeshAgent agent;

    public bool SeePlayer;
    public bool HearPlayer;
    public Vector3 PlayerPoint;

    public int Hp=100;
    
                                    
    void Start()
    {
        
        PlayerObj = GameObject.Find("Player");
        CamO = GameObject.Find("Main Camera");
        Pm = CamO.GetComponent<PlayerMovement>();
        

        PlayerPoint = Me.transform.position;
    }

    
    void Update()
    {
        if (Hp <= 0)
        {
            Destroy(Me);
        }
        HearPl();
        SeePl();


        //move
        if(Hp>0)
        agent.SetDestination(PlayerPoint);
        
    }
    

    public void HearPl()
    {
        if (HearPlayer == false)
        {
            if ((Mathf.Abs(PlayerObj.transform.position.x - Me.transform.position.x) < 10)&& (Mathf.Abs(PlayerObj.transform.position.z - Me.transform.position.z) < 10))
            {
                if (Pm.ShootCanOut == true)
                {
                    HearPlayer = true;
                }
            }
             
           
        }
        else
        {
            PlayerPoint = PlayerObj.transform.position;
            HearPlayer = false;
        }
    }
    public void SeePl()
    {
        if (SeePlayer)
        {
            PlayerPoint = PlayerObj.transform.position;
        }
    }
}
