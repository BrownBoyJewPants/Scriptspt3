using System;
using TMPro;
using UnityEngine;


public class MainText : MonoBehaviour
{ 
public static MainText Instance;

private TextMeshProUGUI textMesh;

private Vector3 maxSize;

private Vector3 desiredScale;

private float speed = 2f;

private AudioSource audio;

private void Awake()
{ 
MainText.Instance = this;
this.maxSize = base.transform.localScale;
this.audio = base.GetComponent<AudioSource>();
this.textMesh = base.GetComponent<TextMeshProUGUI>();
base.transform.localScale = Vector3.zero;
}

private void Update()
{ 
base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * this.speed);
}

public void PutText(string text)
{ 
this.textMesh.text = text;
this.speed = 0.1f;
base.Invoke("UpSpeed", 0.6f);
base.transform.localScale = this.maxSize;
this.audio.PlayDelayed(0.1f);
}

private void UpSpeed()
{ 
this.speed = 30f;
}
}
