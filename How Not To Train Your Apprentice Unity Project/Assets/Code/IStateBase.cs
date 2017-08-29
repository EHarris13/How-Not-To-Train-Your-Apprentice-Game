using UnityEngine;
using System.Collections;

/// William Howell
/// wph140030
/// wph140030@utdallas.edu
/// Senior Project

namespace howellW {
	/// <summary>
	/// IState Base.
	/// Provides common functionality for game states and enables communication
	/// between the GameStateManager and the states.
	/// </summary>
	public interface IStateBase {
		/// <summary>
		/// Enables GameStateManager to query the current state for its identity.
		/// </summary>
		GameStates State { get; }

		/// <summary>
		/// Synchronizes instantiation for each state.
		/// GameStateManager calls this method when the scene is loaded.
		/// Saves object references once the scene is loaded.
		/// </summary>
		void Initialize();

		/// <summary>
		/// Synchronizes updates for each state.
		/// GameStateManager calls this method inside Update()
		/// </summary>
		void StateUpdate();

		/// <summary>
		/// Synchronizes GUI updates for each state.
		/// GameStateManager calls this method inside onGUI()
		/// </summary>
		void StateGUI();
	}
}
