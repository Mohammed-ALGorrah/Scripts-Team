using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    PlayerType playerType;
    PlayerControls playerControls;
    bool n;
    private void Awake()
    {
        playerType = GetComponent<Player>().playerData.playerType;
        playerControls = new PlayerControls();
        //playerControls.Player.Fire.started += _ => normalAttack();
        //playerControls.Player.Fire.performed += normalAttack;
        
            playerControls.Player.Fire.started += _ => StartNormalAttack();

           // playerControls.Player.Fire.canceled += _ => normalAttack();

        

        

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

        //   playerControls.Player.Attack.performed += normalAttack;
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("cancleeeee");
            playerControls.Player.Fire.canceled += _ => normalAttack();
        }
        if(Input.GetKeyDown(KeyCode.R)){
            normalAttack();
        }else if(Input.GetKeyDown(KeyCode.G)){
            specialAttack();
        }
    }
    

    void StartNormalAttack()
    {
       Debug.Log("Start Attack");    
    }
    void normalAttack()
    {
            Debug.Log("Cancle Attack");
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

    void normalAttack(){
        Instantiate(Bullet,firePoint.position, transform.rotation);
        
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
