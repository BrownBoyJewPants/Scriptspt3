using DitzelGames.FastIK;
using System;
using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{ 
private RigidEnemy ikController;

private PlayerController playerController;

public GameObject bloodFx;

[Header("Attributes")]
public new string name;

public int hp;

public int maxHp;

public ProgressiveBar healthBar;

public TextMeshProUGUI nameField;

private Rigidbody rb;

public DamagePlayer damagePlayer;

public GameObject rightHand;

public GameObject rightShoulder;

public FastIKFabric ikHand;

public GameObject sword;

public Transform currentSword;

private bool killed;

public GameObject killFx;

private void Awake()
{ 
this.ikController = base.GetComponent<RigidEnemy>();
this.playerController = base.GetComponent<PlayerController>();
this.rb = this.ikController.root.GetComponent<Rigidbody>();
}

private void Start()
{ 
this.maxHp = this.hp;
this.nameField.text = this.name;
if (this.ikHand)
{ 
this.ikHand.Target = PlayerMovement.Instance.transform;
}
}

public void Damage(int damage, Vector3 damagePos)
{ 
this.hp -= damage;
if (this.hp <= 0)
{ 
this.hp = 0;
this.Kill();
}
this.healthBar.UpdateBar((float)this.hp, (float)this.maxHp);
ParticleSystem expr_53 = UnityEngine.Object.Instantiate<GameObject>(this.bloodFx, damagePos, Quaternion.identity).GetComponent<ParticleSystem>();
ParticleSystem.Burst burst = expr_53.emission.GetBurst(0);
burst.count = (float)Mathf.Clamp(damage, 0, 50);
expr_53.emission.SetBurst(0, burst);
expr_53
burst
}

private void Kill()
{ 
if (this.killed)
{ 
return;
}
this.killed = true;
this.ikController.UpdateState(RigidEnemy.EnemyState.dead);
UnityEngine.Object.Destroy(this.damagePlayer);
Rigidbody component = this.ikController.torso.GetComponent<Rigidbody>();
if (!this.ikHand)
{ 
GameObject expr_66 = UnityEngine.Object.Instantiate<GameObject>(this.sword, this.currentSword.position, this.currentSword.rotation);
expr_66.GetComponent<Rigidbody>().AddForce(Vector3.up * 4000f);
expr_66.GetComponent<LooseSword>().RemoveCollision();
UnityEngine.Object.Destroy(this.currentSword.gameObject);
}
if (this.ikHand)
{ 
UnityEngine.Object.Destroy(this.ikHand);
this.rightShoulder.AddComponent<HingeJoint>().connectedBody = component;
this.rightHand.AddComponent<HingeJoint>().connectedBody = this.rightShoulder.GetComponent<Rigidbody>();
}
UnityEngine.Object.Instantiate<GameObject>(this.killFx);
SubText.Instance.PutText();
component
expr_66
}

public Rigidbody GetRb()
{ 
return this.rb;
}

public Transform GetRoot()
{ 
return this.ikController.root;
}

public Transform GetTorso()
{ 
return this.ikController.torso;
}

public Transform GetTarget()
{ 
return this.playerController.target;
}
}
