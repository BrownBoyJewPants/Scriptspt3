using EZCameraShake;
using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSword : MonoBehaviour
{ 
private List<Player> enemiesHit;

public GameObject arrow;

public RandomSfx sfx;

private void Awake()
{ 
this.enemiesHit = new List<Player>();
}

private void OnTriggerEnter(Collider other)
{ 
if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
{ 
Player component = other.transform.root.GetComponent<Player>();
if (this.enemiesHit.Contains(component))
{ 
return;
}
this.enemiesHit.Add(component);
CameraShaker.Instance.ShakeOnce(6f, 8f, 0.2f, 0.2f);
other.transform.root.GetComponent<Player>().Damage(50 + (int)(PlayerMovement.Instance.GetVelocity().magnitude * 1.4f), other.transform.position);
Vector3 a = PlayerMovement.Instance.playerCam.forward + PlayerMovement.Instance.GetVelocity() * 0.08f;
component.GetTorso().GetComponent<Rigidbody>().AddForce(a * 5000f);
this.sfx.Randomize();
Hitmarker.Instance.StartHitmarker();
}
component
a
}

public void ResetList()
{ 
this.enemiesHit = new List<Player>();
}
}
