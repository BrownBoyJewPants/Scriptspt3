using System;
using UnityEngine;


public class TutorialPowerup : MonoBehaviour
{ 
[TextArea]
public string text;

private void OnDestroy()
{ 
Tutorial.Instance.AddText(this.text);
}
}
