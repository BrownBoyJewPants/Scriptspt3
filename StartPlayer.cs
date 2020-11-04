using System;
using UnityEngine;


public class StartPlayer : MonoBehaviour
{ 
public bool spawnWeapon;

private void Start()
{ 
for (int i = base.transform.childCount - 1; i >= 0; i--)
{ 
base.transform.GetChild(i).parent = null;
}
UnityEngine.Object.Destroy(base.gameObject);
if (this.spawnWeapon)
{ 
Sword.Instance.Pickup();
}
i
}
}
