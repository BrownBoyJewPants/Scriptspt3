using System;
using UnityEngine;


public class MoveCamera : MonoBehaviour
{ 
public Transform player;

public Vector3 offset;

public Vector3 desyncOffset;

public Vector3 vaultOffset;

private Camera cam;

private Rigidbody rb;

public PlayerInput playerInput;

public bool cinematic;

private float desiredTilt;

private float tilt;

private Vector3 desiredBob;

private Vector3 bobOffset;

private float bobSpeed = 15f;

private float bobMultiplier = 1f;

private readonly float bobConstant = 0.2f;

public Camera mainCam;

public static MoveCamera Instance
{ 
get;
private set;
}

private void Start()
{ 
MoveCamera.Instance = this;
this.cam = base.transform.GetChild(0).GetComponent<Camera>();
this.rb = PlayerMovement.Instance.GetRb();
if (GameState.Instance)
{ 
GameState.Instance.ApplySettings();
}
}

private void LateUpdate()
{ 
this.UpdateBob();
this.MoveGun();
base.transform.position = this.player.transform.position + this.bobOffset + this.desyncOffset + this.vaultOffset + this.offset;
if (this.cinematic)
{ 
return;
}
Vector3 cameraRot = this.playerInput.cameraRot;
cameraRot.x = Mathf.Clamp(cameraRot.x, -90f, 90f);
base.transform.rotation = Quaternion.Euler(cameraRot);
this.desyncOffset = Vector3.Lerp(this.desyncOffset, Vector3.zero, Time.deltaTime * 15f);
this.vaultOffset = Vector3.Slerp(this.vaultOffset, Vector3.zero, Time.deltaTime * 7f);
if (PlayerMovement.Instance.IsCrouching())
{ 
this.desiredTilt = 6f;
}
else
{ 
this.desiredTilt = 0f;
}
this.tilt = Mathf.Lerp(this.tilt, this.desiredTilt, Time.deltaTime * 8f);
Vector3 eulerAngles = base.transform.rotation.eulerAngles;
eulerAngles.z = this.tilt;
base.transform.rotation = Quaternion.Euler(eulerAngles);
cameraRot
eulerAngles
}

private void MoveGun()
{ 
if (!this.rb)
{ 
return;
}
if (Mathf.Abs(this.rb.velocity.magnitude) >= 4f && PlayerMovement.Instance.grounded)
{ 
PlayerMovement.Instance.IsCrouching();
}
}

public void UpdateFov(float f)
{ 
this.mainCam.fieldOfView = f;
}

public void BobOnce(Vector3 bobDirection)
{ 
Vector3 a = this.ClampVector(bobDirection * 0.15f, -3f, 3f);
this.desiredBob = a * this.bobMultiplier;
a
}

private void UpdateBob()
{ 
this.desiredBob = Vector3.Lerp(this.desiredBob, Vector3.zero, Time.deltaTime * this.bobSpeed * 0.5f);
this.bobOffset = Vector3.Lerp(this.bobOffset, this.desiredBob, Time.deltaTime * this.bobSpeed);
}

private Vector3 ClampVector(Vector3 vec, float min, float max)
{ 
return new Vector3(Mathf.Clamp(vec.x, min, max), Mathf.Clamp(vec.y, min, max), Mathf.Clamp(vec.z, min, max));
}
}
