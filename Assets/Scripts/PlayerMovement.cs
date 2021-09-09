using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerForwardSpeed;
    [SerializeField] float gravity;
    [SerializeField] float jumpSpeed;
    [SerializeField] float playerRotateSpeed;
    Animator playerAnim;
    CharacterController character;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform cameraPoint;
    public static PlayerMovement instance;
    public GameObject gun;
    public GameObject gunText;
    public bool isGun;
    public int bulletCount;
    public int ammo;
    public Text ammoText;
    public ParticleSystem particleSystem;
    Vector3 hitPoint;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        playerAnim = GetComponentInChildren<Animator>();
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var movement = new Vector3(horizontal, 0, vertical);
            movement += Physics.gravity;
            playerAnim.SetFloat("Speed", vertical);
            transform.Rotate(Vector3.up, horizontal * playerRotateSpeed);
            character.SimpleMove(transform.forward * vertical * playerForwardSpeed*3 * Time.deltaTime);
        }
        else
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var movement = new Vector3(horizontal, 0, vertical);
            movement += Physics.gravity;
            playerAnim.SetFloat("Speed", vertical);
            transform.Rotate(Vector3.up, horizontal * playerRotateSpeed);
            character.SimpleMove(transform.forward * vertical * playerForwardSpeed  * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.position += new Vector3(0, jumpSpeed, 0);
        }
        if (Input.GetMouseButtonDown(0)&&isGun&&bulletCount>0)
        {
            
            particleSystem.Play();
            bulletCount -= 1;
            ammoText.text = "Ammos : " + bulletCount;
            FireGun();   
        }
    }
    void FireGun()
    {
        Debug.DrawRay(cameraPoint.position, cameraPoint.transform.forward*100f, Color.red,2f);
        RaycastHit hit;
        if(Physics.Raycast(cameraPoint.position,cameraPoint.transform.forward,out hit))
        {
            //Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag == "Enemy")
            {
               
                var enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.EnemyDamage(1);
                }
                GameObject spawn = Pooling.instance.Get("Hit");
                if (spawn != null)
                {
                    Debug.Log(hit.point);
                    spawn.transform.position = hit.point;
                    spawn.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gun")
        {
            Destroy(other.gameObject);
            gunText.SetActive(false);
            gun.SetActive(true);
            isGun = true;
        }
        if (other.gameObject.tag == "Ammo")
        {
            ammo = 50;
            bulletCount = ammo;
            ammoText.text="Ammos : "+ammo;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Death")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
