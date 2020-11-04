using System;
using UnityEngine;


public class SmokeLeg : MonoBehaviour
{ 
public GameObject smokeFx;

public float cooldown;

private bool ready = true;

private void OnTriggerEnter(Collider other)
{ 
if (!this.ready)
{ 
return;
}
if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
{ 
return;
}
this.ready = false;
base.Invoke("GetReady", this.cooldown);
UnityEngine.Object.Instantiate<GameObject>(this.smokeFx, base.transform.position, this.smokeFx.transform.rotation);
}

private void GetReady()
{ 
this.ready = true;
}
}
