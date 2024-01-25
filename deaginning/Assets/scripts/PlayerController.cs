using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

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
    [SerializeField] GameObject sword_1, sword_2;
    [SerializeField] Image healthBar;
    [SerializeField] TMP_Text healthUI;
    protected GameObject[] enemies;
    GameObject enemy;
    float distance;
    GameObject sword;

    void Start()
    {
        health = 100;
        anim = GetComponent<Animator>();
        sword = sword_1;
        currentSpeed = movementSpeed;
        CheckEnemies();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float closestDistance = Mathf.Infinity;
        foreach (GameObject closestEnemy in enemies)
        {
            closestEnemy.GetComponent<WalkEnemy>().arrow_off();
            float checkDistance = Vector3.Distance(closestEnemy.transform.position, transform.position);
            if (checkDistance < closestDistance)
            {
                if (closestEnemy.GetComponent<WalkEnemy>().dead == false)
                {
                    enemy = closestEnemy;
                    closestDistance = checkDistance;
                }
            }
        }
        if (enemy != null)
        {
            distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= 6 && enemy.GetComponent<WalkEnemy>().dead == false)
            {
                transform.LookAt(enemy.transform.position);
                enemy.GetComponent<WalkEnemy>().arrow_on();
            }
        }
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
        float fillPercent = health / 100f;
        healthBar.fillAmount = fillPercent;
        healthUI.text = health.ToString();
        //textUpdate.SetHealth(health);
        //damageUi.SetActive(true);
        //Invoke("RemoveDamageUI", 0.1f);

        if (health <= 0)
        {
            dead = true;
            anim.SetBool("Die", true);
        }
    }

    void CheckEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        Invoke("CheckEnemies", 3f);
    }
}
