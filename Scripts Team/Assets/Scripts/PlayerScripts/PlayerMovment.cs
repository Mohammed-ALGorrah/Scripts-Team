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
        public float speintSpeed = 8.0f;
        private float gravityValue = -9.81f;

        private PlayerControls playerControls;
        private void Awake()
        {
            controller = gameObject.GetComponent<CharacterController>();
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

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(playerControls.Player.Movement.ReadValue<Vector2>().x, 0, playerControls.Player.Movement.ReadValue<Vector2>().y);
            if (playerControls.Player.Sprint.IsPressed())
            {
                controller.Move(move * Time.deltaTime * speintSpeed);
            }
            else
            {
                controller.Move(move * Time.deltaTime * playerSpeed);
            }
            

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}
