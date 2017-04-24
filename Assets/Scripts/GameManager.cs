using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

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

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        deathScreen = GameObject.Find("DeathScreen");
        if (deathScreen != null)
            deathScreen.SetActive(false);
    }

	void Awake() {
        _main = this;
        DontDestroyOnLoad(this);
        // Subscribe to player events
        PlayerEvents.playerDamaged += DamagePlayer;     // Run DamagePlayer  function when player  is damaged
        PlayerEvents.playerDead += RespawnPlayer;       // Run RespawnPlayer function when player  is dead
        PlayerEvents.gravityToggled += ToggleGravity;   // Run ToggleGravity function when gravity is toggled
    }

    private int playerHealth = 5;
    public static bool gravityReversed = false;
    public GameObject deathScreen;

    public void DoNothing() {
        // just used to execute the script
    }

    void DamagePlayer(GameObject player, int amount) {
        playerHealth -= amount;
        if (playerHealth <= 0 ) {
            deathScreen.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().enabled = false;
            PlayerEvents.PlayerDead(player, 5);
        }
    }

    void ToggleGravity() {
        gravityReversed = !gravityReversed;
    }

    void RespawnPlayer(GameObject player, int amount) {
        StartCoroutine(RespawnPlayerCoroutine(player, amount));
    }

    public IEnumerator RespawnPlayerCoroutine(GameObject player, int amount) {
        player.transform.position = Checkpoint.GetCurrentCheckpoint();
        playerHealth = amount;
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().enabled = true;
        yield return new WaitForSeconds(1);
        deathScreen.SetActive(false);
    }
}
