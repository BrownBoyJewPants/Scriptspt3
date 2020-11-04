using EZCameraShake;
using System;
using UnityEngine;


public class FallBlock : MonoBehaviour
{ 
private Vector3 startPos;

public float startTime = 2f;

public float speed = 2f;

public Vector3 offset = new Vector3(0f, -35f, 0f);

private Vector3 vel;

private bool done;

private bool falling;

private void Awake()
{ 
this.startPos = base.transform.position;
}

private void OnCollisionEnter(Collision other)
{ 
if (this.done)
{ 
return;
}
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
base.Invoke("StartFall", this.startTime);
this.done = true;
CameraShaker.Instance.ShakeOnce(1f, 7f, 1f, 1f);
}
}

private void StartFall()
{ 
this.falling = true;
}

private void Update()
{ 
if (!this.falling)
{ 
return;
}
base.transform.position = Vector3.SmoothDamp(base.transform.position, this.startPos + this.offset, ref this.vel, this.speed);
}
}
