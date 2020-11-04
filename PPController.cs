using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PPController : MonoBehaviour
{ 
public PostProcessProfile pp;

private ColorGrading colorGrading;

private ChromaticAberration chromaticAberration;

private LensDistortion lensDistortion;

private float desiredSaturation;

private float desiredChroma;

private float desiredDistortion;

private float desiredGrain;

private float speed = 6f;

public static PPController Instance;

private void Awake()
{ 
PPController.Instance = this;
this.colorGrading = this.pp.GetSetting<ColorGrading>();
this.chromaticAberration = this.pp.GetSetting<ChromaticAberration>();
this.lensDistortion = this.pp.GetSetting<LensDistortion>();
}

private void Update()
{ 
if (Mathf.Abs(this.colorGrading.saturation.value - this.desiredSaturation) < 0.1f)
{ 
return;
}
this.colorGrading.saturation.value = Mathf.Lerp(this.colorGrading.saturation.value, this.desiredSaturation, Time.deltaTime * this.speed);
this.chromaticAberration.intensity.value = Mathf.Lerp(this.chromaticAberration.intensity.value, this.desiredChroma, Time.deltaTime * this.speed);
this.lensDistortion.intensity.value = Mathf.Lerp(this.lensDistortion.intensity.value, this.desiredDistortion, Time.deltaTime * this.speed);
}

public void UpdateFx(float t)
{ 
float num = 1f - t;
this.desiredDistortion = -150f * num;
this.desiredSaturation = -100f * num;
this.desiredChroma = 1f * num;
this.desiredGrain = 1f * num;
num
}

public void StartRewind()
{ 
this.lensDistortion.enabled.value = true;
this.chromaticAberration.enabled.value = true;
}

public void StopRewind()
{ 
this.colorGrading.saturation.value = 0f;
this.lensDistortion.enabled.value = false;
this.chromaticAberration.enabled.value = false;
}

private void OnDestroy()
{ 
this.StopRewind();
}
}
