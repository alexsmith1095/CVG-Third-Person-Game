using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Gem : MonoBehaviour {

    public Pedestal skull1;
    public Pedestal skull2;
    public GameObject gravityTrigger;
    public ParticleSystem particles;
    public ParticleSystem fallingParticles;
    public bool collected;
    public GameObject invertedBridge;
    public GameObject normalBridge;
    public GameObject gemIcon;

    private MeshRenderer meshRenderer;
    private Light light;
    private Vector3 skullPos;

    void Start () {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        light = transform.FindChild("Light").gameObject.GetComponent<Light>();
        skullPos = new Vector3(0.00127f, 0.0013f, 0.00802f);
        var fallingEmission = fallingParticles.emission;
        fallingEmission.enabled = false;
        invertedBridge.SetActive(true);
        normalBridge.SetActive(false);
        gemIcon.SetActive(false);
    }

    void Update () {
        if (collected) {
            // Player is at the first skull
            if (skull1.hasPlayer) {
                // Player places the gem
                if(Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.Y)) {
                    collected = false;
                    transform.SetParent(skull1.gameObject.transform);
                    transform.localPosition = skullPos;
                    meshRenderer.enabled = true;
                    light.intensity = 2f;
                    gemIcon.SetActive(false);
                    if (XCI.GetNumPluggedCtrlrs() > 0)
                        PlayerEvents.DisplayPrompt("Press Y to Pick Up", 100);
                    else
                        PlayerEvents.DisplayPrompt("Press E to Pick Up", 100);
                }
            }

            // Player is at the second skull
            if (skull2.hasPlayer) {
                // Player places gem
                if(Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.Y)) {
                    collected = false;
                    transform.SetParent(skull2.gameObject.transform);
                    transform.localPosition = skullPos;
                    meshRenderer.enabled = true;
                    light.intensity = 2f;
                    gemIcon.SetActive(false);
                    StartCoroutine ("GravityOff"); // Puzzle is solved
                    if (XCI.GetNumPluggedCtrlrs() > 0)
                        PlayerEvents.DisplayPrompt("Press Y to Pick Up", 100);
                    else
                        PlayerEvents.DisplayPrompt("Press E to Pick Up", 100);
                }
            }
        } else {
            // Gem is in first skull & player is at first skull
            if (transform.parent.name == "Pedestal1" && skull1.hasPlayer) {
                // Pick up gem
                if (Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.Y)) {
                    collected = true;
                    transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
                    transform.localPosition = Vector3.zero;
                    meshRenderer.enabled = false;
                    light.intensity = .5f;
                    gemIcon.SetActive(true);
                    if (XCI.GetNumPluggedCtrlrs() > 0)
                        PlayerEvents.DisplayPrompt("Press Y to Place Object", 100);
                    else
                        PlayerEvents.DisplayPrompt("Press E to Place Object", 100);
                }
            }
            // Gem is in second skull & player is at second skull
            if (transform.parent.name == "Pedestal2" && skull2.hasPlayer) {
                // Pick up gem
                if (Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.Y)) {
                    collected = true;
                    transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
                    transform.localPosition = Vector3.zero;
                    meshRenderer.enabled = false;
                    light.intensity = .5f;
                    gravityTrigger.SetActive(true); // The puzzle is not solved
                    invertedBridge.SetActive(true);
                    normalBridge.SetActive(false);
                    gemIcon.SetActive(true);
                    var emission = particles.emission;
                    emission.enabled = true;
                    if (XCI.GetNumPluggedCtrlrs() > 0)
                        PlayerEvents.DisplayPrompt("Press Y to Place Object", 100);
                    else
                        PlayerEvents.DisplayPrompt("Press E to Place Object", 100);
                }
            }
        }
    }

    IEnumerator GravityOff () {
        // Turn particles off
        var emission = particles.emission;
        var fallingEmission = fallingParticles.emission;
        var pGravity = particles.main.gravityModifier;
        // Swap the bridge states
        invertedBridge.SetActive(false);
        normalBridge.SetActive(true);
        // Disable the gravity trigger
        gravityTrigger.SetActive(false);
        pGravity = 2;
        fallingEmission.enabled = true;
        yield return new WaitForSeconds(.5f);
        pGravity = 0;
        fallingEmission.enabled = false;
        emission.enabled = false;
    }
}
