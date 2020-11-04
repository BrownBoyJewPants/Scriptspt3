using System;
using UnityEngine;


public class Bullet : MonoBehaviour
{ 
public GameObject enemyKillFx;

public GameObject bulletHitFx;

private void Start()
{ 
base.Invoke("DestroySelf", 2f);
}

private void DestroySelf()
{ 
UnityEngine.Object.Destroy(base.gameObject);
}

private void OnCollisionEnter(Collision other)
{ 
int expr_0B = other.gameObject.layer;
if (expr_0B == LayerMask.NameToLayer("Enemy"))
{ 
UnityEngine.Object.Destroy(other.transform.root.gameObject);
UnityEngine.Object.Instantiate<GameObject>(this.enemyKillFx, other.gameObject.transform.position, this.enemyKillFx.transform.rotation);
}
if (expr_0B == LayerMask.NameToLayer("Player"))
{ 
GameManager.Instance.PlayerDied();
}
UnityEngine.Object.Destroy(base.gameObject);
expr_0B
}
}
