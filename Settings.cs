using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{ 
private float sensitivity;

private float volume;

private float music;

private float fov;

private bool shake;

private bool graphics;

public Slider sliderSens;

public Slider sliderVol;

public Slider sliderMusic;

public Slider sliderFov;

public TextMeshProUGUI sensText;

public TextMeshProUGUI fovText;

public Image shakeBtn;

public Image graphicsBtn;

private void Start()
{ 
this.sensitivity = SaveManager.Instance.state.sensitivity;
this.volume = SaveManager.Instance.state.volume;
this.music = SaveManager.Instance.state.music;
this.fov = SaveManager.Instance.state.fov;
this.shake = SaveManager.Instance.state.cameraShake;
this.graphics = SaveManager.Instance.state.graphics;
this.UpdateAllSliders();
this.UpdateAllButtons();
}

private void UpdateAllSliders()
{ 
this.sliderSens.value = this.sensitivity;
this.sliderFov.value = this.fov;
this.sliderMusic.value = this.music;
this.sliderVol.value = this.volume;
this.sensText.text = string.Concat(this.sensitivity);
this.fovText.text = string.Concat(this.fov);
}

private void UpdateAllButtons()
{ 
this.shakeBtn.enabled = this.shake;
this.graphicsBtn.enabled = this.graphics;
}

public void ToggleGraphics()
{ 
this.graphics = !this.graphics;
SaveManager.Instance.state.graphics = this.graphics;
SaveManager.Instance.Save();
GameState.Instance.SetGraphics(this.graphics);
this.UpdateAllButtons();
}

public void ToggleShake()
{ 
this.shake = !this.shake;
SaveManager.Instance.state.cameraShake = this.shake;
SaveManager.Instance.Save();
GameState.Instance.SetShake(this.shake);
this.UpdateAllButtons();
}

public void Sensitivity()
{ 
this.sensitivity = Mathf.Round(this.sliderSens.value * 100f) / 100f;
SaveManager.Instance.state.sensitivity = this.sensitivity;
SaveManager.Instance.Save();
GameState.Instance.SetSensitivity(this.sensitivity);
this.sensText.text = string.Concat(this.sensitivity);
}

public void Fov()
{ 
this.fov = this.sliderFov.value;
SaveManager.Instance.state.fov = this.fov;
SaveManager.Instance.Save();
GameState.Instance.SetFov(this.fov);
this.fovText.text = string.Concat(this.fov);
}

public void Volume()
{ 
this.volume = this.sliderVol.value;
SaveManager.Instance.state.volume = this.volume;
SaveManager.Instance.Save();
GameState.Instance.SetVolume(this.volume);
}

public void Music()
{ 
this.music = this.sliderMusic.value;
SaveManager.Instance.state.music = this.music;
SaveManager.Instance.Save();
GameState.Instance.SetMusic(this.music);
}
}
