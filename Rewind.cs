using System;
using UnityEngine;


public class Rewind : MonoBehaviour
{ 
private class RewindObject
{ 
public Vector3 position;

public Vector3 velocity;

public RewindObject(Vector3 pos, Vector3 vel)
{ 
this.position = pos;
this.velocity = vel;
}
}

private int tick;

private int seconds = 2;

private int bufferSize;

private Rewind.RewindObject[] playerHistory;

private Rigidbody rb;

public LineRenderer lr;

private bool reversing;

private Vector3 reversePos;

private float t;

private void Awake()
{ 
this.bufferSize = (int)(1f / Time.fixedDeltaTime) * this.seconds;
this.lr.positionCount = this.bufferSize;
this.rb = base.GetComponent<Rigidbody>();
this.playerHistory = new Rewind.RewindObject[this.bufferSize];
Rewind.RewindObject rewindObject = new Rewind.RewindObject(base.transform.position, Vector3.zero);
for (int i = 0; i < this.bufferSize; i++)
{ 
this.playerHistory[i] = rewindObject;
}
this.tick = this.bufferSize;
rewindObject
i
}

private void FixedUpdate()
{ 
Rewind.RewindObject rewindObject = new Rewind.RewindObject(base.transform.position, this.rb.velocity);
this.playerHistory[this.tick % this.bufferSize] = rewindObject;
this.tick++;
this.UpdateLineRenderer();
rewindObject
}

private void UpdateLineRenderer()
{ 
for (int i = 0; i < this.bufferSize; i++)
{ 
this.lr.SetPosition(i, this.playerHistory[(this.tick - this.bufferSize + i) % this.bufferSize].position);
}
i
}

private void Update()
{ 
if (Input.GetKeyDown(KeyCode.R))
{ 
this.reversePos = this.playerHistory[(this.tick - this.bufferSize) % this.bufferSize].position;
this.reversing = true;
this.rb.isKinematic = true;
this.t = 0f;
}
if (this.reversing)
{ 
this.t += Time.deltaTime;
base.transform.position = Vector3.Lerp(base.transform.position, this.reversePos, this.t);
}
}
}
