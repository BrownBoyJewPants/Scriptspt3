using EZCameraShake;
using System;
using UnityEngine;


public class MoveObject : Manipulate
{ 
public Vector3 offset;

public float speed = 1f;

private Vector3 startPos;

private bool active;

private AudioSource sfx;

public float magnitude;

public float roughness;

public float inT;

public float outT;

public bool autoPlay;

private void Awake()
{ 
this.startPos = base.transform.position;
this.sfx = base.GetComponentInChildren<AudioSource>();
}

private void Start()
{ 
if (this.autoPlay)
{ 
this.Activate();
}
}

public override void Activate()
{ 
this.active = true;
if (this.sfx)
{ 
this.sfx.Play();
}
if (this.magnitude > 0f)
{ 
CameraShaker.Instance.ShakeOnce(this.magnitude, this.roughness, this.inT, this.outT);
}
}

private void Update()
{ 
if (!this.active)
{ 
return;
}
base.transform.position = Vector3.Lerp(base.transform.position, this.startPos + this.offset, Time.deltaTime * this.speed);
}
}
