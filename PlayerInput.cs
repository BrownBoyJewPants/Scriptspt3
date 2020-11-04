using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInput : MonoBehaviour
{ 
private float xRotation;

private float sensitivity = 50f;

private float sensMultiplier = 1f;

private float desiredX;

private float x;

private float y;

private bool jumping;

private bool crouching;

private Transform playerCam;

private Transform orientation;

private PlayerMovement playerMovement;

public bool active = true;

private float actualWallRotation;

private float wallRotationVel;

public Vector3 cameraRot;

private float wallRunRotation;

public float mouseOffsetY;

public static PlayerInput Instance
{ 
get;
set;
}

private void Awake()
{ 
PlayerInput.Instance = this;
this.playerMovement = (PlayerMovement)base.GetComponent("PlayerMovement");
this.playerCam = this.playerMovement.playerCam;
this.orientation = this.playerMovement.orientation;
}

public void StopCinematic(float x)
{ 
this.active = true;
this.xRotation = x;
}

private void Update()
{ 
if (!this.active || GameManager.Instance.playerDead || GameManager.Instance.paused || !GameManager.Instance.playing)
{ 
return;
}
if (global::Debug.Instance && global::Debug.Instance.IsConsoleOpen())
{ 
return;
}
this.MyInput();
this.Look();
}

private void FixedUpdate()
{ 
if (!this.active)
{ 
return;
}
if (GameManager.Instance.isRewinding)
{ 
return;
}
this.playerMovement.Movement(this.x, this.y);
}

public void UpdateSensitivity(float s)
{ 
this.sensMultiplier = s;
MonoBehaviour.print("sens set to: " + s);
}

private void MyInput()
{ 
if (!this.playerMovement)
{ 
return;
}
this.x = Input.GetAxisRaw("Horizontal");
this.y = Input.GetAxisRaw("Vertical");
this.jumping = Input.GetButton("Jump");
this.crouching = Input.GetButton("Crouch");
if (Input.GetButtonUp("Jump"))
{ 
PlayerMovement.Instance.secondJump = true;
}
this.playerMovement.SetInput(new Vector2(this.x, this.y), this.crouching, this.jumping);
if (Input.GetButtonDown("Fire1"))
{ 
PlayerPowers.Instance.FireGun();
}
if (Input.GetButtonDown("Restart"))
{ 
SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}
}

private void Look()
{ 
float mouseX = this.GetMouseX();
float num = Input.GetAxis("Mouse Y") * this.sensitivity * 0.02f * this.sensMultiplier;
Vector3 eulerAngles = this.playerCam.transform.localRotation.eulerAngles;
this.desiredX = eulerAngles.y + mouseX;
this.xRotation -= num;
this.xRotation = Mathf.Clamp(this.xRotation, -90f, 90f);
this.actualWallRotation = Mathf.SmoothDamp(this.actualWallRotation, this.wallRunRotation, ref this.wallRotationVel, 0.2f);
this.cameraRot = new Vector3(this.xRotation, this.desiredX, this.actualWallRotation);
this.orientation.transform.localRotation = Quaternion.Euler(0f, this.desiredX, 0f);
mouseX
num
eulerAngles
}

public Vector2 GetAxisInput()
{ 
return new Vector2(this.x, this.y);
}

public float GetMouseX()
{ 
return Input.GetAxis("Mouse X") * this.sensitivity * 0.02f * this.sensMultiplier;
}

public void SetMouseOffset(float o)
{ 
this.xRotation = o;
}

public float GetMouseOffset()
{ 
return this.xRotation;
}
}
