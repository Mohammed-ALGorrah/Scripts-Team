using Heros.Players;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
    public SkillData skillData;
    void Update()
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
            int PlayerDmg = other.gameObject.GetComponent<PhotonView>().ViewID;
            CheckPhoton playerCheckPhoton = other.gameObject.GetComponentInParent<CheckPhoton>();
            if (skillData.skillType.ToString().Equals("NORMAL"))
            {
                if (playerCheckPhoton.CheckFriend2(skillData.playerID))
                {
                    //Debug.Log("friend");
                    return;
                }
                
                Player playerKill = skillData.playerOfBullet;
                PlayerSetup playerSetupKill = skillData.playerSetupOfBullet;
                player.health.TakeDamage(skillData.skillDmg, playerKill.GetComponent<PhotonView>().ViewID, playerSetupKill);
                player.chargeSystem.IncreaseCharge(+1);
                if (player.photonView.IsMine)
                {
                    player.TopPowrBar.value = player.TopPowrBar.value + (1f/ (float)player.playerData.maxCharge);
                }
                

                Destroy(Instantiate(skillData.hitEffect, gameObject.transform.position, Quaternion.identity), 2);
                Destroy(gameObject);
            }
            else
            {
                if (skillData.HelaingSkill)
                {
                    if (playerCheckPhoton.CheckFriend2(skillData.playerID ))
                    {
                        Debug.Log("HelaingSkill");
                        player.health.Heal(skillData.skillDmg);
                    }
                }
                else
                {
                    if (playerCheckPhoton.CheckFriend2(skillData.playerID ))
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
            if (photonView.IsMine && skillData.skillType.ToString().Equals("SPECIAl"))
            {
                transform.SetParent(GameObject.Find("special Point").transform);
                this.transform.position = GameObject.Find("special Point").transform.position;
            }
        }
    }
}
