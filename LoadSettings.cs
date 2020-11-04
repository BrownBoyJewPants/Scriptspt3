using System;
using UnityEngine;


public class LoadSettings : MonoBehaviour
{ 
private void Start()
{ 
if (GameState.Instance)
{ 
GameState.Instance.ApplySettings();
base.transform.GetChild(0).gameObject.SetActive(false);
}
}
}
