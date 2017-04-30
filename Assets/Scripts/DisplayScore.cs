using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {

    public Text deathsText;
    public Text timeText;
    private string gameTime;

	void Start () {

	}

	void OnEnable () {
		gameTime = string.Format("{0}:{1:00}", (int)GameManager.timeCount / 60, (int)GameManager.timeCount % 60);
		timeText.text = gameTime;
		deathsText.text = GameManager.deathCount.ToString();
	}
}
