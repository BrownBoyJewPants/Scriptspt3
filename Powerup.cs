using System;
using UnityEngine;


public abstract class Powerup : MonoBehaviour, IPowerup
{ 
public GameObject destroyFx;

private Collider collider;

private Vector3 size;

private void Awake()
{ 
this.collider = base.GetComponent<Collider>();
this.collider.enabled = false;
base.Invoke("EnableCollider", 0.75f);
this.size = base.transform.localScale;
base.transform.localScale = Vector3.zero;
}

private void Update()
{ 
base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.size, Time.deltaTime * 1.5f);
}

public abstract void Activate();

private void OnTriggerEnter(Collider other)
{ 
if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
{ 
return;
}
this.Activate();
UnityEngine.Object.Destroy(base.gameObject);
GameManager.Instance.StartRewind();
if (!this.destroyFx)
{ 
return;
}
UnityEngine.Object.Instantiate<GameObject>(this.destroyFx, base.transform.position, base.transform.rotation);
}

private void EnableCollider()
{ 
this.collider.enabled = true;
}
}
