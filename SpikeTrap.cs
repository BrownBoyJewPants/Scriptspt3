using System;
using UnityEngine;


public class SpikeTrap : MonoBehaviour
{ 
public Transform spikes;

private Vector3 outPos;

private Vector3 restPos;

public AudioSource sfx;

private bool ready = true;

private void Awake()
{ 
this.outPos = base.transform.position;
this.restPos = this.outPos + Vector3.down;
this.spikes.position = this.restPos;
}

private void OnTriggerEnter(Collider other)
{ 
if (!this.ready)
{ 
return;
}
this.ready = false;
this.spikes.position = this.outPos;
this.sfx.Play();
base.Invoke("ResetSpikes", 2f);
}

private void ResetSpikes()
{ 
this.ready = true;
this.spikes.position = this.restPos;
}
}
