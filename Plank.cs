using System;
using UnityEngine;


public class Plank : MonoBehaviour
{ 
public GameObject plankBreak;

private void OnTriggerEnter(Collider other)
{ 
if (other.gameObject.layer == LayerMask.NameToLayer("Sword"))
{ 
MonoBehaviour.print("yep");
Transform[] componentsInChildren = base.GetComponentsInChildren<Transform>();
for (int i = 0; i < componentsInChildren.Length; i++)
{ 
Transform transform = componentsInChildren[i];
if (!(transform == base.transform))
{ 
Rigidbody expr_52 = transform.gameObject.AddComponent<Rigidbody>();
expr_52.interpolation = RigidbodyInterpolation.Interpolate;
expr_52.AddForce(-base.transform.forward * (float)UnityEngine.Random.Range(50, 750) + base.transform.right * (float)UnityEngine.Random.Range(50, 300) + base.transform.up * (float)UnityEngine.Random.Range(50, 400));
transform.SetParent(null);
transform.gameObject.layer = LayerMask.NameToLayer("GroundOnly");
}
}
UnityEngine.Object.Instantiate<GameObject>(this.plankBreak, base.transform.position, this.plankBreak.transform.rotation);
UnityEngine.Object.Destroy(base.gameObject);
}
componentsInChildren
i
transform
expr_52
}
}
