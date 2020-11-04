using System;
using UnityEngine;


public class SpawnObjectOnEnemyKill : MonoBehaviour
{ 
private Player player;

public GameObject spawnObject;

private Vector3 size;

private void Awake()
{ 
this.player = base.GetComponent<Player>();
if (!this.spawnObject)
{ 
UnityEngine.Object.Destroy(this);
return;
}
this.spawnObject.SetActive(false);
}

private void Update()
{ 
if (this.player.hp <= 0)
{ 
this.ActivateObject();
UnityEngine.Object.Destroy(this);
}
}

private void ActivateObject()
{ 
this.spawnObject.SetActive(true);
}
}
