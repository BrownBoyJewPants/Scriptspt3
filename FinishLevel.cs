using System;
using UnityEngine;


public class FinishLevel : MonoBehaviour
{ 
private void OnCollisionEnter(Collision other)
{ 
if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
{ 
AutoSplitterData.levelBeaten = 1;
WinScreen.Instance.gameObject.SetActive(true);
GameManager.Instance.LevelDone();
SaveManager.Instance.state.times[AutoSplitterData.levelID] = Timer.Instance.GetTimer();
SaveManager.Instance.Save();
PlayerMovement.Instance.GetRb().isKinematic = true;
}
}
}
