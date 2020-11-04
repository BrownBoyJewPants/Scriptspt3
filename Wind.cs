using System;
using UnityEngine;


public class Wind : MonoBehaviour
{ 
private AudioSource wind;

private void Awake()
{ 
this.wind = base.GetComponent<AudioSource>();
}

private void Update()
{ 
float num = PlayerMovement.Instance.GetVelocity().magnitude / 60f;
num = Mathf.Clamp(num, 0f, 0.85f);
if (!PlayerMovement.Instance.grounded)
{ 
num *= 2f;
}
this.wind.volume = Mathf.Lerp(this.wind.volume, num, Time.deltaTime * 5f);
num
}
}
