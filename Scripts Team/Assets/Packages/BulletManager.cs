using Heros.Players;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public SkillData skillData;
    void LateUpdate()
    {
        if (skillData.skillType.ToString().Equals("NORMAL") && skillData.hasProjectile)
        {
            transform.TransformDirection(Vector3.forward);
            transform.Translate(new Vector3(0, 0, skillData.ProjectiSpeed) * Time.deltaTime);
        }
        Destroy(gameObject, skillData.ProjectileLifeTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerSetup>() != null)
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (skillData.skillType.ToString().Equals("NORMAL"))
            {
                if (player.CheckFriend(skillData.player))
                {
                    Debug.Log("friend");
                    return;
                }

                player.health.TakeDamage(skillData.skillDmg);
                player.chargeSystem.IncreaseCharge(+1);

                Destroy(Instantiate(skillData.hitEffect, gameObject.transform.position, Quaternion.identity), 2);
                Destroy(gameObject);
            }
            else
            {
                if (skillData.HelaingSkill)
                {
                    if (player.CheckFriend(skillData.player))
                    {
                        Debug.Log("HelaingSkill");
                        player.health.Heal(skillData.skillDmg);
                    }
                }
                else
                {
                    if (player.CheckFriend(skillData.player))
                    {
                        return;
                    }
                    player.health.TakeDamage(skillData.skillDmg);
                    player.chargeSystem.IncreaseCharge(+2);
                    if (skillData.skillName == "Spin Dash")
                    {
                        gameObject.gameObject.SetActive(false);
                    }
                }
            }
        }
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


    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        if (!skillData.hasProjectile)
        {
            if (photonView.IsMine && skillData.skillType.ToString().Equals("NORMAL"))
            {
                Debug.Log("Bullet instansite IsMine");
                transform.SetParent(GameObject.Find("FirePoint").transform);
                this.transform.position = GameObject.Find("FirePoint").transform.position;
            }
            else if (!skillData.skillType.ToString().Equals("SPECIAl"))
            {
                Debug.Log("Bullet instansite NOT  IsMine");
                Destroy(gameObject);
            }
        }
    }
}
