using System;
using UnityEngine;


public class PlayerController : MonoBehaviour
{ 
private Player player;

private RigidEnemy ikController;

public Transform target;

public Player enemy;

public Rigidbody weapon;

public Rigidbody[] armsRb;

public Rigidbody rootRb;

public Rigidbody torsoRb;

public float distance = 5.5f;

public bool archer;

public Transform bow;

public GameObject arrow;

private bool attacking;

private bool readyToAttack = true;

public RandomSfx sfx;

public float attackHoldForce;

public float attackHoldLength;

[Range(20f, 75f)]
public float LaunchAngle = 45f;

private void Awake()
{ 
this.player = base.GetComponent<Player>();
this.ikController = base.GetComponent<RigidEnemy>();
this.readyToAttack = false;
base.Invoke("GetReadyToAttack", UnityEngine.Random.Range(1f, 2f));
}

private void Start()
{ 
this.target = PlayerMovement.Instance.transform;
}

private void FixedUpdate()
{ 
if (this.player.hp < 1 || this.ikController.state != RigidEnemy.EnemyState.active)
{ 
return;
}
if (this.rootRb.transform.position.y < -50f)
{ 
this.player.Damage(2000, Vector3.zero);
}
if (!this.target)
{ 
return;
}
Vector3 normalized = (this.target.position - this.ikController.root.position).normalized;
float num = Vector3.Distance(this.target.position, this.ikController.root.position);
this.MoveLogic(normalized, num);
this.ikController.RotateBody(normalized);
if (num < this.distance)
{ 
if (this.archer)
{ 
this.Launch();
}
else
{ 
this.Attack(normalized);
}
}
this.AttackLogic();
normalized
num
}

private void LiftArms()
{ 
Rigidbody[] array = this.armsRb;
for (int i = 0; i < array.Length; i++)
{ 
array[i].AddForce(Vector3.up * 35f);
this.rootRb.AddForce(Vector3.down * 35f);
}
array
i
}

private void MoveLogic(Vector3 moveDir, float distanceFromTarget)
{ 
int num = 1;
if ((double)distanceFromTarget < 4.3)
{ 
num = -1;
}
this.ikController.MoveBody(moveDir * (float)num);
num
}

private void Attack(Vector3 dir)
{ 
if (!this.readyToAttack)
{ 
return;
}
this.readyToAttack = false;
base.Invoke("GetReadyToAttack", UnityEngine.Random.Range(0.7f, 3f));
this.weapon.AddForce(dir * 3000f);
this.sfx.Randomize();
if (this.attackHoldForce <= 0f)
{ 
return;
}
this.attacking = true;
base.Invoke("StopAttacking", this.attackHoldLength);
}

private void StopAttacking()
{ 
this.attacking = false;
}

private void AttackLogic()
{ 
if (!this.attacking)
{ 
return;
}
Vector3 a = this.target.position - this.weapon.position;
this.weapon.AddForce(a * this.attackHoldForce);
this.torsoRb.AddForce(-a * this.attackHoldForce);
a
}

private void Launch()
{ 
if (!this.readyToAttack)
{ 
return;
}
this.readyToAttack = false;
base.Invoke("GetReadyToAttack", UnityEngine.Random.Range(1.2f, 3.5f));
GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.arrow, this.bow.position, Quaternion.identity);
Vector3 arg_BF_0 = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
Vector3 vector = new Vector3(this.target.position.x, gameObject.transform.position.y, this.target.position.z);
gameObject.transform.LookAt(vector);
float num = Vector3.Distance(arg_BF_0, vector);
this.LaunchAngle = UnityEngine.Random.Range(5f, Mathf.Clamp(num, 0f, 60f));
float arg_125_0 = Physics.gravity.y;
float num2 = Mathf.Tan(this.LaunchAngle * 0.0174532924f);
float num3 = this.target.position.y - gameObject.transform.position.y;
float num4 = Mathf.Sqrt(arg_125_0 * num * num / (2f * (num3 - num * num2)));
float y = num2 * num4;
Vector3 direction = new Vector3(0f, y, num4);
Vector3 velocity = gameObject.transform.TransformDirection(direction);
gameObject.GetComponent<Rigidbody>().velocity = velocity;
gameObject
arg_BF_0
vector
num
arg_125_0
num2
num3
num4
y
direction
velocity
}

private void GetReadyToAttack()
{ 
this.readyToAttack = true;
}

public void SetTarget(Transform t)
{ 
this.target = t;
}

public void SetSpeed(int s)
{ 
this.ikController.moveSpeed += (float)s;
}
}
