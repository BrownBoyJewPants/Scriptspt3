using System;
using UnityEngine;


public class Hover : MonoBehaviour
{ 
private Vector3 desiredPos;

private float startY;

private void Awake()
{ 
this.desiredPos = base.transform.position;
this.startY = base.transform.position.y;
}

private void Update()
{ 
this.desiredPos.y = this.startY + Mathf.PingPong(Time.time, 1f) - 0.5f;
base.transform.position = Vector3.Lerp(base.transform.position, this.desiredPos, Time.deltaTime);
base.transform.Rotate(Vector3.up, 0.25f);
}
}
