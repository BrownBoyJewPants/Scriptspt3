using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelect : MonoBehaviour
{ 
public TextMeshProUGUI[] times;

private void OnEnable()
{ 
this.UpdateTimes();
}

private void UpdateTimes()
{ 
for (int i = 0; i < this.times.Length; i++)
{ 
this.times[i].text = Timer.GetFormattedTime(SaveManager.Instance.state.times[i]);
}
i
}

public void LoadLevel(int i)
{ 
SceneManager.LoadScene(i + 1);
}
}
