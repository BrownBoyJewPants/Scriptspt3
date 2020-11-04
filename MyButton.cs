using System;
using UnityEngine;


public class MyButton : MonoBehaviour
{ 
public Manipulate[] manipulations;

public GameObject topButton;

private Vector3 topButtonPos;

private bool done;

private void Awake()
{ 
this.topButtonPos = this.topButton.transform.position;
}

public void ActivateButton()
{ 
if (this.done)
{ 
return;
}
Manipulate[] array = this.manipulations;
for (int i = 0; i < array.Length; i++)
{ 
array[i].Activate();
}
this.done = true;
AudioSource componentInChildren = base.GetComponentInChildren<AudioSource>();
if (componentInChildren)
{ 
componentInChildren.Play();
}
array
i
componentInChildren
}

private void Update()
{ 
if (!this.done)
{ 
return;
}
this.topButton.transform.localPosition = Vector3.Lerp(this.topButton.transform.localPosition, Vector3.down * 0.2f, Time.deltaTime * 5f);
}
}
