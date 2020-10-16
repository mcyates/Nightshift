using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MouseLook : MonoBehaviour
{
  [SerializeField] InputMaster controls;

  [SerializeField] float mouseSensitivity = 25f;
  [SerializeField] Transform playerBody;

  float xRotation = 0f;
  Vector2 look = new Vector2();

  public bool inMenu = false;

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
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update()
  {
    if (inMenu == false)
    {
      ProcessMouseLook();
    }

    if (controls.Menu.Menu.triggered)
    {
      if (inMenu == false)
      {
        Cursor.lockState = CursorLockMode.Confined;
        inMenu = true;
      }
      else if (inMenu == true)
      {
        Cursor.lockState = CursorLockMode.Locked;
        inMenu = false;
      }
    }
  }

  private void ProcessMouseLook()
  {
    controls.Player.Look.performed += context => look = context.ReadValue<Vector2>();

    look = look * mouseSensitivity * Time.deltaTime;

    xRotation -= look.y;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    playerBody.Rotate(Vector3.up * look.x);
  }
}
