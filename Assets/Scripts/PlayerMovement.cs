using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Camera camerar;
    public Vector3 PointToMove;


    public GameObject PlayerObject;
    public float  SpeerRotLook;
    public Vector3 DerectionLookAt;
    public Vector3 PointLook;
    public bool LookRotEnd;

    public NavMeshAgent agent;

    public GameObject CameraObj;
    public Vector3 CamPos;
    public float CamSpeed;

    public GameObject PulaObj;
    public Transform PulaSpawnPoint;
    public float ShootTimer;
    public bool ShootCanOut;
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

        if (LookRotEnd == false)
        {

            DerectionLookAt = PointLook - PlayerObject.transform.position;
            Quaternion rotation = Quaternion.LookRotation(DerectionLookAt);
            PlayerObject.transform.rotation = Quaternion.Lerp(PlayerObject.transform.rotation, rotation, SpeerRotLook * Time.deltaTime);

        }
        Shoot();
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
                    }

                }
            }
        }
    }
    public void PlayerMoveToPoint()
    {
            
        agent.SetDestination(PointToMove);
        
    }
    public void Shoot()
    {
        
        if (ShootCanOut == true)
        {
            ShootTimer += Time.deltaTime;
            if (ShootTimer > 0.4f)
            {
                Instantiate(PulaObj, PulaSpawnPoint.position, PlayerObject.transform.rotation);
                ShootCanOut = false;
                ShootTimer = 0;
            }
        }
    }
}

