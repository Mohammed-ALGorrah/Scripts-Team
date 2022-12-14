using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPhoton : MonoBehaviourPunCallbacks
{
    public Vector3 respawnPos;

    public int team;
    public int playerPos;
    public TeamSyncAll tt;
    public GameObject playerBody;
    public HealthBar playerHealthBar;
    HealthSystem health;
    
    private void Awake()
    {
        health = playerBody.GetComponent<HealthSystem>();
        tt = GameObject.FindObjectOfType<TeamSyncAll>();
    }

    

    private void Start()
    {       
      /*  if (team == 1)
        {
            transform.SetParent(GameObject.Find("FirstTeam").transform);
        }
        else if (team == 2)
        {
            transform.SetParent(GameObject.Find("SecondTeam").transform);
        }*/
    }
    private void OnEnable()
    {
        health.OnDead += Health_OnDead;
    }
    private void OnDisable()
    {
        health.OnDead -= Health_OnDead;
    }
    public Text count;
    float currentTime = 0;
    float startingTime = 5;

    private void Health_OnDead(HealthSystem obj)
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            currentTime -= 1 * Time.deltaTime;
            count.text = currentTime.ToString("0");
            if (currentTime <= 1)
            {
                count.text = "";
            }         
        }
        
    }


    public bool CheckFriend2(int playerTeam)
    {
        if (team == playerTeam)
        {
            Debug.Log("Friend ^^ ");
            return true;
        }
        else
            return false;
    }

    [PunRPC]
    public void showPlayer()
    {
        
        StartCoroutine("ViewPlayer");
    }

    int x = (int)PhotonNetwork.LocalPlayer.CustomProperties["xPos"];
    int z = (int)PhotonNetwork.LocalPlayer.CustomProperties["zPos"];

    IEnumerator ViewPlayer()
    {

        //cache Get Components
        //playerBody.transform.position = respawnPos;
        playerBody.GetComponent<Heros.Players.Player>().transform.position = new Vector3(x,0, z) ;
        yield return new WaitForSeconds(3f);
        
        
        playerBody.SetActive(true);
        playerBody.GetComponent<CapsuleCollider>().enabled = true;
        playerBody.GetComponent<Rigidbody>().useGravity = true;
        
        playerBody.GetComponent<Heros.Players.Player>().health.currentHealth = playerBody.GetComponent<Heros.Players.Player>().playerData.maxHealth;
        playerBody.GetComponent<Heros.Players.Player>().chargeSystem.currentCharge = 0;
        playerBody.GetComponent<Heros.Players.Player>().TopPowrBar.value = 0;
        playerBody.GetComponent<Heros.Players.Player>().CanSpecialAttack = false;
        playerBody.GetComponent<Heros.Players.Player>().fxSpecialAttack.gameObject.SetActive(false);
        playerHealthBar.healthBar.value = 1;
        playerHealthBar.TopHealthBar.value = 1;
        
        if (photonView.IsMine)
        {
            playerBody.GetComponent<PlayerAttack>().enabled = true;
            playerBody.GetComponent<PlayerMove>().enabled = true;
            playerBody.GetComponent<ChargeSystem>().enabled = true;
            playerBody.GetComponent<HealthSystem>().enabled = true;
            playerBody.GetComponent<Heros.Players.Player>().diePanel.SetActive(false);

        }
    }
}
