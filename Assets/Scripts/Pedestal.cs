using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Pedestal : MonoBehaviour {

    public bool hasPlayer;
    private Gem gem;

	void Start () {
        gem = GameObject.Find("Gem").GetComponent<Gem>();
	}

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
            hasPlayer = true;

			// If player has the gem
            if (hasPlayer && gem.collected) {
                if (XCI.GetNumPluggedCtrlrs() > 0)
                    PlayerEvents.DisplayPrompt("Press Y to Place Object", 100);
                else
                    PlayerEvents.DisplayPrompt("Press E to Place Object", 100);
            }

			// If gem is in the skull
            if (gem.transform.parent.name == "Pedestal1" && name == "Pedestal1" ||
				gem.transform.parent.name == "Pedestal2" && name == "Pedestal2") {
				if (XCI.GetNumPluggedCtrlrs() > 0)
                    PlayerEvents.DisplayPrompt("Press Y to Pick Up", 100);
                else
                    PlayerEvents.DisplayPrompt("Press E to Pick Up", 100);
            }

			// If player is at the second skull without the gem
            if (gem.transform.parent.name == "Pedestal1" && name == "Pedestal2") {
                    PlayerEvents.DisplayPrompt("Looks like an eye is missing...", 100);
            }
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			hasPlayer = false;
			PlayerEvents.DisplayPrompt("", 100);
		}
	}
}
