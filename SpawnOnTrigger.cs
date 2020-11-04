using System;
using UnityEngine;


public class SpawnOnTrigger : MonoBehaviour
{ 
public GameObject[] toSpawn;

private void Start()
{ 
GameObject[] array = this.toSpawn;
for (int i = 0; i < array.Length; i++)
{ 
array[i].SetActive(false);
}
array
i
}

private void OnTriggerEnter(Collider other)
{ 
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
UnityEngine.Object.Destroy(base.gameObject);
GameObject[] array = this.toSpawn;
for (int i = 0; i < array.Length; i++)
{ 
array[i].SetActive(true);
}
}
array
i
}
}
