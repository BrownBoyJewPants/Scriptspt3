using System;
using UnityEngine;


public class KillPlayer : MonoBehaviour
{ 
public GameObject blood;

private void OnCollisionEnter(Collision other)
{ 
if (GameManager.Instance.isRewinding)
{ 
return;
}
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
GameManager.Instance.PlayerDied();
if (this.blood)
{ 
UnityEngine.Object.Instantiate<GameObject>(this.blood, other.transform.position, Quaternion.identity);
}
}
if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
{ 
Player component = other.transform.root.GetComponent<Player>();
if (component.hp <= 0)
{ 
return;
}
component.Damage(5000, other.transform.position);
}
component
}

private void OnTriggerEnter(Collider other)
{ 
if (GameManager.Instance.isRewinding)
{ 
return;
}
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
GameManager.Instance.PlayerDied();
if (this.blood)
{ 
UnityEngine.Object.Instantiate<GameObject>(this.blood, other.transform.position, Quaternion.identity);
}
}
if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
{ 
Player component = other.transform.root.GetComponent<Player>();
if (component.hp <= 0)
{ 
return;
}
component.Damage(5000, other.transform.position);
}
component
}
}
