using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour {

    public delegate void DamageHandler(GameObject player, int amount);
    public delegate void GravityHandler();
    public delegate void PickupHandler(GameObject pickup);
    public delegate void PromptHandler(string text, int duration);

    public static event DamageHandler playerDamaged;
    public static event DamageHandler playerDead;
    public static event GravityHandler gravityToggled;
    public static event PromptHandler displayPrompt;

    public static void PlayerDamaged(GameObject player, int amount) {
        if(playerDamaged != null)
            playerDamaged(player, amount);
    }

	public static void PlayerDead(GameObject player, int amount) {
		if(playerDead != null)
			playerDead(player, amount);
    }

    public static void GravityToggled() {
        if(gravityToggled != null)
            gravityToggled();
    }

    public static void DisplayPrompt(string text, int duration) {
        if(displayPrompt != null)
            displayPrompt(text, duration);
    }
}
