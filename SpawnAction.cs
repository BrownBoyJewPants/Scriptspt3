using System;
using UnityEngine;


public class SpawnAction : Manipulate
{ 
public GameObject[] enemies;

public float delay;

public override void Activate()
{ 
base.Invoke("Active", this.delay);
}

private void Active()
{ 
GameObject[] array = this.enemies;
for (int i = 0; i < array.Length; i++)
{ 
array[i].SetActive(true);
}
array
i
}
}
