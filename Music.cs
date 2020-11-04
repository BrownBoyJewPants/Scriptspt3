using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Music : MonoBehaviour
{ 
public enum MusicType
{ 
Ambient,
Intense,
Battle
}

private AudioSource music;

private float fadeSpeed = 1f;

private float desiredVolume = 0.7f;

private AudioClip ambient;

private AudioClip intense;

private AudioClip battle;

public static Music Instance
{ 
get;
private set;
}

private void Awake()
{ 
Music.Instance = this;
this.music = base.GetComponent<AudioSource>();
this.ambient = Resources.Load<AudioClip>("Music/ambient");
this.intense = Resources.Load<AudioClip>("Music/intense");
this.battle = Resources.Load<AudioClip>("Music/battle");
base.StartCoroutine(this.loadMusic());
}

public void PlayMusic(Music.MusicType t, float fadeSpeed)
{ 
this.fadeSpeed = fadeSpeed;
this.desiredVolume = 1f;
switch (t)
{ 
case Music.MusicType.Ambient: 
this.music.clip = this.ambient;
this.music.Play();
return;
case Music.MusicType.Intense: 
this.music.clip = this.intense;
this.music.Play();
return;
case Music.MusicType.Battle: 
this.music.clip = this.battle;
this.music.Play();
return;
default: 
return;
 }
}

public void StopMusic(float fadeSpeed)
{ 
this.fadeSpeed = fadeSpeed;
this.desiredVolume = 0f;
}

private void Update()
{ 
this.music.volume = Mathf.Lerp(this.music.volume, this.desiredVolume, Time.deltaTime * this.fadeSpeed);
}

[IteratorStateMachine(typeof(Music.<loadMusic>d__14))]
private IEnumerator loadMusic()
{ 
int num;
WWW wWW;
WWW wWW2;
WWW wWW3;
while (num == 0)
{ 
string str = Application.dataPath + "/Resources/Music";
wWW = new WWW("file:/" + str + "/ambient.ogg");
wWW2 = new WWW("file:/" + str + "/intense.ogg");
wWW3 = new WWW("file:/" + str + "/battle.ogg");
yield return wWW;
}
if (num != 1)
{ 
yield break;
}
this.ambient = wWW.GetAudioClip(false);
this.intense = wWW2.GetAudioClip(false);
this.battle = wWW3.GetAudioClip(false);
this.music.clip = this.ambient;
this.music.Play();
yield break;
num
str
wWW
wWW2
wWW3
}
}
