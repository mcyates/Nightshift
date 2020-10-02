using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
  [SerializeField] float mouseSensitivity = 110f;
  [SerializeField] Transform playerBody;

  float xRotation = 0f;
  float mouseX = 0f;
  float mouseY = 0f;


  // Start is called before the first frame update
  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update()
  {
    mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -89f, 89f);


    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    playerBody.Rotate(Vector3.up * mouseX);




    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Cursor.lockState = CursorLockMode.Confined;
    }
  }
}
