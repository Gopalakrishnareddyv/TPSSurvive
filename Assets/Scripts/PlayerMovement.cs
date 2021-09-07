using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerForwardSpeed;
    [SerializeField] float gravity;
    [SerializeField] float playerRotateSpeed;
    Animator playerAnim;
    CharacterController character;
    [SerializeField] Transform firePoint;
    [SerializeField] Transform cameraPoint;
    [SerializeField] GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetMouseButtonDown(0))
        {
            FireGun();   
        }
    }
    void FireGun()
    {
        Debug.DrawRay(cameraPoint.position, cameraPoint.transform.forward*1000f, Color.red,2f);
        RaycastHit hit;
        if(Physics.Raycast(cameraPoint.position,cameraPoint.transform.forward,out hit, Mathf.Infinity))
        {
            Debug.Log(hit.collider.gameObject.name);
            //firePoint.LookAt(hit.point);
            //Instantiate(bullet, firePoint.transform.position, Quaternion.identity);
        }
    }
}
