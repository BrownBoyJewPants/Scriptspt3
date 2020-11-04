using EZCameraShake;
using System;
using UnityEngine;


public class Hitmarker : MonoBehaviour
{ 
public static Hitmarker Instance;

private Vector3 maxSize;

private Vector3 desiredScale;

private float speed = 2f;

private AudioSource audio;

public string[] texts;

private void Awake()
{ 
Hitmarker.Instance = this;
this.maxSize = base.transform.localScale;
this.audio = base.GetComponent<AudioSource>();
base.transform.localScale = Vector3.zero;
}

private void Update()
{ 
base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.desiredScale, Time.deltaTime * this.speed);
}

public void StartHitmarker()
{ 
this.speed = 40f;
base.Invoke("UpSpeed", 0.1f);
this.desiredScale = this.maxSize;
base.Invoke("DelayRemove", 0.09f);
CameraShaker.Instance.ShakeOnce(4f, 5f, 0.2f, 0.2f);
}

private void DelayRemove()
{ 
this.desiredScale = Vector3.zero;
}

private void UpSpeed()
{ 
this.speed = 25f;
}
}
