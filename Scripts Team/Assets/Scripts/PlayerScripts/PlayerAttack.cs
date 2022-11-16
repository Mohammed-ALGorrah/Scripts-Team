using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
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
                spicalPoint = GameObject.FindGameObjectWithTag("Point").transform;
                spicalPoint.position = new Vector3(spicalPoint.position.x, 0.5f, spicalPoint.position.z);
                animator.SetTrigger("isPowr"); // Change it            
                skillSpecialIndicator.SetActive(false);
                chargeSystem.ResetCharge();
                player.CanSpecialAttack = false;

            } 
        }
    }


    public void shoot(){

          GameObject bullet = (GameObject)Instantiate(basicAttack.skillProjectile, firePoint.position, transform.rotation);

        if(!basicAttack.hasProjectile){
              bullet.transform.SetParent(firePoint);
        }
    }
    
    public void special(){
        Instantiate(specialAttack.skillProjectile, spicalPoint.position , Quaternion.Euler(specialAttack.skillRotation));
    }
}
