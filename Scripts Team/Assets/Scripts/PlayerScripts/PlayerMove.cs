using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5;
    private Rigidbody rB;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    private PlayerControls playerControls;
    private Animator animator;
    public ParticleSystem footStep;
    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();
        footStep = Instantiate(footStep, transform.position, transform.rotation);
        footStep.transform.SetParent(this.transform);
        //transform.LookAt(new Vector3(mainCamera.transform.position.x , transform.position.y , mainCamera.transform.position.z));
    }

    void Start()
    {

        rB = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
    }

    private void OnEnable()
    {
        playerControls?.Enable();
    }

    private void OnDisable()
    {
        playerControls?.Disable();
    }

    private void LookAtMousePostion()
    {
        if (mainCamera != null)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(mainCamera.transform.position, pointToLook, Color.blue);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

            }
        }
        
    }

    void Update()
    {
        if (PhotonNetwork.IsConnected) {
            if (photonView.IsMine) {
                moveInput = new Vector3(playerControls.Player.Move.ReadValue<Vector2>().x, 0, playerControls.Player.Move.ReadValue<Vector2>().y);
                //moveVelocity = moveInput * moveSpeed;
                moveVelocity = new Vector3(moveInput.x, moveInput.y, moveInput.z) * moveSpeed;
                animator.SetFloat("walk", moveVelocity.magnitude);
                LookAtMousePostion();
                rB.velocity = moveVelocity;
            } 
        }
    }

    private void FixedUpdate()
    { 
        rB.AddForce(Vector3.down*10000);
    }

    void FootStep()
    {
        footStep.Play();
    }
}
