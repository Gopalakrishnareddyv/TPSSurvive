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
    public GameObject postProcess;
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
        postProcess.SetActive(true);
        StartCoroutine("PostProcessStop");
        if (currentHealth <= 0)
        {
            //Debug.Log("Player died");
            PlayerData.Instance.SetData();
            PlayerData.Instance.GetData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    IEnumerator PostProcessStop()
    {
        yield return new WaitForSeconds(0.1f);
        postProcess.SetActive(false);
    }
}
