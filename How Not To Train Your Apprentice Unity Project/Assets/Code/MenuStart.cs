﻿using UnityEngine;

public class MenuStart : MonoBehaviour {

	public GameObject MainMenu;

	public void BackPressed() {
		swapToMenu(MainMenu);
	}

	private void swapToMenu(GameObject otherMenu) {
		otherMenu.SetActive(true);
		gameObject.SetActive(false);
	}
}
