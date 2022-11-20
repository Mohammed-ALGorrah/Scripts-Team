using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(HealthSystem))]
public class DestoryObstacle : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject fxDestroy;
    HealthSystem health;
    private void Awake()
    {
        health = gameObject.GetComponent<HealthSystem>();
       
    }

    public void Damage(int dmg)
    {
        health.TakeDamage(dmg);
    }
    private void Health_OnDead(HealthSystem obj)
    {
        fxDestroy.SetActive(true);
        Destroy(gameObject,0.25f);
    }

    private void OnEnable()
    {
        health.OnDead += Health_OnDead;
    }
    private void OnDisable()
    {
        health.OnDead -= Health_OnDead;
    }
}
