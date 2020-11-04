using EZCameraShake;
using System;
using UnityEngine;


public class Sword : MonoBehaviour
{ 
private Animator animator;

public bool throwSword;

[HideInInspector]
public bool pickedUp;

public Transform mainSword;

public GameObject sword;

public RandomSfx audio;

public PlayerSword playerSword;

public static Sword Instance;

[HideInInspector]
public bool blocking;

private bool readyToThrow = true;

private void Awake()
{ 
Sword.Instance = this;
this.animator = base.GetComponent<Animator>();
this.pickedUp = false;
}

public void Update()
{ 
if (!GameManager.Instance.playing || GameManager.Instance.playerDead)
{ 
return;
}
this.blocking = Input.GetButton("Fire2");
this.animator.SetBool("Blocking", this.blocking);
this.CheckIfThrowSword();
if (!this.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
{ 
return;
}
if (Input.GetButtonDown("Fire1"))
{ 
string stateName;
if (UnityEngine.Random.Range(0f, 1f) < 0.5f)
{ 
stateName = "Swing1";
}
else
{ 
stateName = "Swing2";
}
this.animator.Play(stateName);
CameraShaker.Instance.ShakeOnce(8f, 4f, 0.4f, 0.5f);
this.audio.Randomize();
this.playerSword.ResetList();
}
if (Input.GetKeyDown(KeyCode.F))
{ 
this.animator.Play("Throw");
}
stateName
}

public void Pickup()
{ 
this.pickedUp = true;
if (this.animator)
{ 
this.animator.Play("Pickup");
}
CameraShaker.Instance.ShakeOnce(5f, 2f, 0.25f, 0.25f);
}

public bool IsBlocking()
{ 
return this.blocking && this.pickedUp;
}

public void RemoveSword()
{ 
this.animator.Play("RemoveSword");
}

private void CheckIfThrowSword()
{ 
if (this.throwSword && this.pickedUp && this.readyToThrow)
{ 
this.readyToThrow = false;
base.Invoke("GetReadyToThrow", 0.2f);
this.throwSword = false;
this.pickedUp = false;
GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.sword, this.mainSword.position, this.mainSword.rotation);
Rigidbody expr_6E = gameObject.GetComponent<Rigidbody>();
expr_6E.AddForce(PlayerMovement.Instance.playerCam.forward * 16000f);
expr_6E.maxAngularVelocity = 300f;
expr_6E.AddTorque(-gameObject.transform.up * 4050f);
gameObject.GetComponent<LooseSword>().player = true;
CameraShaker.Instance.ShakeOnce(6f, 6f, 0.2f, 0.45f);
}
gameObject
expr_6E
}

private void GetReadyToThrow()
{ 
this.readyToThrow = true;
}
}
