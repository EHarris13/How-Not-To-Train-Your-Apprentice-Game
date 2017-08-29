using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionController : Actor {

	public Vector3 LastCheckpoint;

	public int healthPickupAmount = 10;

	public AudioClip CoinSFX;
	public AudioClip SpecialPickupSFX;
	public AudioClip HurtSFX;
	public AudioClip HealthPickupSFX;
	public AudioClip CheckpointSFX;

	private GameDataManager gdm;
	private AudioSource source;


	private Text status;
	private Text hud;

	private float volLowRange = 0.5f;
	private float volHighRange = 1f;
	private float timer;

	void Awake() {
		gdm = GameObject.Find("GameManager").GetComponent<GameDataManager>();
		source = GetComponent<AudioSource>();
		status = GameObject.Find("Status").GetComponent<Text>();
		hud = GameObject.Find("HUD").GetComponent<Text>();
	}

	void Start() {
		status.text = "";
		hud.text = "";

		CurrentHealth = MaximumHealth;
		LastCheckpoint = transform.position;
	}

	void Update() {
		OnTimerStart();
		updateGUI();

		if (CurrentHealth <= MinimumHealth) {
			IsDead = true;
		}

		if (IsDead) {
			handleDeath();
		}
	}

	void OnTriggerEnter(Collider other) {

		// Pickups


		if (other.CompareTag("Checkpoint")) {
			LastCheckpoint = other.transform.position;
			playSound(CheckpointSFX);

			status.text = "Checkpoint!";
			timer = 2F;
		}

		// Environment Volumes
		if (other.CompareTag("KillZone")) {
			IsDead = true;
			playSound(HurtSFX);
		}
	}

	void OnTriggerExit(Collider other) {}

	public override void TakeDamage(int damage) {
		CurrentHealth -= damage;
		playSound(HurtSFX);
	}

	void updateGUI() {}

	void OnTimerStart() {
		if (timer > 0) {
			timer -= Time.deltaTime;
			if (timer <= 0) {
				status.text = "";
			}
		}
	}

	private void handleDeath() {
		transform.position = LastCheckpoint;
		CurrentHealth = MaximumHealth;

		IsDead = false;
	}

	private void playSound(AudioClip sfx) {
		float vol = Random.Range(volLowRange, volHighRange);
		source.PlayOneShot(sfx, vol);
	}
}
