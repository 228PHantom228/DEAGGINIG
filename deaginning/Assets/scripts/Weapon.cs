using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Weapon : MonoBehaviour
{
    bool canAtk = false;
    [SerializeField] int damage;
    [SerializeField] GameObject trail;
    [SerializeField] Collider sword_col;
    List<GameObject> DamagedEnemies = new List<GameObject>();

    private void OnTriggerEnter(Collider collision)
    {
        if (canAtk)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().ChangeHealth(damage);
            }
        }
    }

    public void Attack()
    {
        canAtk = true;
        trail.GetComponent<TrailRenderer>().enabled = true;
        Invoke("endAttack", 0.6f);
    }

    private void endAttack()
    {
        canAtk = false;
        trail.GetComponent<TrailRenderer>().enabled = false;
        DamagedEnemies.Clear();
    }
}
