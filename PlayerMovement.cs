using EZCameraShake;
using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{ 
public GameObject playerJumpSmokeFx;

public GameObject footstepFx;

public Transform playerCam;

public Transform orientation;

private Rigidbody rb;

public bool dead;

public bool exploded;

private float moveSpeed = 6000f;

private float maxSpeed = 22f;

public bool grounded;

public LayerMask whatIsGround;

private Vector3 crouchScale = new Vector3(1f, 1.05f, 1f);

private Vector3 playerScale;

private float slideForce = 800f;

private float slideCounterMovement = 0.12f;

private bool readyToJump = true;

private float jumpCooldown = 0.25f;

private float jumpForce = 13f;

private float x;

private float y;

private float mouseDeltaX;

private float mouseDeltaY;

private bool jumping;

private bool sliding;

private bool crouching;

private Vector3 normalVector;

public ParticleSystem ps;

private ParticleSystem.EmissionModule psEmission;

private Collider playerCollider;

private float fallSpeed;

private Vector3 lastMoveSpeed;

private float playerHeight;

public GameObject playerSmokeFx;

public AlideAudio slideAudio;

private float distance;

private int ticks;

private bool onRamp;

public bool simulate;

public bool secondJump = true;

[HideInInspector]
public int jumpsLeft = 1;

[HideInInspector]
public int maxJumps = 1;

private int resetJumpCounter;

private int jumpCounterResetTime = 10;

private float counterMovement = 0.14f;

private float threshold = 0.01f;

private int readyToCounterX;

private int readyToCounterY;

private bool cancelling;

private float maxSlopeAngle = 35f;

private bool airborne;

private bool onGround;

private bool surfing;

private bool cancellingGrounded;

private bool cancellingSurf;

private float delay = 5f;

private int groundCancel;

private int wallCancel;

private int surfCancel;

public LayerMask whatIsHittable;

private float vel;

public static PlayerMovement Instance
{ 
get;
private set;
}

private void Awake()
{ 
PlayerMovement.Instance = this;
this.rb = base.GetComponent<Rigidbody>();
this.playerHeight = base.GetComponent<CapsuleCollider>().bounds.size.y;
}

private void Start()
{ 
this.playerScale = base.transform.localScale;
this.playerCollider = base.GetComponent<Collider>();
this.psEmission = this.ps.emission;
Cursor.lockState = CursorLockMode.Locked;
Cursor.visible = false;
this.CameraShake();
}

private void Update()
{ 
if (this.dead)
{ 
return;
}
this.FootSteps();
this.fallSpeed = this.rb.velocity.y;
this.lastMoveSpeed = VectorExtensions.XZVector(this.rb.velocity);
}

public void SetInput(Vector2 dir, bool crouching, bool jumping)
{ 
this.x = dir.x;
this.y = dir.y;
this.crouching = crouching;
this.jumping = jumping;
}

private void CheckInput()
{ 
if (this.crouching && !this.sliding)
{ 
this.StartCrouch();
}
if (!this.crouching && this.sliding)
{ 
this.StopCrouch();
}
}

public void StartCrouch()
{ 
if (this.sliding)
{ 
return;
}
this.sliding = true;
base.transform.localScale = this.crouchScale;
base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.65f, base.transform.position.z);
if (this.rb.velocity.magnitude > 0.5f && this.grounded)
{ 
this.rb.AddForce(this.orientation.transform.forward * this.slideForce);
this.slideAudio.PlayStartSlide();
}
}

public void StopCrouch()
{ 
this.sliding = false;
base.transform.localScale = this.playerScale;
base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 0.65f, base.transform.position.z);
}

private void FootSteps()
{ 
if (this.crouching || this.dead)
{ 
return;
}
if (this.grounded)
{ 
float num = 1f;
float num2 = this.rb.velocity.magnitude;
if (num2 > 20f)
{ 
num2 = 20f;
}
this.distance += num2 * Time.deltaTime * 50f;
if (this.distance > 300f / num)
{ 
UnityEngine.Object.Instantiate<GameObject>(this.footstepFx, base.transform.position, Quaternion.identity);
this.distance = 0f;
}
}
num
num2
}

