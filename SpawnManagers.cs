using System;
using UnityEngine;


public class SpawnManagers : MonoBehaviour
{ 
public GameObject managers;

private void Awake()
{ 
if (!Managers.Instance)
{ 
UnityEngine.Object.Instantiate<GameObject>(this.managers);
}
if (MusicController.Instance)
{ 
MusicController.Instance.SetFreq(0.02f);
}
UnityEngine.Object.Destroy(base.gameObject);
}

private void Start()
{ 
}
}
