using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DeathScreen : MonoBehaviour
{ 
public void RestartLevel()
{ 
SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

public void MainMenu()
{ 
SceneManager.LoadScene("Menu");
Time.timeScale = 1f;
}
}
