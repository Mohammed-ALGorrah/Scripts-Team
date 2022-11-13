using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5;
    private Rigidbody rB;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    private PlayerControls playerControls;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerControls = new PlayerControls();
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
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up , Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay , out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin ,pointToLook , Color.blue);

            transform.LookAt(new Vector3(pointToLook.x , transform.position.y , pointToLook.z));
        }
    }

    void Update()
    {
        moveInput = new Vector3(playerControls.Player.Movement.ReadValue<Vector2>().x, 0, playerControls.Player.Movement.ReadValue<Vector2>().y);
        moveVelocity = moveInput * moveSpeed;
        animator.SetFloat("walk" , moveVelocity.magnitude);
        LookAtMousePostion();
    }

    private void FixedUpdate()
    {
        rB.velocity = moveVelocity;
    }
}
