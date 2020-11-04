using System;
using UnityEngine;


public class Crosshair : MonoBehaviour
{ 
public enum CrosshairMode
{ 
Normal,
Button
}

public GameObject crosshair;

public GameObject button;

public static Crosshair Instance;

private void Awake()
{ 
Crosshair.Instance = this;
}

public void ChangeCrosshair(Crosshair.CrosshairMode mode)
{ 
this.HideAll();
if (mode == Crosshair.CrosshairMode.Normal)
{ 
this.crosshair.SetActive(true);
return;
}
if (mode != Crosshair.CrosshairMode.Button)
{ 
return;
}
this.button.SetActive(true);
}

private void HideAll()
{ 
Transform[] componentsInChildren = base.transform.GetComponentsInChildren<Transform>();
for (int i = 0; i < componentsInChildren.Length; i++)
{ 
Transform transform = componentsInChildren[i];
if (!(transform == base.transform))
{ 
transform.gameObject.SetActive(false);
}
}
componentsInChildren
i
transform
}
}