public void Movement(float x, float y)
{ 
this.UpdateCollisionChecks();
this.SpeedLines();
this.x = x;
this.y = y;
if (this.dead)
{ 
return;
}
this.CheckInput();
if (!this.grounded)
{ 
this.rb.AddForce(Vector3.down * 2f);
}
Vector2 vector = this.FindVelRelativeToLook();
float num = vector.x;
float num2 = vector.y;
this.CounterMovement(x, y, vector);
this.RampMovement(vector);
if (this.readyToJump && this.jumping)
{ 
this.Jump();
}
if (this.crouching && this.grounded && this.readyToJump)
{ 
this.rb.AddForce(Vector3.down * 60f);
return;
}
float num3 = x;
float num4 = y;
if (x > 0f && num > this.maxSpeed)
{ 
num3 = 0f;
}
if (x < 0f && num < -this.maxSpeed)
{ 
num3 = 0f;
}
if (y > 0f && num2 > this.maxSpeed)
{ 
num4 = 0f;
}
if (y < 0f && num2 < -this.maxSpeed)
{ 
num4 = 0f;
}
float d = 1f;
float d2 = 1f;
if (!this.grounded)
{ 
d = 0.6f;
d2 = 0.6f;
if (this.IsHoldingAgainstVerticalVel(vector))
{ 
float num5 = Mathf.Abs(vector.y * 0.025f);
if (num5 < 0.5f)
{ 
num5 = 0.5f;
}
d2 = Mathf.Abs(num5);
}
}
if (this.grounded && this.crouching)
{ 
d2 = 0f;
}
if (this.surfing)
{ 
d = 0.6f;
d2 = 0.3f;
}
float d3 = 0.01f;
this.rb.AddForce(this.orientation.forward * num4 * this.moveSpeed * 0.02f * d2);
this.rb.AddForce(this.orientation.right * num3 * this.moveSpeed * 0.02f * d);
if (!this.grounded)
{ 
if (num3 != 0f)
{ 
this.rb.AddForce(-this.orientation.forward * vector.y * this.moveSpeed * 0.02f * d3);
}
if (num4 != 0f)
{ 
this.rb.AddForce(-this.orientation.right * vector.x * this.moveSpeed * 0.02f * d3);
}
}
if (!this.readyToJump)
{ 
this.resetJumpCounter++;
if (this.resetJumpCounter >= this.jumpCounterResetTime)
{ 
this.ResetJump();
}
}
vector
num
num2
num3
num4
d
d2
num5
d3
}

private void RampMovement(Vector2 mag)
{ 
if (this.grounded && this.onRamp && !this.surfing && !this.crouching && !this.jumping && Math.Abs(this.x) < 0.05f && Math.Abs(this.y) < 0.05f)
{ 
this.rb.useGravity = false;
if (this.rb.velocity.y > 0f)
{ 
this.rb.velocity = new Vector3(this.rb.velocity.x, 0f, 0f);
return;
}
if (this.rb.velocity.y <= 0f && Math.Abs(mag.magnitude) < 1f)
{ 
this.rb.velocity = Vector3.zero;
return;
}
}
else
{ 
this.rb.useGravity = true;
}
}

private void SpeedLines()
{ 
float num = Vector3.Angle(this.rb.velocity, this.playerCam.transform.forward) * 0.15f;
if (num < 1f)
{ 
num = 1f;
}
float rateOverTimeMultiplier = this.rb.velocity.magnitude / num;
if (this.grounded)
{ 
rateOverTimeMultiplier = 0f;
}
this.psEmission.rateOverTimeMultiplier = rateOverTimeMultiplier;
num
rateOverTimeMultiplier
}

private void CameraShake()
{ 
float num = this.rb.velocity.magnitude / 9f;
CameraShaker.Instance.ShakeOnce(num, 0.1f * num, 0.25f, 0.2f);
base.Invoke("CameraShake", 0.2f);
num
}

private void ResetJump()
{ 
this.readyToJump = true;
}

public void Jump()
{ 
if ((this.grounded || this.surfing || this.jumpsLeft > 0) && this.readyToJump && this.secondJump)
{ 
this.readyToJump = false;
this.jumpsLeft--;
this.resetJumpCounter = 0;
this.secondJump = false;
this.rb.AddForce(Vector2.up * this.jumpForce * 1.5f, ForceMode.Impulse);
this.rb.AddForce(this.normalVector * this.jumpForce * 0.5f, ForceMode.Impulse);
Vector3 velocity = this.rb.velocity;
if (this.rb.velocity.y < 0.5f)
{ 
this.rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
}
else if (this.rb.velocity.y > 0f)
{ 
this.rb.velocity = new Vector3(velocity.x, 0f, velocity.z);
}
ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = UnityEngine.Object.Instantiate<GameObject>(this.playerJumpSmokeFx, base.transform.position, Quaternion.LookRotation(Vector3.up)).GetComponent<ParticleSystem>().velocityOverLifetime;
velocityOverLifetime.x = this.rb.velocity.x * 2f;
velocityOverLifetime.z = this.rb.velocity.z * 2f;
}
velocity
velocityOverLifetime
}

