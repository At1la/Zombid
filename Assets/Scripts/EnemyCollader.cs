using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollader : MonoBehaviour
{
    public EnemyMovement Em;
    public GameObject EnemyMy;
    public bool tests;
    // Start is called before the first frame update
    void Start()
    {
        Em = EnemyMy.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Em.SeePlayer = true;
            tests = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Em.SeePlayer = false;
            tests = false;
        }
    }
}
