using System;


public class SwordPowerup : Powerup
{ 
public override void Activate()
{ 
MainText.Instance.PutText("SWORD");
Sword.Instance.Pickup();
}
}
