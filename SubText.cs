using EZCameraShake;
using System;
using TMPro;
using UnityEngine;


public class SubText : MonoBehaviour
{ 
public static SubText Instance;

private TextMeshProUGUI textMesh;

private Vector3 maxSize;

private Vector3 desiredScale;

private float speed = 2f;

private AudioSource audio;

public string[] texts;

private void Awake()
{ 
SubText.Instance = this;
this.maxSize = base.transform.localScale;
this.audio = base.GetComponent<AudioSource>();
this.textMesh = base.GetComponent<TextMeshProUGUI>();
base.transform.localScale = Vector3.zero;
}

private void Update()
{ 
base.transform.localScale = Vector3.Lerp(base.transform.localScale, this.desiredScale, Time.deltaTime * this.speed);
}

public void PutText()
{ 
this.textMesh.text = this.texts[UnityEngine.Random.Range(0, this.texts.Length)];
this.speed = 25f;
base.Invoke("UpSpeed", 0.6f);
this.desiredScale = this.maxSize;
this.audio.PlayDelayed(0.1f);
base.Invoke("DelayRemove", 0.5f);
CameraShaker.Instance.ShakeOnce(7f, 5f, 0.3f, 0.3f);
}

private void DelayRemove()
{ 
this.desiredScale = Vector3.zero;
}

private void UpSpeed()
{ 
this.speed = 30f;
}
}
