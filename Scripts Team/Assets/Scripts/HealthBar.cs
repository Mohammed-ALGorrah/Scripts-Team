using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera cam;
    private HealthSystem health;
    [SerializeField]
    Slider healthBar;
    private void Awake()
    {
        health = this.gameObject.transform.GetComponentInParent<HealthSystem>();
    }

    private void OnEnable()
    {
        health.OnTakeDamage += Health_OnTakeDamage;
    }

    private void OnDisable()
    {
        health.OnTakeDamage -= Health_OnTakeDamage;
    }

    private void Health_OnTakeDamage(HealthSystem obj)
    {
        healthBar.value = health.currentHealth / health.maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }
}
