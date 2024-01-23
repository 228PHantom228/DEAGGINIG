using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    bool canAtk = true;
    [SerializeField] Collider sword_col;

    private void OnTriggerEnter(Collider collision)
    {
        if (canAtk)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().ChangeHealth(50);
                canAtk = false;
            }
        }
    }

    public void Attack()
    {
        canAtk = true;
    }
}
