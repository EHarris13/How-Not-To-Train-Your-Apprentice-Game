using UnityEngine;
using UnityEngine.UI;

// NOTE: I found the code for the resolution array and the resToString function at
// http://answers.unity3d.com/questions/1065562/unity-52-using-dropdown-for-screen-resolutions.html
// and then I modified it a little to suit my purposes.

public class MenuOptions : MonoBehaviour {

	public GameObject MainMenu;

	public Toggle ToggleWindowed;
	public Toggle ToggleYAxis;

	public Slider SliderMouseSensitivity;
	public Slider SliderMasterVolume;
	public Slider SliderGameVolume;
	public Slider SliderMusicVolume;

	public Dropdown DropdownResolution;

	private Resolution[] resolutions;

	void Awake() {
		resolutions = Screen.resolutions;
		DropdownResolution.options.Clear();

		for (int i = 0; i < resolutions.Length; i++) {
			DropdownResolution.options.Add(new Dropdown.OptionData(resToString(resolutions[i])));
		}
	}

	void OnEnable() {
		//NotificationCenter.Default.AddObserver("PassSettings", LoadSettings);
		//NotificationCenter.Default.PostNotification("LoadSetting");
	}

	void OnDisable() {
		//NotificationCenter.Default.RemoveObserver("PassSettings", LoadSettings);
	}

	public void ConfirmPressed() {
		SaveSettings(null);
		//NotificationCenter.Default.PostNotification("SaveSetting");
		swapToMenu(MainMenu);
	}

	public void BackPressed() {
		swapToMenu(MainMenu);
	}

	void LoadSettings(object value) {
		ToggleWindowed.isOn = GameDataManager.PlayerData["isWindowed"] != 0;
		ToggleYAxis.isOn = GameDataManager.PlayerData["isYAxisInverted"] != 0;
		SliderMouseSensitivity.value = GameDataManager.PlayerData["MouseSensitivity"];
		SliderMasterVolume.value = GameDataManager.PlayerData["MasterVolume"];
		SliderGameVolume.value = GameDataManager.PlayerData["GameVolume"];
		SliderMusicVolume.value = GameDataManager.PlayerData["MusicVolume"];
		DropdownResolution.value = GameDataManager.PlayerData["ResolutionIndex"];
	}

	void SaveSettings(object value) {
		GameDataManager.PlayerData["isWindowed"] = ToggleWindowed.isOn ? 1 : 0;
		GameDataManager.PlayerData["isYAxisInverted"] = ToggleYAxis.isOn ? 1 : 0;
		GameDataManager.PlayerData["MouseSensitivity"] = (int) SliderMouseSensitivity.value;
		GameDataManager.PlayerData["MasterVolume"] = (int) SliderMasterVolume.value;
		GameDataManager.PlayerData["GameVolume"] = (int) SliderGameVolume.value;
		GameDataManager.PlayerData["MusicVolume"] = (int) SliderMusicVolume.value;
		GameDataManager.PlayerData["ResolutionIndex"] = DropdownResolution.value;
	}

	private string resToString(Resolution res) {
		return (res.width + " x " + res.height);
	}

	private void swapToMenu(GameObject otherMenu) {
		otherMenu.SetActive(true);
		gameObject.SetActive(false);
	}
}
