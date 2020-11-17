using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFly : MonoBehaviour
{
    public GameObject Me;
    public float Speed;
    public Vector3 StartPos;

    public EnemyMovement Em;
    public bool a;
    // Start is called before the first frame update
    void Start()
    {
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
            Em.Hp -= 40;
            Destroy(Me);
        }
    }
}
