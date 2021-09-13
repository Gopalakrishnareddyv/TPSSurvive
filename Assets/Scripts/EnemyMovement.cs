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
            Debug.Log(Vector3.Distance(transform.position, PlayerMovement.instance.transform.position));
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
        if (this.gameObject.tag == "Enemy")
        {
            anim.SetFloat("EnemySpeed", navMesh.velocity.magnitude);
        }
        else if(this.gameObject.tag=="Monstar")
        {
            anim.SetFloat("EnemyMonstar", navMesh.velocity.magnitude);
        }
        if (Vector3.Distance(transform.position,PlayerMovement.instance.transform.position)<3)
        {
            anim.SetFloat("EnemyMonstar", -1);
            anim.SetFloat("EnemySpeed", -1);
            var health = PlayerMovement.instance.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Damage(1);
            }
        }
    }
}
