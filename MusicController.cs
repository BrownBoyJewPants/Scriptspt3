using System;
using UnityEngine;


public class MusicController : MonoBehaviour
{ 
public AudioSource music;

private AudioLowPassFilter lowpass;

private float desiredFreq = 440f;

public static MusicController Instance;

private void Awake()
{ 
MusicController.Instance = this;
this.lowpass = base.GetComponent<AudioLowPassFilter>();
}

public void SetFreq(float f)
{ 
this.desiredFreq = 22000f * f;
}

private void Update()
{ 
this.lowpass.cutoffFrequency = Mathf.Lerp(this.lowpass.cutoffFrequency, this.desiredFreq, Time.deltaTime * 2f);
}

public void UpdateMusic(float f)
{ 
this.music.volume = f;
}
}
