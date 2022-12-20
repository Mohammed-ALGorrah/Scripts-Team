using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class HealthBar : MonoBehaviourPunCallbacks
{
    private Camera cam;
    private HealthSystem health;
    public Slider healthBar;
    CheckPhoton checkPhoton;
    public Slider TopHealthBar;
    private void Awake()
    {
        checkPhoton = FindObjectOfType<CheckPhoton>();
        health = this.gameObject.transform.GetComponentInParent<HealthSystem>();
        TopHealthBar = FindObjectOfType<SyncTopHealthBar>().GetComponent<Slider>();
        if (photonView.IsMine)
        {
            TopHealthBar.value = healthBar.value;           
        }
            
    }

    private void OnEnable()
    {
        health.OnTakeDamage += Health_OnTakeDamage;
        health.OnHeal += Health_OnHeal;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= Health_OnTakeDamage;
        health.OnHeal -= Health_OnHeal;
    }

    private void Health_OnHeal(HealthSystem obj)
    {
        healthBar.value = health.currentHealth / health.maxHealth;
        if (photonView.IsMine)
        {
            TopHealthBar.value = healthBar.value;
        }
        
    }

    private void Health_OnTakeDamage(HealthSystem obj)
    {
        healthBar.value = health.currentHealth / health.maxHealth;
        if (photonView.IsMine)
        {
            TopHealthBar.value = healthBar.value;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //healthBar.value = health.currentHealth;
        if (checkPhoton.team == 1)
        {
            healthBar.GetComponentsInChildren<Image>()[1].color = Color.blue;
        }
        else if (checkPhoton.team == 2)
        {
            healthBar.GetComponentsInChildren<Image>()[1].color = Color.red;
        }

        if (photonView.IsMine)
        {
            healthBar.GetComponentsInChildren<Image>()[1].color = Color.green;
        }
        cam = Camera.main;

    }

    // Update is called once per frame
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
