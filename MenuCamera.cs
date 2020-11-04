using EZCameraShake;
using System;
using UnityEngine;


public class MenuCamera : MonoBehaviour
{ 
private void Start()
{ 
base.Invoke("StartShake", 0.5f);
}

private void StartShake()
{ 
CameraShaker.Instance.StartShake(2.5f, 0.1f, 0.5f);
}
}
