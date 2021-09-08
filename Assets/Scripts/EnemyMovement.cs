using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float chasingPoint;
    Vector3 startingPoint;
    public bool isChasing;
    NavMeshAgent navMesh;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        startingPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < chasingPoint)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
        if (isChasing)
        {
            navMesh.SetDestination(PlayerMovement.instance.transform.position);
        }
        else
        {
            navMesh.SetDestination(startingPoint);
        }
        anim.SetFloat("EnemySpeed", navMesh.velocity.magnitude);
        if (Vector3.Distance(transform.position,PlayerMovement.instance.transform.position)<3)
        {
            var health = PlayerMovement.instance.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Damage(0.1f);
            }
        }
    }
}
