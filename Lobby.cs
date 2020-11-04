using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Lobby : MonoBehaviour
{ 
public RandomSfx sfx;

public void Play()
{ 
SceneManager.LoadScene("Level0");
}

public void Exit()
{ 
Application.Quit(0);
}

public void PlayButton()
{ 
this.sfx.Randomize();
}

public void LoadPage(string page)
{ 
Application.OpenURL(page);
}
}
