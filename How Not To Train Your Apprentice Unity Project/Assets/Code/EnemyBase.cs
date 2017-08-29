public class EnemyBase : Actor {

	void Update() {
		if (CurrentHealth <= MinimumHealth) {
			IsDead = true;
		}

		if (IsDead) {
			onDeath();
		}
	}

	public void onDeath() {
		Destroy(gameObject);
	}
}
