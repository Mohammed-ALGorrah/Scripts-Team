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
    public Text test;
    public TeamSyncAll tt;
    public GameObject playerBody;
    public HealthBar playerHealthBar;
    HealthSystem health;
    [SerializeField]
    Text KillsTxt,DeathTxt;
    private void Awake()
    {
        health = playerBody.GetComponent<HealthSystem>();
        tt = GameObject.FindObjectOfType<TeamSyncAll>();
        test = GameObject.Find("TextTest").GetComponent<Text>();
    }

    

    private void Start()
    {
        
        if (team == 1)
        {
            //transform.position = tt.SpwanPoints[playerPos].position;
            transform.SetParent(GameObject.Find("FirstTeam").transform);
            test.text = PhotonNetwork.LocalPlayer.CustomProperties["postion"].ToString();
        }
        else if (team == 2)
        {
            //transform.position = tt.SpwanPoints[playerPos].position;
            transform.SetParent(GameObject.Find("SecondTeam").transform);
            test.text = PhotonNetwork.LocalPlayer.CustomProperties["postion"].ToString();
        }
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
            //    KillsTxt.text = "K " + playerBody.GetComponent<Heros.Players.Player>().numOfKills;
               
            DeathTxt.text = "D " + PhotonNetwork.LocalPlayer.CustomProperties["dead"];


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

    IEnumerator ViewPlayer()
    {

        yield return new WaitForSeconds(3f);
        playerBody.transform.position = respawnPos;
        playerBody.SetActive(true);
        playerBody.GetComponent<CapsuleCollider>().enabled = true;
        playerBody.GetComponent<Rigidbody>().useGravity = true;
        playerBody.GetComponent<Heros.Players.Player>().health.currentHealth = playerBody.GetComponent<Heros.Players.Player>().playerData.maxHealth;
        playerBody.GetComponent<Heros.Players.Player>().chargeSystem.currentCharge = 0;
        playerBody.GetComponent<Heros.Players.Player>().CanSpecialAttack = false;
        playerBody.GetComponent<Heros.Players.Player>().fxSpecialAttack.gameObject.SetActive(false);
        playerHealthBar.healthBar.value = 1;

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
