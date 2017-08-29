using UnityEngine;

public class MenuMain : MonoBehaviour {

	public GameObject StartMenu;
	public GameObject OptionsMenu;
	public GameObject CreditsMenu;

	void Awake() {
		StartMenu.SetActive(false);
		OptionsMenu.SetActive(false);
		CreditsMenu.SetActive(false);
	}

	public void StartPressed() {
		swapToMenu(StartMenu);
	}

	public void OptionsPressed() {
		swapToMenu(OptionsMenu);
	}

	public void CreditsPressed() {
		swapToMenu(CreditsMenu);
	}

	public void ExitPressed() {
		UnityEditor.EditorApplication.isPlaying = false;
	}

	private void swapToMenu(GameObject otherMenu) {
		otherMenu.SetActive(true);
		gameObject.SetActive(false);
	}
}
