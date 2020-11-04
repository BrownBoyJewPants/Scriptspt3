using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{ 
public Transform spawnPos;

public GameObject enemy;

[HideInInspector]
public bool isRewinding;

private float rewindTime = 1f;

private float rewindSeconds = 0.175f;

private Vector3 vel;

[HideInInspector]
public bool playerDead;

[HideInInspector]
public bool paused;

[HideInInspector]
public bool playing = true;

private Transform playerTransform;

public GameObject rewindSymbol;

public GameObject deathScreen;

public static GameManager Instance;

private List<GameObject> enemies;

private List<Vector3> positions;

private float t;

private void Awake()
{ 
GameManager.Instance = this;
this.enemies = new List<GameObject>();
this.positions = new List<Vector3>();
AutoSplitterData.levelID = SceneManager.GetActiveScene().buildIndex - 1;
}

private void Start()
{ 
this.playerTransform = PlayerMovement.Instance.transform;
this.playerTransform.position = this.spawnPos.position;
this.t = 1f;
Time.timeScale = 1f;
}

public void AddEnemy(GameObject enemy)
{ 
this.enemies.Add(enemy);
this.positions.Add(enemy.transform.position);
}

private void RestartEnemies()
{ 
using (List<GameObject>.Enumerator enumerator = this.enemies.GetEnumerator())
{ 
while (enumerator.MoveNext())
{ 
UnityEngine.Object.Destroy(enumerator.Current);
}
}
this.enemies = new List<GameObject>();
foreach (Vector3 current in this.positions)
{ 
this.enemies.Add(UnityEngine.Object.Instantiate<GameObject>(this.enemy, current, Quaternion.identity));
}
this.positions = new List<Vector3>();
enumerator
enumerator2
current
}

private void Update()
{ 
if (!this.isRewinding)
{ 
return;
}
this.playerTransform.position = Vector3.Lerp(this.playerTransform.position, this.spawnPos.position, this.t);
this.t += Time.deltaTime * 0.17f;
PPController.Instance.UpdateFx(Mathf.Clamp(this.t * 10f, 0f, 1f));
if (Vector3.Distance(this.playerTransform.position, this.spawnPos.position) < 0.1f)
{ 
this.StopRewinding();
}
}

public void PlayerDied()
{ 
UIManager.Instance.HidePause();
Cursor.lockState = CursorLockMode.None;
Cursor.visible = true;
this.deathScreen.SetActive(true);
this.playerDead = true;
PlayerStatus.Instance.Damage(100);
this.playing = false;
}

public void LevelDone()
{ 
UIManager.Instance.HidePause();
Cursor.lockState = CursorLockMode.None;
Cursor.visible = true;
this.playing = false;
}

public void StartRewind()
{ 
this.isRewinding = true;
this.t = 0f;
PlayerMovement.Instance.GetRb().useGravity = false;
PlayerMovement.Instance.GetRb().velocity = Vector3.zero;
PPController.Instance.StartRewind();
this.rewindSymbol.SetActive(true);
this.RestartEnemies();
}

private void StopRewinding()
{ 
this.isRewinding = false;
PlayerMovement.Instance.GetRb().useGravity = true;
this.t = 1f;
PPController.Instance.StopRewind();
this.rewindSymbol.SetActive(false);
}

public void Restart()
{ 
}
}
