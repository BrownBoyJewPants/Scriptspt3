using System;
using UnityEngine;


namespace DitzelGames.FastIK
{ 
public class FastIKFabric : MonoBehaviour
{ 
public int ChainLength = 2;

public Transform Target;

public Transform Pole;

[Header("Solver Parameters")]
public int Iterations = 10;

public float Delta = 0.001f;

[Range(0f, 1f)]
public float SnapBackStrength = 1f;

protected float[] BonesLength;

public float CompleteLength;

protected Transform[] Bones;

protected Vector3[] Positions;

protected Vector3[] StartDirectionSucc;

protected Quaternion[] StartRotationBone;

protected Quaternion StartRotationTarget;

protected Transform Root;

public Vector3 offset;

private void Awake()
{ 
this.Init();
}

private void Init()
{ 
this.Bones = new Transform[this.ChainLength + 1];
this.Positions = new Vector3[this.ChainLength + 1];
this.BonesLength = new float[this.ChainLength];
this.StartDirectionSucc = new Vector3[this.ChainLength + 1];
this.StartRotationBone = new Quaternion[this.ChainLength + 1];
this.Root = base.transform;
for (int i = 0; i <= this.ChainLength; i++)
{ 
if (this.Root == null)
{ 
throw new UnityException("The chain value is longer than the ancestor chain!");
}
this.Root = this.Root.parent;
}
if (this.Target == null)
{ 
this.Target = new GameObject(base.gameObject.name + " Target").transform;
this.SetPositionRootSpace(this.Target, this.GetPositionRootSpace(base.transform));
}
this.StartRotationTarget = this.GetRotationRootSpace(this.Target);
Transform transform = base.transform;
this.CompleteLength = 0f;
for (int j = this.Bones.Length - 1; j >= 0; j--)
{ 
this.Bones[j] = transform;
this.StartRotationBone[j] = this.GetRotationRootSpace(transform);
if (j == this.Bones.Length - 1)
{ 
this.StartDirectionSucc[j] = this.GetPositionRootSpace(this.Target) - this.GetPositionRootSpace(transform);
}
else
{ 
this.StartDirectionSucc[j] = this.GetPositionRootSpace(this.Bones[j + 1]) - this.GetPositionRootSpace(transform);
this.BonesLength[j] = this.StartDirectionSucc[j].magnitude;
this.CompleteLength += this.BonesLength[j];
}
transform = transform.parent;
}
i
transform
j
}

private void LateUpdate()
{ 
this.ResolveIK();
this.Bones[this.Bones.Length - 1].LookAt(this.Target);
this.Bones[this.Bones.Length - 1].Rotate(this.offset);
}

private void ResolveIK()
{ 
if (this.Target == null)
{ 
return;
}
if (this.BonesLength.Length != this.ChainLength)
{ 
this.Init();
}
for (int i = 0; i < this.Bones.Length; i++)
{ 
this.Positions[i] = this.GetPositionRootSpace(this.Bones[i]);
}
Vector3 positionRootSpace = this.GetPositionRootSpace(this.Target);
Quaternion rotationRootSpace = this.GetRotationRootSpace(this.Target);
if ((positionRootSpace - this.GetPositionRootSpace(this.Bones[0])).sqrMagnitude >= this.CompleteLength * this.CompleteLength)
{ 
Vector3 normalized = (positionRootSpace - this.Positions[0]).normalized;
for (int j = 1; j < this.Positions.Length; j++)
{ 
this.Positions[j] = this.Positions[j - 1] + normalized * this.BonesLength[j - 1];
}
}
else
{ 
for (int k = 0; k < this.Positions.Length - 1; k++)
{ 
this.Positions[k + 1] = Vector3.Lerp(this.Positions[k + 1], this.Positions[k] + this.StartDirectionSucc[k], this.SnapBackStrength);
}
for (int l = 0; l < this.Iterations; l++)
{ 
for (int m = this.Positions.Length - 1; m > 0; m--)
{ 
if (m == this.Positions.Length - 1)
{ 
this.Positions[m] = positionRootSpace;
}
else
{ 
this.Positions[m] = this.Positions[m + 1] + (this.Positions[m] - this.Positions[m + 1]).normalized * this.BonesLength[m];
}
}
for (int n = 1; n < this.Positions.Length; n++)
{ 
this.Positions[n] = this.Positions[n - 1] + (this.Positions[n] - this.Positions[n - 1]).normalized * this.BonesLength[n - 1];
}
if ((this.Positions[this.Positions.Length - 1] - positionRootSpace).sqrMagnitude < this.Delta * this.Delta)
{ 
break;
}
}
}
if (this.Pole != null)
{ 
Vector3 positionRootSpace2 = this.GetPositionRootSpace(this.Pole);
for (int num = 1; num < this.Positions.Length - 1; num++)
{ 
Plane plane = new Plane(this.Positions[num + 1] - this.Positions[num - 1], this.Positions[num - 1]);
Vector3 a = plane.ClosestPointOnPlane(positionRootSpace2);
float angle = Vector3.SignedAngle(plane.ClosestPointOnPlane(this.Positions[num]) - this.Positions[num - 1], a - this.Positions[num - 1], plane.normal);
this.Positions[num] = Quaternion.AngleAxis(angle, plane.normal) * (this.Positions[num] - this.Positions[num - 1]) + this.Positions[num - 1];
}
}
for (int num2 = 0; num2 < this.Positions.Length; num2++)
{ 
if (num2 == this.Positions.Length - 1)
{ 
this.SetRotationRootSpace(this.Bones[num2], Quaternion.Inverse(rotationRootSpace) * this.StartRotationTarget * Quaternion.Inverse(this.StartRotationBone[num2]));
}
else
{ 
this.SetRotationRootSpace(this.Bones[num2], Quaternion.FromToRotation(this.StartDirectionSucc[num2], this.Positions[num2 + 1] - this.Positions[num2]) * Quaternion.Inverse(this.StartRotationBone[num2]));
}
this.SetPositionRootSpace(this.Bones[num2], this.Positions[num2]);
}
i
positionRootSpace
rotationRootSpace
normalized
j
k
l
m
n
positionRootSpace2
num
plane
a
angle
num2
}

private Vector3 GetPositionRootSpace(Transform current)
{ 
if (this.Root == null)
{ 
return current.position;
}
return Quaternion.Inverse(this.Root.rotation) * (current.position - this.Root.position);
}

private void SetPositionRootSpace(Transform current, Vector3 position)
{ 
if (this.Root == null)
{ 
current.position = position;
return;
}
current.position = this.Root.rotation * position + this.Root.position;
}

private Quaternion GetRotationRootSpace(Transform current)
{ 
if (this.Root == null)
{ 
return current.rotation;
}
return Quaternion.Inverse(current.rotation) * this.Root.rotation;
}

private void SetRotationRootSpace(Transform current, Quaternion rotation)
{ 
if (this.Root == null)
{ 
current.rotation = rotation;
return;
}
current.rotation = this.Root.rotation * rotation;
}

private void OnDrawGizmos()
{ 
}
}
}
