using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class WinScreen : MonoBehaviour
{ 
public TextMeshProUGUI timer;

public Image image;

public static WinScreen Instance;

private void Awake()
{ 
WinScreen.Instance = this;
base.gameObject.SetActive(false);
}

private void OnEnable()
{ 
this.image.CrossFadeAlpha(0f, 0f, true);
this.image.CrossFadeAlpha(1f, 1f, true);
this.timer.text = Timer.GetFormattedTime(Timer.Instance.GetTimer());
}

public void NextLevel()
{ 
int num = SceneManager.GetActiveScene().buildIndex + 1;
AutoSplitterData.isLoading = 1;
AutoSplitterData.levelID = num;
int sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
MonoBehaviour.print(string.Concat(new object[]
{ 
"next: ",
num,
", scenes: ",
sceneCountInBuildSettings
}));
if (num >= sceneCountInBuildSettings)
{ 
SceneManager.LoadScene("Menu");
return;
}
SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
num
sceneCountInBuildSettings
}
}
