using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    PlayerType playerType;
    PlayerControls playerControls;
    public GameObject normalAttackImage;
    private void Awake()
    {
        playerType = GetComponent<Player>().playerData.playerType;
        playerControls = new PlayerControls();
        
      //  playerControls.Player.Fire.started += _ => StartNormalAttack();

    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    [SerializeField]
    Transform firePoint;

    public GameObject Bullet;


    void Start()
    {
        playerType = GetComponent<Player>().playerData.playerType;

        switch(playerType){

            case PlayerType.fighter:
                Bullet = Resources.Load<GameObject>("BulletsPrefs/Warrior_Ammo");
                break;

            case PlayerType.defneder:
            
                Bullet = Resources.Load<GameObject>("BulletsPrefs/Archer_Ammo");

                break;

            case PlayerType.healther:
            
                Bullet = Resources.Load<GameObject>("BulletsPrefs/Wizard_Ammo");

                break;
           }
    }


    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            normalAttackImage.SetActive(true);


        }
        else if (Input.GetMouseButtonUp(0) && !Input.GetKey(KeyCode.Space))
        {
            normalAttack();
            normalAttackImage.SetActive(false);
        }

        /*if(Input.GetMouseButtonUp(0)){
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("attac camncle");
                return;
            }
            normalAttack();
        }else if(Input.GetMouseButtonDown(1))
        {
            specialAttack();
        }*/
    }
    

    void StartNormalAttack()
    {
       Debug.Log("Start Attack");    
    }

        void normalAttack()
        {
            Instantiate(Bullet, firePoint.position, transform.rotation);
        }


        void specialAttack()
        {
            switch (playerType)
            {

                case PlayerType.fighter:
                    Debug.Log(playerType);
                    break;

                case PlayerType.defneder:
                    Debug.Log(playerType);

                    break;

                case PlayerType.healther:
                    Debug.Log(playerType);

                    break;
            }
        }
    


}
