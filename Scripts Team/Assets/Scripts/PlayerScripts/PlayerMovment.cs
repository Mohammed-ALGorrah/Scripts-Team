using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovment : MonoBehaviour
    {

        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        public float playerSpeed = 5.0f;
        private float gravityValue = -9.81f;

        private PlayerControls playerControls;
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            playerControls?.Enable();
        }

        private void OnDisable()
        {
            playerControls?.Disable();
        }


        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<CharacterController>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("walk", speed);
        }
        void Update()
        {


            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(playerControls.Player.Movement.ReadValue<Vector2>().x, 0, playerControls.Player.Movement.ReadValue<Vector2>().y);
            controller.Move(move * Time.deltaTime * playerSpeed);

            UpdateAnimator();

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            
        }
    }
}
