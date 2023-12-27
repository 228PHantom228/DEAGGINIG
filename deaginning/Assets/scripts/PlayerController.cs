using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3f;
    float currentSpeed;
    Rigidbody rb;
    Animator anim;
    [SerializeField] float shiftSpeed = 5f;
    float stamina = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                stamina -= Time.deltaTime;
                currentSpeed = shiftSpeed;
            }
            else
            {
                currentSpeed = movementSpeed;
            }
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            stamina += Time.deltaTime;
            currentSpeed = movementSpeed;
        }

        if (stamina > 5f)
        {
            stamina = 5f;
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }
    }

    void FixedUpdate()
    {
        // W
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + currentSpeed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // S
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - currentSpeed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // A
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            transform.position = new Vector3(transform.position.x - currentSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            anim.SetBool("Run", true);
        }
        // D
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position = new Vector3(transform.position.x + currentSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            anim.SetBool("Run", true);
        }

        // W + A
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 315, 0);
            transform.position = new Vector3(transform.position.x - currentSpeed/1.5f * Time.deltaTime, transform.position.y, transform.position.z + currentSpeed/ 1.5f * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // S + D
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 135, 0);
            transform.position = new Vector3(transform.position.x + currentSpeed/ 1.5f * Time.deltaTime, transform.position.y, transform.position.z - currentSpeed/ 1.5f * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // A + S
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 225, 0);
            transform.position = new Vector3(transform.position.x - currentSpeed/ 1.5f * Time.deltaTime, transform.position.y, transform.position.z - currentSpeed/ 1.5f * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // D + W
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
            transform.position = new Vector3(transform.position.x + currentSpeed/ 1.5f * Time.deltaTime, transform.position.y, transform.position.z + currentSpeed/ 1.5f * Time.deltaTime);
            anim.SetBool("Run", true);
        }

        //условие стояния
        if ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)) || (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)) || (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)))
        {
            anim.SetBool("Run", false);
        }
    }
}
