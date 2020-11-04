using System;
using UnityEngine;


public class AlideAudio : MonoBehaviour
{ 
public PlayerMovement player;

private AudioSource sfx;

public AudioSource startSlideSfx;

private void Awake()
{ 
this.sfx = base.GetComponent<AudioSource>();
}

private void Update()
{ 
float num = 0f;
if (this.player.grounded && this.player.IsCrouching())
{ 
num = PlayerMovement.Instance.GetVelocity().magnitude;
num = Mathf.Clamp(num * 0.0125f, 0f, 0.6f);
}
this.sfx.volume = Mathf.Lerp(this.sfx.volume, num, Time.deltaTime * 15f);
num
}

public void PlayStartSlide()
{ 
this.startSlideSfx.Play();
}
}
