using System;
using UnityEngine;


public class PickupSword : MonoBehaviour
{ 
private bool ready;

private void Awake()
{ 
base.Invoke("GetReady", 0.5f);
}

private void GetReady()
{ 
this.ready = true;
}

private void OnTriggerEnter(Collider other)
{ 
if (!this.ready)
{ 
return;
}
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
if (Sword.Instance.pickedUp)
{ 
return;
}
Sword.Instance.Pickup();
UnityEngine.Object.Destroy(base.transform.parent.gameObject);
}
}

private void OnTriggerStay(Collider other)
{ 
if (!this.ready)
{ 
return;
}
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
if (Sword.Instance.pickedUp)
{ 
return;
}
Sword.Instance.Pickup();
UnityEngine.Object.Destroy(base.transform.parent.gameObject);
}
}
}
