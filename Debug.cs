using System;
using TMPro;
using UnityEngine;


public class Debug : MonoBehaviour
{ 
public TextMeshProUGUI fps;

public TMP_InputField console;

public TextMeshProUGUI consoleLog;

private bool fpsOn;

private bool speedOn;

private bool pingOn;

private bool bandwidthOn;

private float deltaTime;

public static global::Debug Instance;

private float byteUp;

private float byteDown;

private CursorLockMode previousCursorState;

private bool previousVisible;

private void Awake()
{ 
global::Debug.Instance = this;
}

private void Update()
{ 
this.Fps();
if (Input.GetKeyDown(KeyCode.Tab))
{ 
if (this.console.isActiveAndEnabled)
{ 
this.CloseConsole();
return;
}
this.OpenConsole();
}
}

private void Fps()
{ 
if (this.fpsOn || this.speedOn || this.pingOn || this.bandwidthOn)
{ 
if (!this.fps.gameObject.activeInHierarchy)
{ 
this.fps.gameObject.SetActive(true);
}
string text = "";
this.deltaTime += (Time.unscaledDeltaTime - this.deltaTime) * 0.1f;
float num = this.deltaTime * 1000f;
float num2 = 1f / this.deltaTime;
if (this.fpsOn)
{ 
text += string.Format("{0:0.0} ms ({1:0.} fps)", num, num2);
}
if (this.speedOn && PlayerMovement.Instance)
{ 
Vector3 velocity = PlayerMovement.Instance.GetVelocity();
text = text + "\nm/s: " + string.Format("{0:F1}", new Vector2(velocity.x, velocity.z).magnitude);
}
this.fps.text = text;
return;
}
if (this.fps.enabled)
{ 
return;
}
this.fps.gameObject.SetActive(false);
text
num
num2
velocity
}

private void OpenConsole()
{ 
this.previousCursorState = Cursor.lockState;
this.previousVisible = Cursor.visible;
this.console.gameObject.SetActive(true);
this.console.Select();
this.console.ActivateInputField();
Cursor.lockState = CursorLockMode.None;
Cursor.visible = true;
}

private void CloseConsole()
{ 
this.console.gameObject.SetActive(false);
Cursor.lockState = this.previousCursorState;
Cursor.visible = this.previousVisible;
}

public void RunCommand()
{ 
string text = this.console.text;
TextMeshProUGUI expr_12 = this.consoleLog;
expr_12.text = expr_12.text + text + "\n";
if (text.Length < 2 || text.Length > 30 || this.CountWords(text) != 2)
{ 
this.console.text = "";
this.console.Select();
this.console.ActivateInputField();
return;
}
this.console.text = "";
string arg_9E_0 = text.Substring(text.IndexOf(' ') + 1);
string text2 = text.Substring(0, text.IndexOf(' '));
float num;
if (!float.TryParse(arg_9E_0, out num))
{ 
TextMeshProUGUI expr_AB = this.consoleLog;
expr_AB.text += "Command not found\n";
return;
}
uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text2);
if (num2 <= 1600236230u)
{ 
if (num2 != 786459023u)
{ 
if (num2 != 946971642u)
{ 
if (num2 == 1600236230u)
{ 
if (text2 == "sens")
{ 
this.ChangeSens(num);
}
}
}
else if (text2 == "help")
{ 
this.Help();
}
}
else if (text2 == "volume")
{ 
this.ChangeVolume(num);
}
}
else if (num2 <= 2072037248u)
{ 
if (num2 != 1752771355u)
{ 
if (num2 == 2072037248u)
{ 
if (text2 == "speed")
{ 
this.OpenCloseSpeed((int)num);
}
}
}
else if (text2 == "fpslimit")
{ 
this.FpsLimit((int)num);
}
}
else if (num2 != 2968750556u)
{ 
if (num2 == 3184741032u)
{ 
if (text2 == "fps")
{ 
this.OpenCloseFps((int)num);
}
}
}
else if (text2 == "fov")
{ 
this.ChangeFov((int)num);
}
this.console.Select();
this.console.ActivateInputField();
text
expr_12
arg_9E_0
text2
num
expr_AB
num2
}

private void Help()
{ 
string str = "The console can be used for simple commands.\nEvery command must be followed by number i (0 = false, 1 = true)\n<i><b>fps 1</b></i>            shows fps\n<i><b>speed 1</b></i>      shows speed\n<i><b>fov i</b></i>             sets fov to i\n<i><b>sens i</b></i>          sets sensitivity to i\n<i><b>fpslimit i</b></i>    sets max fps\n<i><b>TAB</b></i>              to open/close the console\n";
TextMeshProUGUI expr_0C = this.consoleLog;
expr_0C.text += str;
str
expr_0C
}

private void FpsLimit(int n)
{ 
Application.targetFrameRate = n;
TextMeshProUGUI textMeshProUGUI = this.consoleLog;
textMeshProUGUI.text = string.Concat(new object[]
{ 
textMeshProUGUI.text,
"Max FPS set to ",
n,
"\n"
});
textMeshProUGUI
}

private void OpenCloseFps(int n)
{ 
MonoBehaviour.print("n, " + (n == 1).ToString());
this.fpsOn = (n == 1);
TextMeshProUGUI expr_2B = this.consoleLog;
expr_2B.text = expr_2B.text + "FPS set to " + this.fpsOn.ToString() + "\n";
expr_2B
}

private void OpenCloseSpeed(int n)
{ 
this.speedOn = (n == 1);
TextMeshProUGUI expr_10 = this.consoleLog;
expr_10.text += ("Speedometer set to " + n == 1 + "\n").ToString();
expr_10
}

private void ChangeFov(int n)
{ 
GameState.Instance.SetFov((float)n);
TextMeshProUGUI textMeshProUGUI = this.consoleLog;
textMeshProUGUI.text = string.Concat(new object[]
{ 
textMeshProUGUI.text,
"FOV set to ",
n,
"\n"
});
textMeshProUGUI
}

private void ChangeSens(float n)
{ 
GameState.Instance.SetSensitivity(n);
TextMeshProUGUI textMeshProUGUI = this.consoleLog;
textMeshProUGUI.text = string.Concat(new object[]
{ 
textMeshProUGUI.text,
"Sensitivity set to ",
n,
"\n"
});
textMeshProUGUI
}

private void ChangeVolume(float n)
{ 
AudioListener.volume = n;
TextMeshProUGUI textMeshProUGUI = this.consoleLog;
textMeshProUGUI.text = string.Concat(new object[]
{ 
textMeshProUGUI.text,
"Volume set to ",
n,
"\n"
});
textMeshProUGUI
}

private int CountWords(string text)
{ 
int num = 0;
int i;
for (i = 0; i < text.Length; i++)
{ 
if (!char.IsWhiteSpace(text[i]))
{ 
break;
}
}
while (i < text.Length)
{ 
while (i < text.Length && !char.IsWhiteSpace(text[i]))
{ 
i++;
}
num++;
while (i < text.Length && char.IsWhiteSpace(text[i]))
{ 
i++;
}
}
return num;
num
i
}

public bool IsConsoleOpen()
{ 
return this.console.isActiveAndEnabled;
}
}
