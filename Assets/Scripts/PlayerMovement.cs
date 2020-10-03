using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // float x = Input.GetAxis("Horizontal");
    // float z = Input.GetAxis("Vertical");

    // Vector3 move = transform.right * x + transform.forward * z;

    // move = move * speed * Time.deltaTime;

    controls.Player.Movement.performed += context => movement = context.ReadValue<Vector2>();

    Vector3 move = transform.right * movement.x + transform.forward * movement.y;

    move = move * speed * Time.deltaTime;

    controller.Move(move);

    // if (Input.GetButtonDown("Jump"))
    // {

    // }

    if (controls.Player.Jump.triggered)
    {
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }


    velocity.y += gravity * Time.deltaTime;


    controller.Move(move * Time.deltaTime);


  }
}
