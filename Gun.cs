using System;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{ 
private Rigidbody rb;

private Transform playerCam;

private Vector3 startPos;

private List<Vector3> velHistory;

private Vector3 desiredBob;

private float xBob = 0.12f;

private float yBob = 0.08f;

private float zBob = 0.1f;

private float bobSpeed = 0.45f;

private Vector3 recoilOffset;

private Vector3 recoilRotation;

private Vector3 recoilOffsetVel;

private Vector3 recoilRotVel;

private float reloadRotation;

private float desiredReloadRotation;

private float reloadTime;

private float rVel;

private float reloadPosOffset;

private float rPVel;

private float gunDrag = 0.2f;

public float currentGunDragMultiplier = 1f;

private float desX;

private float desY;

private Vector3 speedBob;

private float reloadProgress;

private int spins;

public static Gun Instance
{ 
get;
set;
}

private void Start()
{ 
Gun.Instance = this;
this.velHistory = new List<Vector3>();
this.startPos = base.transform.localPosition;
this.rb = PlayerMovement.Instance.GetRb();
this.playerCam = PlayerMovement.Instance.playerCam;
}

private void Update()
{ 
if (!PlayerMovement.Instance || GameManager.Instance.playerDead || GameManager.Instance.paused || !GameManager.Instance.playing)
{ 
return;
}
this.MovementBob();
this.ReloadGun();
this.RecoilGun();
this.SpeedBob();
float b = -Input.GetAxis("Mouse X") * this.gunDrag * this.currentGunDragMultiplier;
float b2 = -Input.GetAxis("Mouse Y") * this.gunDrag * this.currentGunDragMultiplier;
this.desX = Mathf.Lerp(this.desX, b, Time.unscaledDeltaTime * 10f);
this.desY = Mathf.Lerp(this.desY, b2, Time.unscaledDeltaTime * 10f);
this.Rotation(new Vector2(this.desX, this.desY));
Vector3 b3 = this.startPos + new Vector3(this.desX, this.desY, 0f) + this.desiredBob + this.recoilOffset + new Vector3(0f, -this.reloadPosOffset, 0f) + this.speedBob;
base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, b3, Time.unscaledDeltaTime * 15f);
b
b2
b3
}

private void Rotation(Vector2 offset)
{ 
float num = offset.magnitude * 0.03f;
if (offset.x < 0f)
{ 
num = -num;
}
float arg_31_0 = offset.y;
float num2 = -offset.x;
Vector3 euler = new Vector3(arg_31_0 * 80f + this.reloadRotation, num2 * 40f, num * 50f) + this.recoilRotation;
try
{ 
if (Time.deltaTime > 0f)
{ 
base.transform.localRotation = Quaternion.Lerp(base.transform.localRotation, Quaternion.Euler(euler), Time.deltaTime * 20f);
}
}
catch (Exception)
{ 
}
num
arg_31_0
num2
euler
}

public void SpawnGun()
{ 
base.transform.localPosition += Vector3.down * 2f;
base.transform.GetChild(0).gameObject.SetActive(true);
}

private void MovementBob()
{ 
if (!this.rb)
{ 
return;
}
if (Mathf.Abs(this.rb.velocity.magnitude) < 4f || !PlayerMovement.Instance.grounded || PlayerMovement.Instance.IsCrouching())
{ 
this.desiredBob = Vector3.zero;
return;
}
float x = Mathf.PingPong(Time.time * this.bobSpeed, this.xBob) - this.xBob / 2f;
float y = Mathf.PingPong(Time.time * this.bobSpeed, this.yBob) - this.yBob / 2f;
float z = Mathf.PingPong(Time.time * this.bobSpeed, this.zBob) - this.zBob / 2f;
this.desiredBob = new Vector3(x, y, z);
x
y
z
}

private void SpeedBob()
{ 
Vector2 vector = PlayerMovement.Instance.FindVelRelativeToLook();
Vector3 vector2 = new Vector3(vector.x, PlayerMovement.Instance.GetVelocity().y, vector.y);
vector2 *= -0.01f;
vector2 = Vector3.ClampMagnitude(vector2, 0.6f);
this.speedBob = Vector3.Lerp(this.speedBob, vector2, Time.deltaTime * 10f);
vector
vector2
}

public void Shoot()
{ 
float d = 1.5f;
this.recoilOffset += -(Vector3.forward + Vector3.up * 0.3f - Vector3.right * 0.35f);
this.recoilRotation += -new Vector3(90f, UnityEngine.Random.Range(10f, 30f), UnityEngine.Random.Range(-50f, 50f)) * d;
d
}

private void RecoilGun()
{ 
this.recoilOffset = Vector3.SmoothDamp(this.recoilOffset, Vector3.zero, ref this.recoilOffsetVel, 0.05f);
this.recoilRotation = Vector3.SmoothDamp(this.recoilRotation, Vector3.zero, ref this.recoilRotVel, 0.07f);
}

private void ReloadGun()
{ 
this.reloadProgress += Time.deltaTime;
this.reloadRotation = Mathf.Lerp(0f, this.desiredReloadRotation, this.reloadProgress / this.reloadTime);
this.reloadPosOffset = Mathf.SmoothDamp(this.reloadPosOffset, 0f, ref this.rPVel, this.reloadTime * 0.2f);
if (this.reloadRotation / 360f > (float)this.spins)
{ 
this.spins++;
}
}

public void Reload(float time, int spinAmount)
{ 
this.reloadProgress = 0f;
this.reloadRotation = 0f;
this.reloadTime = time;
this.spins = 0;
int num = spinAmount;
if (num < 1)
{ 
num = Mathf.RoundToInt(time * 3f);
}
this.desiredReloadRotation = (float)(-360 * num);
this.reloadPosOffset = 0.45f;
MonoBehaviour.print("rel time on gun: " + this.reloadTime);
num
}

public void ChangeWeapon()
{ 
base.transform.localPosition = Vector3.down * 1f;
this.Reload(1f, 0);
}
}
