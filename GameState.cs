using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class GameState : MonoBehaviour
{ 
public GameObject ppVolume;

public PostProcessProfile pp;

private AmbientOcclusion ambientOcclusion;

private Bloom bloom;

private LensDistortion lens;

public bool graphics = true;

public bool muted;

public bool blur = true;

public bool shake = true;

public bool slowmo = true;

private float sensitivity = 1f;

private float volume;

private float music;

public float fov = 1f;

public float cameraShake = 1f;

public static GameState Instance
{ 
get;
private set;
}

private void Awake()
{ 
GameState.Instance = this;
Application.targetFrameRate = 200;
this.ambientOcclusion = this.pp.GetSetting<AmbientOcclusion>();
this.bloom = this.pp.GetSetting<Bloom>();
this.lens = this.pp.GetSetting<LensDistortion>();
}

private void Start()
{ 
this.graphics = SaveManager.Instance.state.graphics;
this.shake = SaveManager.Instance.state.cameraShake;
this.blur = SaveManager.Instance.state.motionBlur;
this.slowmo = SaveManager.Instance.state.slowmo;
this.muted = SaveManager.Instance.state.muted;
this.sensitivity = SaveManager.Instance.state.sensitivity;
this.music = SaveManager.Instance.state.music;
this.volume = SaveManager.Instance.state.volume;
this.fov = SaveManager.Instance.state.fov;
this.UpdateSettings();
}

public void SetGraphics(bool b)
{ 
this.graphics = b;
this.ambientOcclusion.enabled.value = b;
this.lens.enabled.value = b;
this.bloom.enabled.value = b;
if (!this.graphics)
{ 
QualitySettings.SetQualityLevel(0);
}
if (this.graphics)
{ 
QualitySettings.SetQualityLevel(5);
}
SaveManager.Instance.state.graphics = b;
SaveManager.Instance.Save();
}

public void SetBlur(bool b)
{ 
}

public void SetShake(bool b)
{ 
this.shake = b;
if (b)
{ 
this.cameraShake = 1f;
}
else
{ 
this.cameraShake = 0f;
}
SaveManager.Instance.state.cameraShake = b;
SaveManager.Instance.Save();
}

public void SetSlowmo(bool b)
{ 
this.slowmo = b;
SaveManager.Instance.state.slowmo = b;
SaveManager.Instance.Save();
}

public void SetSensitivity(float s)
{ 
float num = Mathf.Clamp(s, 0f, 5f);
this.sensitivity = num;
if (PlayerInput.Instance)
{ 
PlayerInput.Instance.UpdateSensitivity(this.sensitivity);
}
SaveManager.Instance.state.sensitivity = num;
SaveManager.Instance.Save();
num
}

public void SetMusic(float s)
{ 
float f = Mathf.Clamp(s, 0f, 1f);
this.music = f;
MusicController.Instance.UpdateMusic(f);
SaveManager.Instance.state.music = f;
SaveManager.Instance.Save();
f
}

public void SetVolume(float s)
{ 
float num = Mathf.Clamp(s, 0f, 1f);
this.volume = num;
AudioListener.volume = num;
SaveManager.Instance.state.volume = num;
SaveManager.Instance.Save();
num
}

public void ApplySettings()
{ 
AudioListener.volume = this.volume;
if (PlayerInput.Instance)
{ 
PlayerInput.Instance.UpdateSensitivity(this.sensitivity);
}
if (MoveCamera.Instance)
{ 
MoveCamera.Instance.UpdateFov(this.fov);
}
}

public void SetFov(float f)
{ 
float num = Mathf.Clamp(f, 50f, 150f);
this.fov = num;
if (MoveCamera.Instance)
{ 
MoveCamera.Instance.UpdateFov(this.fov);
}
SaveManager.Instance.state.fov = num;
SaveManager.Instance.Save();
num
}

private void UpdateSettings()
{ 
this.SetGraphics(this.graphics);
this.SetBlur(this.blur);
this.SetSensitivity(this.sensitivity);
this.SetMusic(this.music);
this.SetVolume(this.volume);
this.SetFov(this.fov);
this.SetShake(this.shake);
this.SetSlowmo(this.slowmo);
}

public bool GetGraphics()
{ 
return this.graphics;
}

public float GetSensitivity()
{ 
return this.sensitivity;
}

public float GetVolume()
{ 
return this.volume;
}

public float GetMusic()
{ 
return this.music;
}

public float GetFov()
{ 
return this.fov;
}

public bool GetMuted()
{ 
return this.muted;
}
}
