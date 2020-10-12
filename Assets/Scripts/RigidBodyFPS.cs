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
    controls.Player.MovementHorizontal.performed += context => movement.x = context.ReadValue<float>();
    controls.Player.MovementVertical.performed += context => movement.y = context.ReadValue<float>();


  }


  private void ProcessMovement()
  {
    // player.AddForce()
    Vector3 intendedMovement = new Vector3(0, 0, 0);
    intendedMovement = (transform.forward * movement.x) + (transform.right * movement.y) + (transform.up * player.velocity.y);
    player.velocity = intendedMovement;
  }
}
