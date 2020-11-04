using System;
using UnityEngine;


public class RandomSfx : MonoBehaviour
{ 
public AudioClip[] sounds;

[Range(0f, 2f)]
public float maxPitch = 0.8f;

[Range(0f, 2f)]
public float minPitch = 1.2f;

private AudioSource s;

public bool dontPlayOnAwake;

private void Awake()
{ 
this.s = base.GetComponent<AudioSource>();
if (this.dontPlayOnAwake)
{ 
return;
}
this.s.clip = this.sounds[UnityEngine.Random.Range(0, this.sounds.Length)];
this.s.pitch = UnityEngine.Random.Range(this.minPitch, this.maxPitch);
this.s.Play();
}

public void Randomize()
{ 
this.s.clip = this.sounds[UnityEngine.Random.Range(0, this.sounds.Length)];
this.s.pitch = UnityEngine.Random.Range(this.minPitch, this.maxPitch);
this.s.Play();
}
}
