using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] float movementSpeed = 3f;
    float currentSpeed;
    Animator anim;
    bool canAttackAnim = false;
    float attackCD = 0.657f;
    float attackT = 0.657f;
    private int health;
    public bool dead;
    [SerializeField] GameObject sword;

    void Start()
    {
        health = 100;
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y+6, transform.position.z-6);
        cam.transform.LookAt(transform.position);
        attackT += Time.deltaTime;
        if (attackT > attackCD + 0.2f && (anim.GetBool("Attack_1") || anim.GetBool("Attack_2")))
        {
            anim.SetBool("Attack_1", false);
            anim.SetBool("Attack_2", false);
            canAttackAnim = false;
            currentSpeed = movementSpeed;
        }

        if (anim.GetBool("Attack_1") || anim.GetBool("Attack_2"))
        {
            currentSpeed = 1.5f;
        }

        //attack
        if (Input.GetKey(KeyCode.Mouse0) && !anim.GetBool("Attack_1") && attackT > attackCD && canAttackAnim == false)
        {
            attackT = 0;
            anim.SetBool("Attack_1", true);
            anim.SetBool("Attack_2", false);
            canAttackAnim = true;
            sword.GetComponent<Weapon>().Attack();
        }
        if (Input.GetKey(KeyCode.Mouse0) && !anim.GetBool("Attack_2") && attackT > attackCD && canAttackAnim == true)
        {
            attackT = 0;
            anim.SetBool("Attack_2", true);
            anim.SetBool("Attack_1", false);
            canAttackAnim = false;
            sword.GetComponent<Weapon>().Attack();
        }

        // W
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*10);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + currentSpeed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // S
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime * 10);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - currentSpeed * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // A
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 270, 0), Time.deltaTime * 10);
            transform.position = new Vector3(transform.position.x - currentSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            anim.SetBool("Run", true);
        }
        // D
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 10);
            transform.position = new Vector3(transform.position.x + currentSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            anim.SetBool("Run", true);
        }

        // W + A
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 315, 0), Time.deltaTime * 10);
            transform.position = new Vector3(transform.position.x - currentSpeed/1.5f * Time.deltaTime, transform.position.y, transform.position.z + currentSpeed/ 1.5f * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // S + D
        if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 135, 0), Time.deltaTime * 10);
            transform.position = new Vector3(transform.position.x + currentSpeed/ 1.5f * Time.deltaTime, transform.position.y, transform.position.z - currentSpeed/ 1.5f * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // A + S
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 225, 0), Time.deltaTime * 10);
            transform.position = new Vector3(transform.position.x - currentSpeed/ 1.5f * Time.deltaTime, transform.position.y, transform.position.z - currentSpeed/ 1.5f * Time.deltaTime);
            anim.SetBool("Run", true);
        }
        // D + W
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45, 0), Time.deltaTime * 10);
            transform.position = new Vector3(transform.position.x + currentSpeed/ 1.5f * Time.deltaTime, transform.position.y, transform.position.z + currentSpeed/ 1.5f * Time.deltaTime);
            anim.SetBool("Run", true);
        }

        //условие стояния
        if ((Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)) || (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W)) || (!Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)))
        {
            anim.SetBool("Run", false);
        }
    }

    public void ChangeHealth(int count)
    {

        health -= count;
        //textUpdate.SetHealth(health);
        //damageUi.SetActive(true);
        //Invoke("RemoveDamageUI", 0.1f);

        if (health <= 0)
        {
            dead = true;
            anim.SetBool("Die", true);
        }
    }
}
