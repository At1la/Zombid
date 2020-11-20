using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement :  MonoBehaviour 
{
    public Camera camerar;
    public Vector3 PointToMove;


    public GameObject PlayerObject;
    public Animator AnimatorPlayer;
    public float  SpeerRotLook;
    public Vector3 DerectionLookAt;
    public Vector3 PointLook;
    public bool LookRotEnd;

    public bool IsMoving;
    public float Pogreshnosty=0.5f;

    public NavMeshAgent agent;

    public GameObject CameraObj;
    public Vector3 CamPos;
    public float CamSpeed;

    public GameObject PulaObj;
    public Transform PulaSpawnPoint;
    public float ShootTimer;
    public bool ShootCanOut;

    public int TipeWeapon=1;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        CameraObj.transform.position = Vector3.MoveTowards(CameraObj.transform.position, PlayerObject.transform.position-CamPos, CamSpeed * Time.deltaTime);

        GetRayHitVector();
        PlayerMoveToPoint();
        TipeOfShoot(TipeWeapon);
        AnimationController();

        //Проверка движения
        if ((PlayerObject.transform.position.x+Pogreshnosty >= PointToMove.x)&& (PlayerObject.transform.position.x - Pogreshnosty <= PointToMove.x) && (PlayerObject.transform.position.z+Pogreshnosty >= PointToMove.z) && (PlayerObject.transform.position.z - Pogreshnosty <= PointToMove.z))
        {
            IsMoving = false;
        }
        //Прицеливание персонажа
        if (LookRotEnd == false)
        {

            DerectionLookAt = PointLook - PlayerObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(DerectionLookAt);
            PlayerObject.transform.rotation = Quaternion.Lerp(PlayerObject.transform.rotation, rotation, SpeerRotLook * Time.deltaTime);

        }
        
    }
    public void GetRayHitVector()
    {
        //pc
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camerar.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "vrag")
                {
                    PointLook = hit.transform.position;
                    PointToMove = PlayerObject.transform.position;
                    LookRotEnd = false;
                    ShootCanOut = true;


                }

                if ((hit.collider.tag != "vrag") && (hit.collider.tag != "Wall"))
                {
                    PointToMove = hit.point;
                    LookRotEnd = true;
                    IsMoving = true;

                }

            }
        }

        //phone
        foreach (Touch touch in Input.touches)
        {
            if (Input.touchCount > 0)
            {
                Ray ray = camerar.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.tag == "vrag")
                    {
                        PointLook = hit.transform.position;
                        PointToMove = PlayerObject.transform.position;
                        LookRotEnd = false;
                        ShootCanOut = true;



                    }

                    if ((hit.collider.tag != "vrag") && (hit.collider.tag != "Wall"))
                    {
                        PointToMove = hit.point;
                        LookRotEnd = true;
                        IsMoving = true;
                    }

                }
            }
        }
    }
    public void PlayerMoveToPoint()
    {
            
        agent.SetDestination(PointToMove);
        
    }

    [HideInInspector] public int LocalCount = 0;
    public void TipeOfShoot(int tipe)
    {
            
        if (ShootCanOut == true)
        {
            ShootTimer += Time.deltaTime;
            switch (tipe)
            {
                case 1:
                    AnimatorPlayer.SetBool("shoot", true);
                    if (ShootTimer > 0.4f)
                    {
                        Instantiate(PulaObj, PulaSpawnPoint.position, PlayerObject.transform.rotation);
                        ShootCanOut = false;
                        ShootTimer = 0;
                        AnimatorPlayer.SetBool("shoot", false);
                    }
                    
                    break;
                case 2:
                   
                    if (ShootTimer >= 0.5f)
                        {
                        if (LocalCount == 0)
                        {
                            Instantiate(PulaObj, PulaSpawnPoint.position, PlayerObject.transform.rotation);
                            LocalCount = 1;
                        }
                            if(ShootTimer >= 0.7f)
                            {
                            if (LocalCount == 1)
                            {
                                Instantiate(PulaObj, PulaSpawnPoint.position, PlayerObject.transform.rotation);
                                LocalCount = 2;
                            }
                            if (ShootTimer >= 0.9f)
                                {
                                if (LocalCount == 2)
                                {
                                    Instantiate(PulaObj, PulaSpawnPoint.position, PlayerObject.transform.rotation);
                                }
                                ShootCanOut = false;
                                    ShootTimer = 0;
                                LocalCount = 0;
                                }
                            }
                            
                        
                         }
                   

                    break;
                case 3:
                    
                    if (ShootTimer > 1f)
                    {                        
                        Instantiate(PulaObj, PulaSpawnPoint.position, PlayerObject.transform.rotation);
                        ShootCanOut = false;
                        ShootTimer = 0;
                    }

                    break;

            }
        }

    }

   public void AnimationController()
    {
        if (IsMoving)
        {
            AnimatorPlayer.SetBool("run", true);
        }
        else
        {
            AnimatorPlayer.SetBool("run", false);
        }
    }
   
}

