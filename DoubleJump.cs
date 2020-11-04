using System;


public class DoubleJump : Powerup
{ 
public override void Activate()
{ 
PlayerPowers.Instance.DoubleJump();
MainText.Instance.PutText("DOUBLE JUMP");
}
}
