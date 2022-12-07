using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Heros.Players;
using Photon.Pun;

public class PlayerAttack : MonoBehaviourPunCallbacks
{
    PlayerControls playerControls;
    public SkillData basicAttack;
    public SkillData specialAttack;
    [SerializeField]
    Transform firePoint;
    Transform spicalPoint;
    Animator animator;
    GameObject skillIndicator;
    GameObject skillSpecialIndicator;
    public Transform indicatorParent;
    private Camera mainCamera;
    Player player;
    ChargeSystem chargeSystem;

    public ParticleSystem releasspical;

    private void Awake()
    {

        player = GetComponent<Player>();
        chargeSystem = GetComponent<ChargeSystem>();
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();

    }


    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }


    void Start()
    {

        mainCamera = FindObjectOfType<Camera>();

        releasspical = Instantiate(releasspical, transform.position, Quaternion.Euler(specialAttack.skillRotation));
        releasspical.transform.SetParent(this.transform);

        skillIndicator = Instantiate(basicAttack.skillIndicator, indicatorParent);
        skillIndicator.transform.position = transform.position;

        skillSpecialIndicator = Instantiate(specialAttack.skillIndicator, indicatorParent);
        skillSpecialIndicator.transform.position = transform.position;
    }


    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            skillIndicator.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetTrigger("isAttack");
            skillIndicator.SetActive(false);
        }
        if (player.CanSpecialAttack)
        {
            if (Input.GetMouseButton(1))
            {
                skillSpecialIndicator.SetActive(true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                if (player.playerData.name == "Warrior")
                {
                    spicalPoint = GameObject.FindGameObjectWithTag("WarriorPoint").transform;
                }
                else
                {
                    spicalPoint = GameObject.FindGameObjectWithTag("Point").transform;
                }

                spicalPoint.position = new Vector3(spicalPoint.position.x, 0.5f, spicalPoint.position.z);
                animator.SetTrigger("isPowr"); // Change it            
                skillSpecialIndicator.SetActive(false);
                GetComponent<PhotonView>().RPC("restChrages", RpcTarget.AllBuffered);

            }
        }
    }

    public void shoot()
    {

        if (photonView.IsMine)
        {
            GameObject basicbullet = (GameObject)PhotonNetwork.Instantiate(
               "Prefab/BulletsPrefabs/" + basicAttack.skillProjectile.name, firePoint.position, transform.rotation);
            GetComponent<PhotonView>().RPC("SyncBasicDamage", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void SyncBasicDamage()
    {
        basicAttack.playerID = this.GetComponentInParent<CheckPhoton>().team;
        basicAttack.playerOfBullet = player;
    }

    public void special()
    {
        if (photonView.IsMine)
        {
            GameObject specialBullet = (GameObject)PhotonNetwork.Instantiate("Prefab/BulletsPrefabs/" + specialAttack.skillProjectile.name
                , spicalPoint.position, Quaternion.Euler(specialAttack.skillRotation));
            GetComponent<PhotonView>().RPC("SyncSpecialDamage", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    private void SyncSpecialDamage()
    {
        specialAttack.playerID = this.GetComponentInParent<CheckPhoton>().team;
        specialAttack.playerOfBullet = player;
    }

    [PunRPC]
    private void restChrages()
    {
        chargeSystem.ResetCharge();
        player.CanSpecialAttack = false;
        player.fxSpecialAttack.gameObject.SetActive(false);
    }


    public void releasSpical()
    {
        releasspical.transform.position = new Vector3(this.transform.position.x, 1f, this.transform.position.z);
        releasspical.Play();
    }

    public void stopSpical()
    {
        // releasspical.transform.position = new Vector3(this.transform.position.x, 1f, this.transform.position.z);
        releasspical.Stop();
    }

}
