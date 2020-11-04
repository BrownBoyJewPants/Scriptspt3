using System;
using UnityEngine;


public class ControlMusic : MonoBehaviour
{ 
private float desiredFreq;

private void Awake()
{ 
}

private void Update()
{ 
if (!MusicController.Instance)
{ 
return;
}
float b = 0.02f;
if (PlayerMovement.Instance.GetVelocity().magnitude > 15f && (!GameManager.Instance.playerDead & GameManager.Instance.playing))
{ 
b = 1f;
}
this.desiredFreq = Mathf.Lerp(this.desiredFreq, b, Time.deltaTime * 5f);
MusicController.Instance.SetFreq(this.desiredFreq);
b
}
}
