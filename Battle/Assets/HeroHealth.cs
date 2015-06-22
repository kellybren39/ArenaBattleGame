using UnityEngine;
using System.Collections;

public class HeroHealth : MonoBehaviour
{	
	public float health = 100f;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player
	public float spikeDamage = 2f;
	
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private HeroControl playerControl;		// Reference to the HeroControl script.
	private Animator anim;						// Reference to the Animator on the player
	private CounterBehaviour counter;				// Reference to the Score script.

	float height;
	
	
	void Awake ()
	{
		// Setting up references.
		playerControl = GetComponent<HeroControl>();
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		
		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;

		BoxCollider2D bCollider = GetComponent<BoxCollider2D>();
		height = bCollider.size.y;
		counter = GameObject.Find("Counter").GetComponent<CounterBehaviour>();
		//print ("Height of BoxCollider is " + height);
	}

	void Update () 
	{
		if (transform.position.y < -20f){
			health = 0;
			PlayerPrefs.SetFloat("Score", counter.score);
			Application.LoadLevel ("deadMenu");
		}
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		//print ("enemy Y = " + col.gameObject.transform.position.y);
		//print ("and tag = " + col.gameObject.tag);
		float playerY = transform.position.y - (height / 2);
		//print ("player Y = " + playerY);
		float damageMult = 1f;
		if(col.gameObject.tag == "Enemy")
		{
			if(playerY <= col.gameObject.transform.position.y){
			//print ("enemy Y > our y and is an enemy");
			// ... and if the time exceeds the time of the last hit plus the time between hits...
				if (Time.time > lastHitTime + repeatDamagePeriod) 
				{
					// ... and if the player still has health...
					if(health > 0f)
					{
						if(transform.position.x < col.gameObject.transform.position.x){
							if(col.gameObject.GetComponent<Baddy>().sides.ContainsKey("back")){
								if(col.gameObject.GetComponent<Baddy>().sides["back"].name == "Spikes"){
									damageMult = spikeDamage;
								}
							}
						}

						if(transform.position.x > col.gameObject.transform.position.x){
							if(col.gameObject.GetComponent<Baddy>().sides.ContainsKey("front")){
								if(col.gameObject.GetComponent<Baddy>().sides["front"].name == "Spikes"){
									damageMult = spikeDamage;
								}
							}
						}

						//print ("Sending us to TakeDamage function");
						// ... take damage and reset the lastHitTime.
						TakeDamage(col.gameObject.transform, damageMult); 
						lastHitTime = Time.time; 
					}
					// If the player doesn't have health, do some stuff.
					else
					{
						// Find all of the colliders on the gameobject and set them all to be triggers.
						Collider2D[] cols = GetComponents<Collider2D>();
						foreach(Collider2D c in cols)
						{
							c.isTrigger = true;
						}
						
						// Move all sprite parts of the player to the front
						SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
						foreach(SpriteRenderer s in spr)
						{
							s.sortingLayerName = "UI";
						}
						
						// ... disable user Player Control script
						GetComponent<HeroControl>().enabled = false;

						PlayerPrefs.SetFloat("Score", GetComponent<CounterBehaviour>().score);
						Application.LoadLevel ("deadMenu");
					}
				}
			}

			else{
				//print ("landed on its head");
				Baddy bad = col.gameObject.GetComponent<Baddy>();
				bad.Hurt("top");
				if (bad.sides.ContainsKey("top") && (bad.sides["top"].name == "Spikes")){
					damageMult = spikeDamage;
					TakeDamage(col.gameObject.transform, damageMult);
				}
				else{
					KnockBack (col.gameObject.transform, 5);
				}
			}
				
		}
	}

	public void KnockBack(Transform enemy, float power){
		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = (transform.position - enemy.position) * 4 + Vector3.up * 5f;
		if (power == 5) {
			hurtVector.x += 10;
				}
		
		//print ("hurtVector is " + hurtVector);
		
		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		rigidbody2D.AddForce(hurtVector * hurtForce * power);
		}
	
	public void TakeDamage (Transform enemy, float multiplier)
	{
		// Make sure the player can't jump.
		playerControl.jump = false;
		// ...or run
		playerControl.run = false;

		KnockBack (enemy, 10);
		
		// Reduce the player's health by 10.
		health -= damageAmount * multiplier;
		
		// Update what the health bar looks like.
		UpdateHealthBar();
	}
	
	
	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
		
		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
	}
}
