using System;
using UnityEngine;


public class GenerateClouds : MonoBehaviour
{ 
public GameObject cloud;

private int n = 100;

private void Start()
{ 
this.MakeClouds();
}

private void MakeClouds()
{ 
for (int i = 0; i < this.n; i++)
{ 
Vector3 position = base.transform.position + Vector3.right * (float)UnityEngine.Random.Range(-250, 250) + Vector3.forward * (float)UnityEngine.Random.Range(-250, 250) + Vector3.up * (float)UnityEngine.Random.Range(-10, 10);
Vector3 localScale = base.transform.localScale * UnityEngine.Random.Range(0.75f, 1.5f);
Quaternion rotation = Quaternion.Euler((float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360), (float)UnityEngine.Random.Range(0, 360));
UnityEngine.Object.Instantiate<GameObject>(this.cloud, position, rotation).transform.localScale = localScale;
}
i
position
localScale
rotation
}
}
