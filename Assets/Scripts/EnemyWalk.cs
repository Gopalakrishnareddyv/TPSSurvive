using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{
    public GameObject[] goals;
    NavMeshAgent nav;
    Animator anim;
    Vector3 lastgoal;
    public float chasingPoint;
    float distanceToStop=3f;
    EnemyMovement enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }
    public void SetLocation()
    {
        nav.SetDestination(goals[Random.Range(0, goals.Length)].transform.position);
        anim.SetFloat("EnemySpeed", 1f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < chasingPoint)
        {
            enemyScript.enabled = true;
        }
        else
        {
            enemyScript.enabled = false;
        }
        if (nav.remainingDistance < distanceToStop)
        {
            SetLocation();
        }
    }
}
