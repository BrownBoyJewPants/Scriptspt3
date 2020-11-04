using System;
using UnityEngine;


public class ChildPlayer : MonoBehaviour
{ 
private void OnCollisionEnter(Collision other)
{ 
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
PlayerMovement.Instance.transform.parent = base.transform;
}
}

private void OnCollisionExit(Collision other)
{ 
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
PlayerMovement.Instance.transform.parent = null;
}
}
}
