using UnityEngine;

/// William Howell
/// wph140030
/// wph140030@utdallas.edu
/// Senior Project

namespace howellW {
	/// <summary>
	/// GameState enum.
	/// Enumeration for the scenes as a way to help the
	/// GameStateManager communicate with states and identify them.
	/// Matches the alphabetical build order of the scenes.
	/// Alphabetical order is irrelevent when all levels live in the same scene.
	/// </summary>
	public enum GameStates {}

	/// <summary>
	/// GameStateManager.
	/// Controls state and scene behaviour and synchronization with MonoBehaviour.
	/// </summary>
	public class GameStateManager : MonoBehaviour {

		/// <summary>
		/// Static reference to this object.
		/// Ensures only one of these objects exists at any time during execution.
		/// </summary>
		public static GameStateManager Self;

		/// <summary>
		/// Reference to the GameDataManager.
		/// Used to change the level number.
		/// </summary>
		public GameDataManager GDM;

		/// <summary>
		/// Polymorphic reference that enables the GameStateManager
		/// to communicate with the concrete state classes.
		/// </summary>
		public IStateBase ActiveState;

		/// <summary>
		/// Reference to an enum value so that the GameStateManager can actually tell what ActiveState is.
		/// </summary>
		public GameStates CurrentState;

		/// <summary>
		/// Determines whether or not an instance of GameStateManager already exists and,
		/// if so, destroys the duplicate.
		/// </summary>
		void Awake() {
			if (Self == null) {
				Self = this;
				DontDestroyOnLoad(gameObject);
			} else {
				DestroyImmediate(gameObject);
				Debug.Log("A duplicate GameStateManager has been destroyed.");
			}
		}

		/// <summary>
		/// Starts this object by initializing ActiveState to a new BeginState,
		/// setting CurrentState to the corresponding value for BeginState,
		/// and then calls the state's initialization code.
		/// </summary>
		void Start() {
			/*
			Debug.Log("Starting GameStateManager...");
			ActiveState = new LevelOneState(this);
			CurrentState = GameStates.LevelOne;
			ActiveState.Initialize();

			GDM = GameObject.Find("GameManager").GetComponent<GameDataManager>();

			LevelTwoContainer = GameObject.Find("LevelTwoContainer");
			LevelThreeContainer = GameObject.Find("LevelThreeContainer");
			LevelFourContainer = GameObject.Find("LevelFourContainer");

			LoadLevelOne();
			*/

			Debug.Log("Start complete.");
		}

		/// <summary>
		/// Updates the current scene by calling the current state's update method.
		/// </summary>
		void Update() {
			if (ActiveState != null) {
				ActiveState.StateUpdate();
			}
		}

		/// <summary>
		/// Switches the current ActiveState to a new state,
		/// and then sets CurrentState to the right state.
		/// Determines which level should be revealed.
		/// </summary>
		/// <param name = "newState">A newly constructed object that implements IStateBase.</param>
		public void SwitchState(IStateBase newState) {
			ActiveState = newState;
			CurrentState = newState.State;
			Debug.Log("State switched to " + CurrentState + ".");

			/*
			// Determines which level should be loaded by checking what CurrentState is.
			switch (CurrentState) {
			case GameStates.LevelTwo:
				LoadLevelTwo();
				break;
			case GameStates.LevelThree:
				LoadLevelThree();
				break;
			case GameStates.LevelFour:
				LoadLevelFour();
				break;
			default:
				break;
			}
			*/
		}

		/*
		/// <summary>
		/// Loads LevelOne by hiding the containers for the other levels.
		/// </summary>
		public void LoadLevelOne() {
			LevelTwoContainer.SetActive(false);
			LevelThreeContainer.SetActive(false);
			LevelFourContainer.SetActive(false);
		}

		/// <summary>
		/// Loads LevelTwo by revealing the container for LevelTwo and changing the level number to 2.
		/// </summary>
		public void LoadLevelTwo() {
			GDM.LevelNumber = 2;
			LevelTwoContainer.SetActive(true);
		}

		/// <summary>
		/// Loads LevelThree by revealing the container for LevelThree and changing the level number to 3.
		/// </summary>
		public void LoadLevelThree() {
			GDM.LevelNumber = 3;
			LevelThreeContainer.SetActive(true);
		}

		/// <summary>
		/// Loads LevelThree by revealing the container for LevelThree and changing the level number to 3.
		/// </summary>
		public void LoadLevelFour() {
			GDM.LevelNumber = 4;
			LevelFourContainer.SetActive(true);
		}
		*/

		/// <summary>
		/// Once a level is loaded, initialize its corresponding ActiveState.
		/// SwitchState ensures ActiveState is connected to the right state.
		/// </summary>
		void OnLevelWasLoaded(int levelName) {
			if (levelName == (int) CurrentState) {
				Debug.Log("levelName is " + levelName + " and CurrentState is " + (int) CurrentState);
				Debug.Log("Initializing scene.");
				ActiveState.Initialize();
			} else {
				Debug.Log("There is a problem with the numbering of the levels.\n" +
					"levelName is " + levelName + " and CurrentState is " + (int) CurrentState);
			}
		}
	}
}