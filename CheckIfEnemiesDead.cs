using System;
using UnityEngine;


public class CheckIfEnemiesDead : MonoBehaviour
{ 
public Manipulate[] actions;

public Player[] enemies;

public float delay;

private bool done;

private void Update()
{ 
if (this.done)
{ 
return;
}
int num = 0;
Player[] array = this.enemies;
for (int i = 0; i < array.Length; i++)
{ 
Player player = array[i];
if (!player || player.hp <= 0)
{ 
num++;
}
}
if (num >= this.enemies.Length)
{ 
this.done = true;
base.Invoke("Activate", this.delay);
}
num
array
i
player
}

private void Activate()
{ 
Manipulate[] array = this.actions;
for (int i = 0; i < array.Length; i++)
{ 
array[i].Activate();
}
UnityEngine.Object.Destroy(base.gameObject);
array
i
}
}
