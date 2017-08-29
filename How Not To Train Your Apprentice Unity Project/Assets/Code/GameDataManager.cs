using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

	public static GameDataManager Self;

	// This is an amalgamation of the fields below.
	// This is a temporary container for the options settings.
	// This way, the options menu does not directly change PlayerPrefs
	public static Dictionary<string, int> PlayerData = new Dictionary<string, int>();

	public bool isWindowed;
	public bool isYAxisInverted;
	public int MouseSensitivity;
	public int MasterVolume;
	public int GameVolume;
	public int MusicVolume;
	public int ResolutionIndex;

	void Awake() {
		if (Self == null) {
			Self = this;
			DontDestroyOnLoad(gameObject);
		} else {
			DestroyImmediate(gameObject);
		}

		PlayerData.Add("isWindowed", isWindowed ? 1 : 0);
		PlayerData.Add("isYAxisInverted", isYAxisInverted ? 1 : 0);
		PlayerData.Add("MouseSensitivity", MouseSensitivity);
		PlayerData.Add("MasterVolume", MasterVolume);
		PlayerData.Add("GameVolume", GameVolume);
		PlayerData.Add("MusicVolume", MusicVolume);
		PlayerData.Add("ResolutionIndex", ResolutionIndex);

		//NotificationCenter.Default.AddObserver("SaveSetting", SaveSetting);
		//NotificationCenter.Default.AddObserver("LoadSetting", LoadSetting);
	}

	void SaveSetting(object value) {
		PlayerPrefs.SetInt("isWindowed", PlayerData["isWindowed"]);
		PlayerPrefs.SetInt("isYAxisInverted", PlayerData["isYAxisInverted"]);
		PlayerPrefs.SetInt("MouseSensitivity", PlayerData["MouseSensitivity"]);
		PlayerPrefs.SetInt("MasterVolume", PlayerData["MasterVolume"]);
		PlayerPrefs.SetInt("GameVolume", PlayerData["GameVolume"]);
		PlayerPrefs.SetInt("MusicVolume", PlayerData["MusicVolume"]);
		PlayerPrefs.SetInt("ResolutionIndex", PlayerData["ResolutionIndex"]);
		PlayerPrefs.Save();
	}

	void LoadSetting(object value) {
		isWindowed = PlayerPrefs.GetInt("isWindowed") == 1;
		isYAxisInverted = PlayerPrefs.GetInt("isYAxisInverted") == 1;
		MouseSensitivity = PlayerPrefs.GetInt("MouseSensitivity");
		MasterVolume = PlayerPrefs.GetInt("MasterVolume");
		GameVolume = PlayerPrefs.GetInt("GameVolume");
		MusicVolume = PlayerPrefs.GetInt("MusicVolume");
		ResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");

		updateDictionary();

		//NotificationCenter.Default.PostNotification("PassSettings");
	}

	private void updateDictionary() {
		PlayerData["isWindowed"] = isWindowed ? 1 : 0;
		PlayerData["isYAxisInverted"] = isYAxisInverted ? 1 : 0;
		PlayerData["MouseSensitivity"] = MouseSensitivity;
		PlayerData["MasterVolume"] = MasterVolume;
		PlayerData["GameVolume"] = GameVolume;
		PlayerData["MusicVolume"] = MusicVolume;
		PlayerData["ResolutionIndex"] = ResolutionIndex;
	}
}
