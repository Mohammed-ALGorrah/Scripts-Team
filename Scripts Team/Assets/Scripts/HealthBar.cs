using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBar : MonoBehaviour
{
    private Camera cam;
      
      
    [SerializeField]
    Slider healthBar;
    HealthSystem health;




    void Start()
    {
        cam = Camera.main;
        health = GetComponent<HealthSystem>();
    }/*
    [PunRPC]
    private void Health_OnTakeDamage(HealthSystem obj)
        {
           // healthBar.value = health.currentHealth / health.maxHealth;
        }*/

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

        private void OnEnable()
        {
            //health.OnTakeDamage += 
        }

            private void OnDisable()
        {
           // health.OnTakeDamage -= Health_OnTakeDamage;
        }
}