private void CounterMovement(float x, float y, Vector2 mag)
{ 
if (!this.grounded || this.jumping || this.exploded)
{ 
return;
}
if (this.crouching)
{ 
this.rb.AddForce(this.moveSpeed * 0.02f * -this.rb.velocity.normalized * this.slideCounterMovement);
return;
}
if (Math.Abs(mag.x) > this.threshold && Math.Abs(x) < 0.05f && this.readyToCounterX > 1)
{ 
this.rb.AddForce(this.moveSpeed * this.orientation.transform.right * 0.02f * -mag.x * this.counterMovement);
}
if (Math.Abs(mag.y) > this.threshold && Math.Abs(y) < 0.05f && this.readyToCounterY > 1)
{ 
this.rb.AddForce(this.moveSpeed * this.orientation.transform.forward * 0.02f * -mag.y * this.counterMovement);
}
if (this.IsHoldingAgainstHorizontalVel(mag))
{ 
this.rb.AddForce(this.moveSpeed * this.orientation.transform.right * 0.02f * -mag.x * this.counterMovement * 2f);
}
if (this.IsHoldingAgainstVerticalVel(mag))
{ 
this.rb.AddForce(this.moveSpeed * this.orientation.transform.forward * 0.02f * -mag.y * this.counterMovement * 2f);
}
if (Mathf.Sqrt(Mathf.Pow(this.rb.velocity.x, 2f) + Mathf.Pow(this.rb.velocity.z, 2f)) > this.maxSpeed)
{ 
float num = this.rb.velocity.y;
Vector3 vector = this.rb.velocity.normalized * this.maxSpeed;
this.rb.velocity = new Vector3(vector.x, num, vector.z);
}
if (Math.Abs(x) < 0.05f)
{ 
this.readyToCounterX++;
}
else
{ 
this.readyToCounterX = 0;
}
if (Math.Abs(y) < 0.05f)
{ 
this.readyToCounterY++;
return;
}
this.readyToCounterY = 0;
num
vector
}

private bool IsHoldingAgainstHorizontalVel(Vector2 vel)
{ 
return (vel.x < -this.threshold && this.x > 0f) || (vel.x > this.threshold && this.x < 0f);
}

private bool IsHoldingAgainstVerticalVel(Vector2 vel)
{ 
return (vel.y < -this.threshold && this.y > 0f) || (vel.y > this.threshold && this.y < 0f);
}

public Vector2 FindVelRelativeToLook()
{ 
float arg_42_0 = this.orientation.transform.eulerAngles.y;
float target = Mathf.Atan2(this.rb.velocity.x, this.rb.velocity.z) * 57.29578f;
float num = Mathf.DeltaAngle(arg_42_0, target);
float num2 = 90f - num;
float expr_7E = new Vector2(this.rb.velocity.x, this.rb.velocity.z).magnitude;
float num3 = expr_7E * Mathf.Cos(num * 0.0174532924f);
return new Vector2(expr_7E * Mathf.Cos(num2 * 0.0174532924f), num3);
arg_42_0
target
num
num2
expr_7E
num3
}

private bool IsFloor(Vector3 v)
{ 
return Vector3.Angle(Vector3.up, v) < this.maxSlopeAngle;
}

private bool IsSurf(Vector3 v)
{ 
float num = Vector3.Angle(Vector3.up, v);
return num < 89f && num > this.maxSlopeAngle;
num
}

private bool IsWall(Vector3 v)
{ 
return Math.Abs(90f - Vector3.Angle(Vector3.up, v)) < 0.1f;
}

private bool IsRoof(Vector3 v)
{ 
return v.y == -1f;
}

