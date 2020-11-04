using System;
using UnityEngine;


public class PlayerAudio : MonoBehaviour
{ 
private Rigidbody rb;

public AudioSource wind;

public AudioSource foley;

private float currentVol;

private float volVel;

private void Start()
{ 
this.rb = PlayerMovement.Instance.GetRb();
}

private void Update()
{ 
}
}
