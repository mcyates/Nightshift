﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class RigidBodyFPS : MonoBehaviour
{

  [SerializeField] InputMaster controls;
  Rigidbody player;


  Vector2 movement = new Vector2();
  Vector3 movementVector = new Vector3();

  [SerializeField] float moveSpeed = 7f;
  float jumpHeight = 2f;
  float speedBoost = 2f;

  bool isSprinting = false;


  bool isGrounded = true;


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
    ProcessInputAxis();
    ProcessMovement();
  }

  private void ProcessInputAxis()
  {
    // if (controls.Player.Dash.triggered)
    // {
    //   isSprinting = !isSprinting;
    // }

    controls.Player.MovementVertical.performed += context => movement.x = context.ReadValue<float>() * moveSpeed;
    controls.Player.MovementHorizontal.performed += context => movement.y = context.ReadValue<float>() * moveSpeed;
  }


  private void ProcessMovement()
  {

    movementVector = (transform.forward * movement.x) + (transform.right * movement.y) + (transform.up * player.velocity.y);


    if (isGrounded && movementVector.y < 0)
    {
      movementVector.y = -1f;
    }

    if (controls.Player.Jump.triggered && isGrounded == true)
    {
      // movementVector.y = Jump();
      player.AddForce(transform.up * Jump(), ForceMode.VelocityChange);

    }

    if (isSprinting == true)
    {
      moveSpeed = moveSpeed * speedBoost;
    }
    else
    {
      moveSpeed = 7f;
    }


    player.velocity = movementVector;
  }

  private float Jump()
  {
    // isGrounded = false;
    return Mathf.Sqrt(jumpHeight * -2f * -9.81f);
  }


  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.layer == 0)
    {
      isGrounded = true;
    }
  }

  private void OnCollisionExit(Collision collision)
  {
    if (collision.gameObject.layer == 0)
    {
      isGrounded = false;
    }
  }
}
