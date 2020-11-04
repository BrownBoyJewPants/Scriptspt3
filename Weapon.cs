using System;
using UnityEngine;


public class Weapon : MonoBehaviour
{ 
public GameObject bullet;

public GameObject muzzle;

public Transform gunTip;

public float force = 3000f;

public float cooldown = 0.4f;

private bool readyToShoot = true;

public bool Shoot(Vector3 hitPoint)
{ 
if (!this.readyToShoot)
{ 
return false;
}
Vector3 normalized = (hitPoint - this.gunTip.position).normalized;
UnityEngine.Object.Instantiate<GameObject>(this.muzzle, this.gunTip.position, Quaternion.identity);
UnityEngine.Object.Instantiate<GameObject>(this.bullet, this.gunTip.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce(normalized * this.force);
this.readyToShoot = false;
base.Invoke("GetReady", this.cooldown);
return true;
normalized
}

private void GetReady()
{ 
this.readyToShoot = true;
}
}
