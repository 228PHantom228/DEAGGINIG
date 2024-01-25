using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    [SerializeField] Image healthBar;
    [SerializeField] Image arrow;
    protected GameObject player;
    protected Animator anim;
    protected Rigidbody rb;
    protected float distance;
    protected float timer;
    protected GameObject[] players;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        CheckPlayers();
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (!dead)
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        if (!dead && player != null)
        {
            Move();
        }
    }

    void CheckPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        Invoke("CheckPlayers", 3f);
    }

    public virtual void Move()
    {

    }
    public virtual void Attack()
    {

    }
    public void ChangeHealth(int count)
    {
        //отнимаем здоровье
        health -= count;
        float fillPercent = health / 100f;
        healthBar.fillAmount = fillPercent;
        //если здоровье меньше, либо равно нулю, то..
        if (health <= 0)
        {
            //меняем значение булевой переменной(перестают работать методы Attack и Move
            dead = true;
            anim.enabled = true;
            //включаем анимацию смерти
            anim.SetBool("Die", true);
        }
    }

    public void arrow_on()
    {
        arrow.enabled = true;
    }

    public void arrow_off()
    {
        arrow.enabled = false;
    }
}
