using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RigidBodyFPS : MonoBehaviour
{

  [SerializeField] InputMaster controls;
  float moveForward;
  float moveSide;
  Vector2 movement = new Vector2();
  Rigidbody player;
  Vector3 movementVector = new Vector3();

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
  // Start is called before the first frame update
  void Start()
  {
    player = GetComponent<Rigidbody>();

  }

  // Update is called once per frame
  void Update()
  {
    ProcessInput();
    ProcessMovement();
  }

  private void ProcessInput()
  {
    controls.Player.MovementVertical.performed += context => movement.x = context.ReadValue<float>();
    controls.Player.MovementHorizontal.performed += context => movement.y = context.ReadValue<float>();


  }


  private void ProcessMovement()
  {
    movementVector = (transform.forward * movement.x) + (transform.right * movement.y) + (transform.up * player.velocity.y);
    // player.AddForce(movementVector, ForceMode.VelocityChange);

    player.velocity = movementVector;
  }
}
