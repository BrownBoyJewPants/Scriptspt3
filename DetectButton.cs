using System;
using UnityEngine;


public class DetectButton : MonoBehaviour
{ 
public LayerMask whatIsButton;

private MyButton currentButton;

private void Update()
{ 
this.CheckInput();
Transform playerCam = PlayerMovement.Instance.playerCam;
RaycastHit raycastHit;
if (Physics.Raycast(playerCam.position, playerCam.forward, out raycastHit, 3.5f, this.whatIsButton))
{ 
if (!this.currentButton)
{ 
Crosshair.Instance.ChangeCrosshair(Crosshair.CrosshairMode.Button);
}
this.currentButton = raycastHit.transform.parent.GetComponent<MyButton>();
return;
}
if (this.currentButton)
{ 
Crosshair.Instance.ChangeCrosshair(Crosshair.CrosshairMode.Normal);
}
this.currentButton = null;
playerCam
raycastHit
}

private void CheckInput()
{ 
if (!this.currentButton)
{ 
return;
}
if (Input.GetButtonDown("Use"))
{ 
this.currentButton.ActivateButton();
}
}
}
