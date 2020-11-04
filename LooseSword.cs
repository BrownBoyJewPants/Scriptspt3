using System;
using UnityEngine;


public class LooseSword : MonoBehaviour
{ 
private bool ready = true;

private Rigidbody rb;

private Collider collider;

public GameObject hitSfx;

public bool player;

private void Awake()
{ 
this.rb = base.GetComponent<Rigidbody>();
this.collider = base.GetComponent<Collider>();
}

private void OnCollisionEnter(Collision other)
{ 
if (!this.ready)
{ 
return;
}
if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
{ 
if (other.relativeVelocity.magnitude < 15f)
{ 
return;
}
Player arg_A4_0 = other.transform.root.GetComponent<Player>();
this.ready = false;
this.collider.enabled = false;
base.Invoke("GetReady", 0.25f);
other.transform.root.GetComponent<Player>().Damage(100, other.transform.position);
Vector3 a = this.rb.velocity * 0.2f;
arg_A4_0.GetTorso().GetComponent<Rigidbody>().AddForce(a * 500f);
this.rb.velocity = Vector3.zero;
this.rb.angularVelocity = Vector3.zero;
Vector3 normalized = (Vector3.up * 1.4f + (PlayerMovement.Instance.transform.position - base.transform.position).normalized).normalized;
float d = Mathf.Clamp(Vector3.Distance(base.transform.position, PlayerMovement.Instance.transform.position) * 0.06f, 0.65f, 1f);
this.rb.AddForce(normalized * 4500f * d);
UnityEngine.Object.Instantiate<GameObject>(this.hitSfx, base.transform.position, Quaternion.identity);
if (this.player)
{ 
Hitmarker.Instance.StartHitmarker();
}
}
this.player = false;
arg_A4_0
a
normalized
d
}

private void GetReady()
{ 
this.ready = true;
this.collider.enabled = true;
}

public void RemoveCollision()
{ 
this.collider.enabled = false;
base.Invoke("GetReady", 0.25f);
}
}
