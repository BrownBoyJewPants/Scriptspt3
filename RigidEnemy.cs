using System;
using UnityEngine;


[RequireComponent(typeof(IkEnemy))]
public class RigidEnemy : MonoBehaviour
{ 
public enum EnemyState
{ 
active,
tumbling,
falling,
recovering,
dead
}

public Transform root;

public Transform head;

public Transform torso;

private Rigidbody rb;

private Rigidbody headRb;

private Rigidbody torsoRb;

private float force;

[HideInInspector]
public RigidEnemy.EnemyState state = RigidEnemy.EnemyState.recovering;

[HideInInspector]
public IkEnemy ik;

private Transform[] groundChecks;

public bool minOneGrounded;

private int nLegs;

public float legPushForce = 0.55f;

public float groundCheckRadius = 0.2f;

public float moveLegsWithSpeedScale = 0.25f;

public float moveSpeed = 10f;

private float rotationForce = 0.01f;

public float maxRotationForce = 0.05f;

private float stabilizeForce = 1f;

public float recoverTime = 2f;

public float recoveryForce = 0.3f;

public float tumbleAngle = 30f;

public float fallAngle = 70f;

public float getupMagT = 0.2f;

public float getupAng = 15f;

private bool ragdoll;

[HideInInspector]
public bool recovering;

private void Start()
{ 
this.rb = this.root.GetComponent<Rigidbody>();
if (this.head)
{ 
this.headRb = this.head.GetComponent<Rigidbody>();
}
else
{ 
this.headRb = this.rb;
}
if (this.torso)
{ 
this.torsoRb = this.torso.GetComponent<Rigidbody>();
}
else
{ 
this.torsoRb = this.rb;
}
this.ik = base.GetComponent<IkEnemy>();
this.CaluclateForce();
this.UpdateState(RigidEnemy.EnemyState.active);
this.nLegs = this.ik.legs.Length;
this.groundChecks = new Transform[this.nLegs];
for (int i = 0; i < this.nLegs; i++)
{ 
this.groundChecks[i] = this.ik.legs[i].transform;
}
this.DisableSelfCollision(true);
i
}

private void FixedUpdate()
{ 
if (this.state == RigidEnemy.EnemyState.dead)
{ 
return;
}
this.minOneGrounded = false;
for (int i = 0; i < this.nLegs; i++)
{ 
if (Physics.CheckSphere(this.groundChecks[i].position, this.groundCheckRadius, this.ik.whatIsGround))
{ 
this.minOneGrounded = true;
}
}
float num = 0f;
if (this.state == RigidEnemy.EnemyState.active || this.state == RigidEnemy.EnemyState.tumbling || this.state == RigidEnemy.EnemyState.recovering || this.state == RigidEnemy.EnemyState.falling)
{ 
RaycastHit raycastHit;
if (!Physics.Raycast(this.root.position, Vector3.down, out raycastHit, this.ik.heightAboveGround * 3f, this.ik.whatIsGround))
{ 
this.UpdateState(RigidEnemy.EnemyState.falling);
}
else
{ 
num = raycastHit.distance;
}
}
float num2 = Vector3.Angle(Vector3.up, this.root.up);
if (this.state == RigidEnemy.EnemyState.falling)
{ 
if (num != 0f && num < this.ik.heightAboveGround * 1.5f && num2 < 50f)
{ 
this.UpdateState(RigidEnemy.EnemyState.active);
base.CancelInvoke("GetUp");
this.ConfigureLegs(false);
this.recovering = false;
return;
}
if (!base.IsInvoking("GetUp"))
{ 
base.Invoke("GetUp", this.recoverTime);
}
return;
}
else
{ 
if (this.state == RigidEnemy.EnemyState.recovering)
{ 
bool flag = Physics.CheckSphere(this.root.position, 0.5f, this.ik.whatIsGround);
if (num < this.ik.heightAboveGround | flag)
{ 
this.headRb.AddForce(Vector3.up * this.force * this.recoveryForce * 1.1f);
this.rb.AddForce(Vector3.up * this.force * this.recoveryForce * 0.9f);
}
if ((num2 < this.getupAng && this.torsoRb.velocity.magnitude < this.getupMagT) || (num > this.ik.heightAboveGround * 0.85f && num < this.ik.heightAboveGround * 1.85f && num2 < 30f))
{ 
this.UpdateState(RigidEnemy.EnemyState.active);
base.CancelInvoke("RecoveryCooldown");
base.Invoke("RecoveryCooldown", 2f);
}
return;
}
if (this.state == RigidEnemy.EnemyState.active && this.rb.velocity.magnitude < 1f && num > this.ik.heightAboveGround && num < this.ik.heightAboveGround + this.ik.heightAboveGround * 0.1f)
{ 
this.headRb.AddForce(Vector3.up * this.force * 0.86f);
return;
}
float d = Mathf.Clamp(1f - this.RootHeight() / this.ik.heightAboveGround, -1f, 1f);
if (num2 < this.tumbleAngle)
{ 
this.UpdateState(RigidEnemy.EnemyState.active);
}
else if (num2 < this.fallAngle)
{ 
this.UpdateState(RigidEnemy.EnemyState.tumbling);
}
else if (num2 > this.fallAngle)
{ 
this.UpdateState(RigidEnemy.EnemyState.falling);
}
if (this.minOneGrounded)
{ 
this.rb.AddForce(this.root.up * this.force * d * 2f);
this.rb.AddForce(this.root.up * this.force * this.legPushForce);
}
if (num < this.ik.heightAboveGround * 2f)
{ 
this.StabilizingBody();
}
return;
}
i
num
raycastHit
num2
flag
d
}

private void RecoveryCooldown()
{ 
this.recovering = false;
}

public void Concuss()
{ 
this.UpdateState(RigidEnemy.EnemyState.falling);
this.ConfigureLegs(true);
this.recovering = true;
base.Invoke("GetUp", this.recoverTime * UnityEngine.Random.Range(0.7f, 1.5f));
}

private void GetUp()
{ 
if (Physics.CheckSphere(this.root.position, this.ik.heightAboveGround * 0.5f, this.ik.whatIsGround))
{ 
this.UpdateState(RigidEnemy.EnemyState.recovering);
this.ConfigureLegs(false);
return;
}
base.Invoke("GetUp", this.recoverTime);
}

private void ConfigureLegs(bool makeRagdoll)
{ 
if (makeRagdoll == this.ragdoll)
{ 
return;
}
this.ragdoll = makeRagdoll;
for (int i = 0; i < this.ik.legs.Length; i++)
{ 
int j = this.ik.legs[i].ChainLength;
Transform transform = this.ik.legs[i].transform;
while (j > 0)
{ 
transform = transform.parent;
if (makeRagdoll)
{ 
transform.gameObject.AddComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
}
else
{ 
UnityEngine.Object.Destroy(transform.gameObject.GetComponent<Joint>());
}
j--;
}
Rigidbody[] componentsInChildren = transform.GetComponentsInChildren<Rigidbody>();
for (int k = 0; k < componentsInChildren.Length; k++)
{ 
Rigidbody expr_8F = componentsInChildren[k];
expr_8F.isKinematic = !makeRagdoll;
expr_8F.interpolation = (makeRagdoll ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None);
}
this.ik.legs[i].enabled = !makeRagdoll;
this.ik.ForceCurrentPosition(i);
}
i
j
transform
componentsInChildren
k
expr_8F
}

private float RootHeight()
{ 
RaycastHit raycastHit;
if (Physics.Raycast(this.root.position, Vector3.down, out raycastHit, 10f, this.ik.whatIsGround))
{ 
return raycastHit.distance;
}
return 0f;
raycastHit
}

private void StabilizingBody()
{ 
this.headRb.AddForce(Vector3.up * this.force * this.stabilizeForce);
this.torsoRb.AddForce(Vector3.down * this.force * this.stabilizeForce);
}

private void CaluclateForce()
{ 
float num = 0f;
Rigidbody[] componentsInChildren = base.GetComponentsInChildren<Rigidbody>();
for (int i = 0; i < componentsInChildren.Length; i++)
{ 
Rigidbody rigidbody = componentsInChildren[i];
if (!rigidbody.isKinematic)
{ 
num += rigidbody.mass;
}
}
this.force = num * -Physics.gravity.y;
num
componentsInChildren
i
rigidbody
}

public void RotateBody(Vector3 dir)
{ 
float arg_2A_0 = this.root.transform.eulerAngles.y;
float y = Quaternion.LookRotation(dir).eulerAngles.y;
float num = Mathf.DeltaAngle(arg_2A_0, y);
num = Mathf.Clamp(num, -2f, 2f);
this.rb.AddTorque(Vector3.up * num * this.force * this.rotationForce);
arg_2A_0
y
num
}

public void MoveBody(Vector3 dir)
{ 
this.rb.AddForce(dir * this.moveSpeed * this.rb.mass);
this.headRb.AddForce(dir * this.moveSpeed * this.headRb.mass);
this.torsoRb.AddForce(dir * this.moveSpeed * this.torsoRb.mass);
}

public void UpdateState(RigidEnemy.EnemyState s)
{ 
if (this.state == s)
{ 
return;
}
this.state = s;
switch (s)
{ 
case RigidEnemy.EnemyState.active: 
this.ConfigureRb(5f, 5f, this.maxRotationForce, 1f);
return;
case RigidEnemy.EnemyState.tumbling: 
this.ConfigureRb(1f, 4f, 0f, 0.1f);
return;
case RigidEnemy.EnemyState.falling: 
this.ConfigureRb(0f, 0f, 0f, 0f);
this.Concuss();
return;
case RigidEnemy.EnemyState.recovering: 
this.ConfigureRb(4f, 4f, this.maxRotationForce, 0.15f);
return;
case RigidEnemy.EnemyState.dead: 
this.ConfigureRb(0f, 0f, 0f, 0f);
this.KillRigidEnemy();
return;
default: 
this.rb.drag = 0f;
this.rb.angularDrag = 0f;
return;
 }
}

public void KillRigidEnemy()
{ 
this.DisableSelfCollision(false);
this.ConfigureLegs(true);
base.CancelInvoke();
Transform[] componentsInChildren = base.transform.GetComponentsInChildren<Transform>();
for (int i = 0; i < componentsInChildren.Length; i++)
{ 
Transform transform = componentsInChildren[i];
if (transform.CompareTag("GrapplePoint"))
{ 
UnityEngine.Object.Destroy(transform.gameObject);
}
transform.tag = "Dead";
}
this.ik.CollectGarbage();
base.gameObject.AddComponent<DestroyObject>().time = 10f;
UnityEngine.Object.Destroy(this);
UnityEngine.Object.Destroy(this.ik);
componentsInChildren
i
transform
}

private void ConfigureRb(float drag, float angularDrag, float rotation, float stabilize)
{ 
if (drag != -1f)
{ 
this.rb.drag = drag;
this.torsoRb.drag = drag;
}
if (angularDrag != -1f)
{ 
this.rb.angularDrag = angularDrag;
this.torsoRb.angularDrag = angularDrag;
}
if (this.rotationForce != -1f)
{ 
this.rotationForce = rotation;
}
if (stabilize != -1f)
{ 
this.stabilizeForce = stabilize;
}
}

public Vector3 GetVelocity()
{ 
if (!this.rb)
{ 
return Vector3.zero;
}
Vector3 result = this.rb.velocity * this.moveLegsWithSpeedScale;
if (result.magnitude > 1f)
{ 
return result.normalized;
}
return result;
result
}

private void DisableSelfCollision(bool ignore)
{ 
try
{ 
Collider[] componentsInChildren = base.GetComponentsInChildren<Collider>();
for (int i = 0; i < componentsInChildren.Length; i++)
{ 
for (int j = i; j < componentsInChildren.Length; j++)
{ 
Physics.IgnoreCollision(componentsInChildren[i], componentsInChildren[j], ignore);
}
}
}
catch (Exception)
{ 
}
componentsInChildren
i
j
}
}
