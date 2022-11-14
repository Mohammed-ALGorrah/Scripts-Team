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
    Animator animator;
    GameObject skillIndicator;
    GameObject skillSpecialIndicator;
    public Transform indicatorParent;
    private Camera mainCamera;
    private void Awake()
    {
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
        skillIndicator.transform.position = Vector3.zero;
        
//        skillSpecialIndicator= Instantiate(specialAttack.skillIndicator,indicatorParent);
      //  skillSpecialIndicator.transform.position = Vector3.zero;   
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
/*
        if(Input.GetMouseButton(1)){
            skillSpecialIndicator.SetActive(true);
            Vector3 pointToLook = Vector3.zero;
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                pointToLook = cameraRay.GetPoint(rayLength);
            }
            skillSpecialIndicator.transform.position = new Vector3(pointToLook.x,transform.position.y,pointToLook.z);

        }
        else if(Input.GetMouseButtonUp(1))
        {
            animator.SetTrigger("isAttack"); // Change it
            skillSpecialIndicator.SetActive(false);
        }*/
    }


    public void shoot(){
        Instantiate(basicAttack.skillProjectile, firePoint.position, transform.rotation);
    }


}
