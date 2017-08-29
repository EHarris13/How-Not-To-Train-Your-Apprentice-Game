using UnityEngine;

public enum PickupType {
	HealthPickup,
}

public class Pickup : MonoBehaviour {
	public delegate void OnDied(Pickup thisPickup);

	public event OnDied onDied;

	public PickupType Type;

	public int Value;

	public void DestroyMe() {
		Destroy(gameObject);
	}

	public virtual void Died() {
		if (onDied != null) {
			onDied(this);
		}
		DestroyMe();
	}
}
