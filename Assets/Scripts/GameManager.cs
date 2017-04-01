using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

    // public static GameManager Main {
    //     get {
    //         return Camera.main.GetComponent<GameManager>();
    //     }
    // }

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

    public void DoNothing() {
        // just used to execute the script
    }

    void DamagePlayer(GameObject player, int amount) {
        playerHealth -= amount;
        if (playerHealth <= 0 )
            PlayerEvents.PlayerDead(player, 5);
    }

    void RespawnPlayer(GameObject player, int amount) {
        player.transform.position = new Vector3(0, 0, 0);
        playerHealth = amount;
    }

    void ToggleGravity() {
        gravityReversed = !gravityReversed;
    }
}
