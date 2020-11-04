using System;
using UnityEngine;
using UnityEngine.UI;


public class ProgressiveBar : MonoBehaviour
{ 
public Image currentBar;

public int barSpeed = 10;

public float popSize = 1.5f;

private float popScale;

public bool popOut;

private Vector3 defaultScale;

private Vector3 desiredScale;

private Vector3 desiredPopScale;

private void Awake()
{ 
this.defaultScale = this.currentBar.transform.localScale;
this.desiredScale = this.defaultScale;
}

private void Update()
{ 
this.currentBar.transform.localScale = Vector3.Lerp(this.currentBar.transform.localScale, this.desiredScale, Time.unscaledDeltaTime * (float)this.barSpeed);
if (this.popOut)
{ 
base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.one * this.popScale, Time.unscaledDeltaTime * (float)this.barSpeed);
this.popScale = Mathf.Lerp(this.popScale, 1f, Time.unscaledDeltaTime * (float)this.barSpeed);
}
}

public void UpdateBar(float current, float total)
{ 
float num = current / total;
this.desiredScale = new Vector3(num * this.defaultScale.x, this.defaultScale.y, this.defaultScale.z);
if (this.popOut)
{ 
this.popScale = this.popSize;
}
num
}
}
