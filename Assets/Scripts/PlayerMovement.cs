using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] InputMaster controls;

  [SerializeField] CharacterController controller;
  [SerializeField] Transform groundCheck;

  [SerializeField] float speed = 12f;
  [SerializeField] float gravity = -9.82f;
  [SerializeField] float jumpHeight = 1.5f;

  [SerializeField] float groundDistance = 0.4f;
  [SerializeField] LayerMask groundMask;

  bool isGrounded;
  Vector2 movement = new Vector2();

  Vector3 velocity;
  Vector3 drag = new Vector3(0.2f, 0.2f, 0.2f);


  private void OnEnable()
  {
    if (controls == null)
    {
      controls = new InputMaster();
    }
    controls.Enable();
  }

  private void OnDisable()
  {
    controls.Disable();
  }

  void Update()
  {
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    if (isGrounded && velocity.y < 0)
    {
      velocity.y = -1f;
    }

    controls.Player.MovementHorizontal.performed += context => movement.x = context.ReadValue<float>();
    controls.Player.MovementVertical.performed += context => movement.y = context.ReadValue<float>();

    controls.Player.Dash.performed += context =>
    {
      if (context.interaction is TapInteraction)
      {
        // Dash();
      }
      else if (context.interaction is HoldInteraction)
      {
        // Sprint();
      }
    };


    Vector3 move = transform.right * movement.x + transform.forward * movement.y;

    move = move * speed * Time.deltaTime;

    controller.Move(move);


    if (controls.Player.Jump.triggered)
    {
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }


    velocity.y += gravity * Time.deltaTime;

    move += velocity;

    controller.Move(move * Time.deltaTime);

  }
}
