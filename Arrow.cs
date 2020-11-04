using System;
using UnityEngine;


public class Arrow : MonoBehaviour
{ 
private Collider collider;

private Rigidbody rb;

public GameObject swordHit;

public GameObject blood;

public GameObject arrowHit;

private bool done;

private void Awake()
{ 
this.collider = base.GetComponent<Collider>();
this.rb = base.GetComponent<Rigidbody>();
this.collider.enabled = false;
base.Invoke("ActivateCollider", 0.25f);
}

private void ActivateCollider()
{ 
this.collider.enabled = true;
}

private void Update()
{ 
base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Quaternion.LookRotation(this.rb.velocity), Time.deltaTime * 5f);
}

private void OnCollisionEnter(Collision other)
{ 
if (this.done)
{ 
return;
}
this.done = true;
int layer = other.gameObject.layer;
if (layer == LayerMask.NameToLayer("Player"))
{ 
if (Sword.Instance.IsBlocking())
{ 
Vector3 to = PlayerMovement.Instance.transform.position - base.transform.position;
float num = Vector3.Angle(PlayerMovement.Instance.orientation.forward, to);
MonoBehaviour.print("a: " + num);
if (num > 100f)
{ 
UnityEngine.Object.Instantiate<GameObject>(this.swordHit, base.transform.position, Quaternion.identity);
UnityEngine.Object.Destroy(base.gameObject);
return;
}
}
UnityEngine.Object.Instantiate<GameObject>(this.blood, other.contacts[0].point, Quaternion.identity);
PlayerStatus.Instance.Damage(90);
UnityEngine.Object.Destroy(base.gameObject);
return;
}
if (layer == LayerMask.NameToLayer("Enemy"))
{ 
Player component = other.transform.root.GetComponent<Player>();
if (!component)
{ 
return;
}
component.Damage(100, base.transform.position);
UnityEngine.Object.Destroy(base.gameObject);
UnityEngine.Object.Instantiate<GameObject>(this.blood, other.contacts[0].point, Quaternion.identity);
return;
}
else
{ 
if (layer != LayerMask.NameToLayer("Ground"))
{ 
UnityEngine.Object.Destroy(base.gameObject);
return;
}
base.GetComponent<Rigidbody>().isKinematic = true;
UnityEngine.Object.Destroy(base.transform.GetComponentInChildren<AudioSource>());
UnityEngine.Object.Destroy(this);
base.gameObject.AddComponent<DestroyObject>().time = 5f;
this.collider.enabled = false;
ParticleSystemRenderer component2 = UnityEngine.Object.Instantiate<GameObject>(this.arrowHit, other.contacts[0].point, Quaternion.identity).GetComponent<ParticleSystemRenderer>();
Renderer component3 = other.gameObject.GetComponent<Renderer>();
if (!component3)
{ 
return;
}
component2.material = component3.material;
return;
}
layer
to
num
component
component2
component3
}
}
