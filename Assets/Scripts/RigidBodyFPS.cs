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


  public Vector2 movement = new Vector2();
  public Vector3 movementVector = new Vector3();

  [SerializeField] float moveSpeed = 7f;
  float speedBoost = 2f;

  public bool isSprinting = false;

  public bool isGrounded = true;

  #region Jumping
  [SerializeField] int maxJumpsAllowed = 2;
  int jumpsLeft;
  float jumpHeight = 2f;

  #endregion


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

    jumpsLeft = maxJumpsAllowed;

  }

  void Update()
  {
    // ProcessMovement();
  }

  void FixedUpdate()
  {
    ProcessMovement();

  }

  private void ProcessMovement()
  {
    controls.Player.MovementVertical.performed += context => movement.x = context.ReadValue<float>();
    controls.Player.MovementHorizontal.performed += context => movement.y = context.ReadValue<float>();

    controls.Player.Dash.performed += ctx => isSprinting = true;
    controls.Player.Dash.canceled += ctx => isSprinting = false;

    if (isSprinting == true)
    {
      moveSpeed = Mathf.Clamp(moveSpeed * speedBoost, 0f, 15f);
    }
    else
    {
      moveSpeed = 7f;
    }


    movementVector = (transform.forward * movement.x * moveSpeed) + (transform.right * movement.y * moveSpeed) + (transform.up * player.velocity.y);


    if (controls.Player.Jump.triggered)
    {
      Jump();
    }

    Vector3.ClampMagnitude(movementVector, 20f);
    player.velocity = movementVector;
  }


  private void Jump()
  {
    float jumpFloat = Mathf.Sqrt(jumpHeight * -2f * -9.81f);
    if (isGrounded == true)
    {
      player.AddForce(transform.up * jumpFloat, ForceMode.Impulse);
    }

    // double jump reset jumpsleft in OnCollisionEnter
    if (jumpsLeft > 0 && isGrounded == false)
    {
      player.AddForce(transform.up * jumpFloat, ForceMode.Impulse);

    }
    jumpsLeft--;
  }


  private void OnCollisionEnter(Collision collision)
  {
    Vector3 normal = collision.contacts[0].normal;

    if (collision.gameObject.layer == 0)
    {

      if (normal.y > 0)
      {
        isGrounded = true;
        jumpsLeft = maxJumpsAllowed;
      }
    }
  }

  private void OnCollisionExit(Collision collision)
  {
    Vector3 normal;
    if (collision.contacts.Length > 0)
    {
      normal = collision.contacts[0].normal;

      if (collision.gameObject.layer == 0)
      {
        if (normal.y > 0)
        {
          isGrounded = false;
        }
      }
    }
  }
}
