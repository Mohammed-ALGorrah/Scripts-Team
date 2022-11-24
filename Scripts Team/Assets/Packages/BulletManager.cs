using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public SkillData skillData;

    void Update()
    {
        

        if (skillData.skillType.ToString().Equals("NORMAL") && skillData.hasProjectile)
        {
            transform.TransformDirection(Vector3.forward);        
            transform.Translate(new Vector3(0,0, skillData.ProjectiSpeed) * Time.deltaTime);
        }
        Destroy(gameObject,skillData.ProjectileLifeTime);
    }
/*
    private void OnTriggerEnter(Collider other)
    {
        //IDamageable damageable = other.GetComponent<IDamageable>();

        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.Damage(10);
        }

        if (other.CompareTag("obstacle"))
        {
            Debug.Log("obstacle");
            Destroy(gameObject);
        }
    }
*/
   
}
