using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PlayerStatus : MonoBehaviour
{ 
private int hp;

public int maxHp = 100;

public PostProcessProfile pp;

private Vignette vignette;

private ColorGrading colorGrading;

public RandomSfx sfx;

public static PlayerStatus Instance;

private bool healing;

private float speed = 13f;

private float defaultVignette = 0.3f;

private float defaultContrast = 10f;

private void Awake()
{ 
PlayerStatus.Instance = this;
this.vignette = this.pp.GetSetting<Vignette>();
this.colorGrading = this.pp.GetSetting<ColorGrading>();
this.hp = this.maxHp;
}

public void Damage(int damage)
{ 
if (this.hp <= 0 || !GameManager.Instance.playing)
{ 
return;
}
this.hp -= damage;
this.vignette.intensity.value *= 1.5f;
this.colorGrading.colorFilter.value = Color.red;
this.healing = false;
base.CancelInvoke("StartHealing");
base.Invoke("StartHealing", 4f);
this.sfx.Randomize();
if (this.hp <= 0)
{ 
this.Kill();
}
}

private void StartHealing()
{ 
this.healing = true;
}

public void ResetStatus()
{ 
this.hp = this.maxHp;
}

private void Update()
{ 
float num = 1f - (float)this.hp / (float)this.maxHp;
num = Mathf.Clamp(num, 0f, 1f);
this.vignette.intensity.value = Mathf.Lerp(this.vignette.intensity.value, this.defaultVignette + num * 0.25f, Time.deltaTime * this.speed);
this.colorGrading.contrast.value = Mathf.Lerp(this.colorGrading.contrast.value, this.defaultContrast + num * 40f, Time.deltaTime * this.speed);
this.colorGrading.colorFilter.value = Color.Lerp(this.colorGrading.colorFilter.value, new Color(1f, 1f - num * 0.3f, 1f - num * 0.3f), Time.deltaTime * this.speed * 2f);
if (this.healing && this.hp < this.maxHp && this.hp > 0)
{ 
this.hp++;
}
num
}

private void Kill()
{ 
GameManager.Instance.PlayerDied();
PlayerMovement.Instance.GetRb().isKinematic = true;
PlayerMovement.Instance.GetRb().velocity = Vector3.zero;
PlayerMovement.Instance.SetInput(Vector2.zero, false, false);
if (Sword.Instance.pickedUp)
{ 
Sword.Instance.RemoveSword();
}
}

private void OnDestroy()
{ 
this.vignette.intensity.value = this.defaultVignette;
this.colorGrading.contrast.value = this.defaultContrast;
this.colorGrading.colorFilter.value = Color.white;
}
}
