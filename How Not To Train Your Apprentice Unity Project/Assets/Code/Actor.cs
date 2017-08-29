using UnityEngine;

public class Actor : MonoBehaviour {

	public int MinimumHealth = 0;
	public int MaximumHealth = 100;
	public bool IsDead;
	public bool Debug;
	public int CurrentHealth;

	void Awake() {
		CurrentHealth = MaximumHealth;
		IsDead = false;
	}

	public virtual void TakeDamage(int damage) {
		CurrentHealth -= damage;
	}
}
