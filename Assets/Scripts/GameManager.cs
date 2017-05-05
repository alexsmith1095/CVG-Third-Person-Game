using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class GameManager : MonoBehaviour {

    private static GameManager _main;

    public static GameManager Main {
		get {
			if(_main == null) {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
			}
			return _main;
        }
    }

    void OnEnable () {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable () {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        timeCount = 0;
        deathCount = 0;
        deathScreen = GameObject.Find("DeathScreen");
        if (deathScreen != null) {
            deathScreen.SetActive(false);
        }
        if (XCI.GetNumPluggedCtrlrs() > 0) {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide the cursor
        } else {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Show the cursor
        }
    }

	void Awake () {
        _main = this;
        DontDestroyOnLoad(this);
        // Subscribe to player events
        PlayerEvents.playerDamaged += DamagePlayer;     // Run DamagePlayer  function when player  is damaged
        PlayerEvents.playerDead += RespawnPlayer;       // Run RespawnPlayer function when player  is dead
        PlayerEvents.gravityToggled += ToggleGravity;   // Run ToggleGravity function when gravity is toggled
    }

    private int playerHealth = 5;
    public static int deathCount = 0;
    public static float timeCount = 0;
    public static bool gravityReversed = false;
    public GameObject deathScreen;

    public void DoNothing () {
        // just used to execute the script
    }

    void Update () {
        timeCount += Time.deltaTime;
    }

    void ToggleGravity () {
        gravityReversed = !gravityReversed;
    }

    void DamagePlayer (GameObject player, int amount) {
        playerHealth -= amount;
        if (playerHealth <= 0 ) {
            deathScreen.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().enabled = false;
            PlayerEvents.PlayerDead(player, 5);
        }
    }

    void RespawnPlayer (GameObject player, int amount) {
        StartCoroutine(RespawnPlayerCoroutine(player, amount));
    }

    public IEnumerator RespawnPlayerCoroutine (GameObject player, int amount) {
        player.transform.position = Checkpoint.GetCurrentCheckpoint();
        playerHealth = amount;
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().enabled = true;
        yield return new WaitForSeconds(1);
        deathScreen.SetActive(false);
    }
}
