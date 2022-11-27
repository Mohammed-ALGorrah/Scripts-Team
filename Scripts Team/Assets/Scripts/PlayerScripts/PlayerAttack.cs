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
        releasspical = Instantiate(releasspical, transform.position, Quaternion.Euler(specialAttack.skillRotation));
        releasspical.transform.SetParent(this.transform);
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

        skillIndicator = Instantiate(basicAttack.skillIndicator,indicatorParent);
        skillIndicator.transform.position = transform.position;
        
        skillSpecialIndicator= Instantiate(specialAttack.skillIndicator,indicatorParent);
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
            if (Input.GetMouseButton(1)) {
                skillSpecialIndicator.SetActive(true);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                if (player.playerData.name == "Warrior0")
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

    [PunRPC]
    private void restChrages()
    {
        chargeSystem.ResetCharge();
        player.CanSpecialAttack = false;
        player.fxSpecialAttack.gameObject.SetActive(false);
    }


    public void shoot() {
        if (photonView.IsMine) {
            GameObject bullet = (GameObject)PhotonNetwork.Instantiate("BulletsPrefs/" + basicAttack.skillProjectile.name, firePoint.position, transform.rotation);
            basicAttack.player = player;
            GetComponent<PhotonView>().RPC("WarroirBasic", RpcTarget.AllBuffered , bullet);
        } 
    }
    [PunRPC]
    private void WarroirBasic(PhotonView bullet)
    {
        
        if (!basicAttack.hasProjectile)
        {
            bullet.transform.SetParent(firePoint);
        }
    }
    public void special()
    {
        if (photonView.IsMine)
        {
            GameObject bullet = (GameObject)PhotonNetwork.Instantiate("BulletsPrefs/"+ specialAttack.skillProjectile.name, spicalPoint.position, Quaternion.Euler(specialAttack.skillRotation));
            specialAttack.player = player;
            GetComponent<PhotonView>().RPC("WarroirBasic", RpcTarget.AllBuffered, bullet);
        }
    }

    [PunRPC]
    private void WarroirSpecial(GameObject bullet)
    {
        

        if (!specialAttack.hasProjectile)
        {
            bullet.transform.SetParent(releasspical.transform);
        }
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
