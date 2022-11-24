using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
    
    public class HealthSystem : MonoBehaviour
    {
        public float currentHealth;
        public float maxHealth ;

        public event Action <HealthSystem> OnTakeDamage;
        public event Action <HealthSystem> OnDead;
        public event Action <HealthSystem> OnHeal;

        public void Start()
        {
            currentHealth = maxHealth;
        }

       /* public HealthSystem(int max)
        {
            maxHealth = max;
        }*/

        [PunRPC]
        public void TakeDamage(int skillDmg,string hitEffectname)
        {
            // if (IsDead())
            // {
            //     return;
            // }
            if (currentHealth > 0 )
            {
                currentHealth -= skillDmg;
                
                //this.OnTakeDamage?.Invoke(this);
                GetComponent<PhotonView>().RPC("Health_OnTakeDamage",RpcTarget.AllBuffered,0);
                
                /*
                GameObject fx = PhotonNetwork.Instantiate("FX/" + hitEffectname, coll.position, Quaternion.identity);
                StartCoroutine(hidFx(fx));*/
                if (currentHealth <= 0)
                {
                    // this.OnDead?.Invoke(this);
                GetComponent<PhotonView>().RPC("Health_OnDead",RpcTarget.AllBuffered,null);
                    
                }
            }
            
        }

        IEnumerator hidFx(GameObject fx){

            yield return new WaitForSeconds(1f);
            PhotonNetwork.Destroy(fx.GetComponent<PhotonView>());
            
        }


        public void Heal(int amount)
        {
            if(currentHealth + amount > maxHealth){
                currentHealth = maxHealth;
            }else{
                currentHealth += amount;
            }
            
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }

    }
