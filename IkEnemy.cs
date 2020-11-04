using DitzelGames.FastIK;
using System;
using UnityEngine;


public class IkEnemy : MonoBehaviour
{ 
public LayerMask whatIsGround;

public float heightAboveGround;

public FastIKFabric[] legs;

private Transform[] legTargets;

private Vector3[] targetPositions;

private Vector3[] currentPositions;

public Vector3 legTargetOffset;

public Transform root;

private float thresholdDistance;

private float[] legProgress;

private RigidEnemy rigidEnemy;

public float legSpeed = 10f;

private Vector3 currentVelocity;

public float upAmount = 2f;

private void Start()
{ 
this.rigidEnemy = base.GetComponent<RigidEnemy>();
this.legTargets = new Transform[this.legs.Length];
this.targetPositions = new Vector3[this.legs.Length];
this.currentPositions = new Vector3[this.legs.Length];
this.legProgress = new float[this.legs.Length];
this.InitLegTargets();
if (this.heightAboveGround == 0f)
{ 
this.heightAboveGround = this.legs[0].CompleteLength;
this.thresholdDistance = this.heightAboveGround;
}
this.UpdateLegTargets();
this.UpdateCurrentLegPosition(0);
this.UpdateCurrentLegPosition(1);
base.InvokeRepeating("SlowUpdate", 1f, 1f);
}

private void Update()
{ 
this.currentVelocity = this.rigidEnemy.GetVelocity() * this.thresholdDistance;
this.UpdateLegTargets();
this.UpdateCurrentLegPositions(this.thresholdDistance);
this.LerpLegs();
}

private void SlowUpdate()
{ 
this.UpdateCurrentLegPositions(this.thresholdDistance * 0.2f);
}

private void InitLegTargets()
{ 
for (int i = 0; i < this.legs.Length; i++)
{ 
int j = this.legs[i].ChainLength;
Transform transform = this.legs[i].transform;
while (j > 0)
{ 
transform = transform.parent;
j--;
}
this.legTargets[i] = transform;
}
i
j
transform
}

private void UpdateLegTargets()
{ 
for (int i = 0; i < this.legTargets.Length; i++)
{ 
Vector3 a = this.legTargets[i].position - this.root.position;
RaycastHit raycastHit;
if (Physics.Raycast(this.legTargets[i].position + this.legTargetOffset.x * a + this.currentVelocity + Vector3.up, Vector3.down, out raycastHit, 50f, this.whatIsGround))
{ 
this.targetPositions[i] = raycastHit.point;
}
}
i
a
raycastHit
}

private void UpdateCurrentLegPositions(float threshold)
{ 
for (int i = 0; i < this.legs.Length; i++)
{ 
if (!this.OppositeLegGrounded(i) && this.legProgress[i] < 0.01f && this.CheckDistanceFromTargetPoint(i) < 4f)
{ 
return;
}
if (this.CheckDistanceFromTargetPoint(i) > threshold)
{ 
this.UpdateCurrentLegPosition(i);
}
}
i
}

private bool OppositeLegGrounded(int leg)
{ 
int num = (leg + 1) % this.legs.Length;
return this.legProgress[num] < 0.01f;
num
}

private float CheckDistanceFromTargetPoint(int leg)
{ 
return Vector3.Distance(this.currentPositions[leg], this.targetPositions[leg]);
}

private void UpdateCurrentLegPosition(int leg)
{ 
this.currentPositions[leg] = this.targetPositions[leg];
this.legProgress[leg] = 1f;
}

private void LerpLegs()
{ 
for (int i = 0; i < this.legs.Length; i++)
{ 
Transform arg_58_0 = this.legs[i].Target;
this.legProgress[i] = Mathf.Lerp(this.legProgress[i], 0f, Time.deltaTime * this.legSpeed);
Vector3 b = Vector3.up * this.upAmount * this.legProgress[i];
arg_58_0.position = Vector3.Lerp(arg_58_0.position, this.currentPositions[i] + b, Time.deltaTime * this.legSpeed);
}
i
arg_58_0
b
}

private void OnDrawGizmos()
{ 
}

public void CollectGarbage()
{ 
FastIKFabric[] array = this.legs;
for (int i = 0; i < array.Length; i++)
{ 
UnityEngine.Object.Destroy(array[i].Target.gameObject);
}
array
i
}

public void ForceCurrentPosition(int i)
{ 
if (this.legProgress != null)
{ 
float arg_10_0 = this.legProgress[i];
this.legProgress[i] = 1f;
this.legs[i].Target.position = this.legs[i].transform.position;
return;
}
arg_10_0
}
}
