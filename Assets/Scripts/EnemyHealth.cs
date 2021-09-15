using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Animator anim;
    public int enemyHealth;
    public int currentEnemyHealth;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void EnemyDamage(int damage)
    {
        currentEnemyHealth += damage;
        if (currentEnemyHealth > enemyHealth)
        {
            //anim.SetFloat("EnemySpeed", 2);
            this.gameObject.SetActive(false);
            //Destroy(gameObject, 3f);
        }
    }
    
}
