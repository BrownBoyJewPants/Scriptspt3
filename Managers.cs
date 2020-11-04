using System;
using UnityEngine;


public class Managers : MonoBehaviour
{ 
public static Managers Instance;

private void Awake()
{ 
if (Managers.Instance != null)
{ 
MonoBehaviour.print("managers: " + Managers.Instance);
UnityEngine.Object.Destroy(base.gameObject);
return;
}
Managers.Instance = this;
UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
}
}
