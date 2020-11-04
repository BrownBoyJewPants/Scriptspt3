using EZCameraShake;
using System;
using UnityEngine;


public class RotateObject : Manipulate
{ 
public Vector3 offsetRotation;

public float speed = 1f;

private Quaternion startRotation;

private Quaternion desiredRotation;

private bool active;

private AudioSource sfx;

public float magnitude;

public float roughness;

public float inT;

public float outT;

private void Awake()
{ 
this.sfx = base.GetComponent<AudioSource>();
this.startRotation = base.transform.rotation;
this.desiredRotation = Quaternion.Euler(this.startRotation.eulerAngles + this.offsetRotation);
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
base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.desiredRotation, Time.deltaTime * this.speed);
}
}
