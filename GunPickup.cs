using System;


public class GunPickup : Powerup
{ 
public override void Activate()
{ 
PlayerPowers.Instance.Gun();
MainText.Instance.PutText("GUN");
}
}
