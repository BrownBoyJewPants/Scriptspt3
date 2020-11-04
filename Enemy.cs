using DitzelGames.FastIK;
using System;
using UnityEngine;


public class Enemy : MonoBehaviour
{ 
private Transform target;

public Transform hips;

private float speed = 4f;

public Weapon gun;

public FastIKFabric ik;

private void Start()
{ 
this.target = PlayerMovement.Instance.transform;
this.ReportToGameManager();
this.ik.Target = this.target;
}

private void ReportToGameManager()
{ 
GameManager.Instance.AddEnemy(base.gameObject);
}

private void Update()
{ 
if (GameManager.Instance.isRewinding)
{ 
return;
}
if (!this.target)
{ 
return;
}
Vector3 vector = this.target.transform.position - base.transform.position;
if (Vector3.Angle(base.transform.forward, vector) > 10f)
{ 
this.hips.transform.rotation = Quaternion.Slerp(this.hips.transform.rotation, Quaternion.LookRotation(vector), Time.deltaTime * this.speed);
}
this.hips.transform.rotation = Quaternion.Euler(0f, this.hips.transform.rotation.eulerAngles.y, 0f);
this.ShootLogic();
vector
}

private void ShootLogic()
{ 
Vector3 normalized = (this.target.position - this.gun.transform.position).normalized;
RaycastHit raycastHit;
if (Physics.Raycast(this.gun.transform.position, normalized, out raycastHit, 100f) && raycastHit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
this.gun.cooldown = UnityEngine.Random.Range(0.6f, 2f);
this.gun.Shoot(raycastHit.point);
}
normalized
raycastHit
}
}
