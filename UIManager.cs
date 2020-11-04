using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{ 
public GameObject pauseMenu;

public static UIManager Instance;

private void Awake()
{ 
UIManager.Instance = this;
}

private void Update()
{ 
if (Input.GetButtonDown("Cancel"))
{ 
this.TogglePause();
}
}

public void TogglePause()
{ 
if (GameManager.Instance.playerDead || !GameManager.Instance.playing)
{ 
return;
}
if (this.pauseMenu.activeInHierarchy)
{ 
this.HidePause();
}
else
{ 
this.ShowPause();
}
this.pauseMenu.SetActive(!this.pauseMenu.activeInHierarchy);
GameManager.Instance.paused = this.pauseMenu.activeInHierarchy;
}

public void HidePause()
{ 
Time.timeScale = 1f;
Cursor.visible = false;
Cursor.lockState = CursorLockMode.Locked;
}

private void ShowPause()
{ 
Time.timeScale = 0f;
Cursor.visible = true;
Cursor.lockState = CursorLockMode.None;
}

public void MainMenu()
{ 
SceneManager.LoadScene("Menu");
}
}
