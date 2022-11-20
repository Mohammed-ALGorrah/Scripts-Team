using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(HealthSystem))]
public class DestoryObstacle : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject fxDestroy;
    HealthSystem health;
    Animation hitAnim;
    private void Awake()
    {
        health = gameObject.GetComponent<HealthSystem>();
        hitAnim = gameObject.GetComponent<Animation>();
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
    private void Health_OnTakeDamage(HealthSystem obj)
    {
        hitAnim.Play();
    }



    private void OnEnable()
    {
        health.OnDead += Health_OnDead;
        health.OnTakeDamage += Health_OnTakeDamage;
    }
    private void OnDisable()
    {
        health.OnDead -= Health_OnDead;
        health.OnTakeDamage -= Health_OnTakeDamage;
    }
}
