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

    void Start()
    {
        playerType = GetComponent<Player>().playerData.playerType;

        //   playerControls.Player.Attack.performed += normalAttack;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("cancleeeee");
            playerControls.Player.Fire.canceled += _ => normalAttack();
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
