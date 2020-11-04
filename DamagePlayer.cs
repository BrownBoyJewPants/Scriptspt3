using EZCameraShake;
using System;
using UnityEngine;


public class DamagePlayer : MonoBehaviour
{ 
private bool ready = true;

public int damage = 40;

public GameObject blood;

public GameObject swordHit;

public Transform swordTip;

public Transform enemyTorso;

private void OnCollisionEnter(Collision other)
{ 
if (!this.ready)
{ 
return;
}
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
MonoBehaviour.print("rel vel: " + other.relativeVelocity.magnitude);
if (other.relativeVelocity.magnitude < 10f)
{ 
return;
}
this.ready = false;
base.Invoke("GetReady", 0.5f);
CameraShaker.Instance.ShakeOnce(12f, 3f, 0.3f, 0.3f);
if (Sword.Instance.IsBlocking())
{ 
Vector3 to = PlayerMovement.Instance.transform.position - this.enemyTorso.position;
if (Vector3.Angle(PlayerMovement.Instance.orientation.forward, to) > 130f)
{ 
UnityEngine.Object.Instantiate<GameObject>(this.swordHit, this.swordTip.position, Quaternion.identity);
PPController.Instance.StartRewind();
return;
}
}
UnityEngine.Object.Instantiate<GameObject>(this.blood, other.contacts[0].point, Quaternion.identity);
PlayerStatus.Instance.Damage(this.damage);
}
to
}

private void GetReady()
{ 
this.ready = true;
}
}