private void OnCollisionEnter(Collision other)
{ 
int layer = other.gameObject.layer;
Vector3 normal = other.contacts[0].normal;
if (this.whatIsGround != (this.whatIsGround | 1 << layer))
{ 
return;
}
if (this.IsFloor(normal))
{ 
this.jumpsLeft = this.maxJumps;
this.secondJump = true;
MoveCamera.Instance.BobOnce(new Vector3(0f, this.fallSpeed, 0f));
if (this.fallSpeed < -15f)
{ 
Vector3 point = other.contacts[0].point;
ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = UnityEngine.Object.Instantiate<GameObject>(this.playerSmokeFx, point, Quaternion.LookRotation(base.transform.position - point)).GetComponent<ParticleSystem>().velocityOverLifetime;
velocityOverLifetime.x = this.rb.velocity.x * 2f;
velocityOverLifetime.z = this.rb.velocity.z * 2f;
}
}
float num = 1.3f;
if (this.IsWall(normal))
{ 
Vector3 normalized = this.lastMoveSpeed.normalized;
Vector3 vector = base.transform.position + Vector3.up * 1.6f;
UnityEngine.Debug.DrawLine(vector, vector + normalized * num, Color.blue, 10f);
if (Physics.Raycast(vector, normalized, num, this.whatIsGround))
{ 
return;
}
RaycastHit raycastHit;
if (!Physics.Raycast(vector + normalized * num, Vector3.down, out raycastHit, 3f, this.whatIsGround))
{ 
return;
}
Vector3 vector2 = raycastHit.point + Vector3.up * this.playerHeight * 0.5f;
MoveCamera.Instance.vaultOffset += base.transform.position - vector2;
base.transform.position = vector2;
this.rb.velocity = this.lastMoveSpeed * 0.4f;
this.jumpsLeft = this.maxJumps;
}
layer
normal
point
velocityOverLifetime
num
normalized
vector
raycastHit
vector2
}

private void OnCollisionStay(Collision other)
{ 
int layer = other.gameObject.layer;
if (this.whatIsGround != (this.whatIsGround | 1 << layer))
{ 
return;
}
for (int i = 0; i < other.contactCount; i++)
{ 
Vector3 normal = other.contacts[i].normal;
if (this.IsFloor(normal))
{ 
if (!this.grounded && this.crouching)
{ 
this.slideAudio.PlayStartSlide();
}
if (Vector3.Angle(Vector3.up, normal) > 1f)
{ 
this.onRamp = true;
}
else
{ 
this.onRamp = false;
}
this.grounded = true;
this.normalVector = normal;
this.cancellingGrounded = false;
this.groundCancel = 0;
}
if (this.IsSurf(normal))
{ 
this.surfing = true;
this.cancellingSurf = false;
this.surfCancel = 0;
}
}
layer
i
normal
}

private void UpdateCollisionChecks()
{ 
if (!this.cancellingGrounded)
{ 
this.cancellingGrounded = true;
}
else
{ 
this.groundCancel++;
if ((float)this.groundCancel > this.delay)
{ 
this.StopGrounded();
}
}
if (!this.cancellingSurf)
{ 
this.cancellingSurf = true;
this.surfCancel = 1;
return;
}
this.surfCancel++;
if ((float)this.surfCancel > this.delay)
{ 
this.StopSurf();
}
}

private void StopGrounded()
{ 
this.grounded = false;
}

private void StopSurf()
{ 
this.surfing = false;
}

public Vector3 GetVelocity()
{ 
return this.rb.velocity;
}

public float GetFallSpeed()
{ 
return this.rb.velocity.y;
}

public Collider GetPlayerCollider()
{ 
return this.playerCollider;
}

public Transform GetPlayerCamTransform()
{ 
return this.playerCam.transform;
}

public Vector3 HitPoint()
{ 
RaycastHit[] array = Physics.RaycastAll(this.playerCam.transform.position, this.playerCam.transform.forward, 100f, this.whatIsHittable);
if (array.Length < 1)
{ 
return this.playerCam.transform.position + this.playerCam.transform.forward * 100f;
}
if (array.Length > 1)
{ 
for (int i = 0; i < array.Length; i++)
{ 
if (array[i].transform.gameObject.layer == LayerMask.NameToLayer("Enemy") || array[i].transform.gameObject.layer == LayerMask.NameToLayer("Object") || array[i].transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
{ 
return array[i].point;
}
}
}
return array[0].point;
array
i
}

public bool IsCrouching()
{ 
return this.crouching;
}

public bool IsDead()
{ 
return this.dead;
}

public Rigidbody GetRb()
{ 
return this.rb;
}
}
