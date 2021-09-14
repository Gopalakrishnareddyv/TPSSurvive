using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    public void Damage(float damage)
    {
        currentHealth -= damage;
        healthText.text = "Helath : " +Mathf.RoundToInt( currentHealth);
        if (currentHealth <= 0)
        {
            //Debug.Log("Player died");
            PlayerData.Instance.SetData();
            PlayerData.Instance.GetData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
