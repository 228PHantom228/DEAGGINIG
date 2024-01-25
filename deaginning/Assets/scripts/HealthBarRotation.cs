using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRotation : MonoBehaviour
{
    [SerializeField] GameObject cam;
    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt(cam.transform.position);
    }
}
