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
    public int enemyCount;
    public int ammo;
    public int refilAmmo;
    public Text ammoText;
    public Text enemyText;
    public ParticleSystem partSystem;
    Vector3 hitPoint;
    AudioSource audioSource;
    public AudioClip bulletSound;
    public AudioClip bulletHitSound;
    float jumpTime=0f;
    bool jump, doublejump;
    // Start is called before the first frame update
    void Start()
    {
        refilAmmo = ammo;
        instance = this;
        playerAnim = GetComponentInChildren<Animator>();
        character = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            //Double speed
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
            //Normal speed
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var movement = new Vector3(horizontal, 0, vertical);
            movement += Physics.gravity;
            playerAnim.SetFloat("Speed", vertical);
            transform.Rotate(Vector3.up, horizontal * playerRotateSpeed);
            character.SimpleMove(transform.forward * vertical * playerForwardSpeed  * Time.deltaTime);
        }
        //transform.position += new Vector3(0, jumpSpeed, 0);
        jumpTime += Time.deltaTime;
        if (jumpTime >= 2f)
        {
            jump = true;
            if (jump)
            {
                doublejump = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.E)&&jump&&doublejump)
        { 
            transform.position += new Vector3(0, jumpSpeed, 0);
            jumpTime = 0f;            
        }
        if (Input.GetMouseButtonDown(0)&&isGun&&bulletCount>0)
        {
            audioSource.clip = bulletSound;
            audioSource.Play();
            partSystem.Play();
            bulletCount --;
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
            if (hit.collider.gameObject.tag == "Enemy"||hit.collider.gameObject.tag=="Monstar")
            {
                
                var enemy = hit.collider.gameObject.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    
                    enemy.EnemyDamage(1);
                    if (enemy.currentEnemyHealth > enemy.enemyHealth)
                    {
                        enemyCount++;
                        enemyText.text = "Enemies : " + enemyCount;
                    }
                }
                GameObject spawn = Pooling.instance.Get("Hit");
                if (spawn != null)
                {
                    //Debug.Log(hit.point);
                    audioSource.clip = bulletHitSound;
                    audioSource.Play();
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
            ammo = refilAmmo;
            bulletCount = ammo;
            ammoText.text="Ammos : "+ammo;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Death")
        {
            PlayerData.Instance.SetData();
            PlayerData.Instance.GetData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.gameObject.tag == "EndPoint")
        {
            PlayerData.Instance.SetData();
            PlayerData.Instance.GetData();
            SceneManager.LoadScene(2);
            Destroy(other.gameObject);
        }
    }
}
