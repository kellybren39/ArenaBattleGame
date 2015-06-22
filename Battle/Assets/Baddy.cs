using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Baddy : MonoBehaviour
{
	public float moveSpeed = 2f;					// The speed the enemy moves at.
	public float HP;									// How many times the enemy can be hit before it dies.
	public float value;								// How many points this enemy is worth
	public Sprite deadEnemy;						// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;						// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;					// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;				// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;				// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;				// A value to give the maximum amount of Torque when dying
	public Dictionary<string, GameObject> sides;	// 

	
	
	private SpriteRenderer ren;				// Reference to the sprite renderer.
	private Transform frontCheck;			// Reference to the position of the gameobject used for checking
											// 	if something is in front.
	private bool dead = false;				// Whether or not the enemy is dead.
	private CounterBehaviour counter;				// Reference to the Score script.
	private GameObject player;				// Reference to the Player
	//private Dictionary<string, int> HPLookup;
	
	
	void Awake()
	{
		// Setting up the references.
		//ren = transform.Find("body").GetComponent<SpriteRenderer>();
		counter = GameObject.Find("Counter").GetComponent<CounterBehaviour>();//.GetComponent<Score>();
		player = GameObject.Find ("Hero");
		/**
		HPLookup = new Dictionary<string, int>();
		HPLookup.Add("Bear", 2);
		HPLookup.Add("Slime", 1);
		*/
		sides = new Dictionary<string, GameObject>();
		/**
		sides.Add("top", null);
		sides.Add("front", null);
		sides.Add("back", null);
		*/

	}



	void FixedUpdate ()
	{
		if (transform.position.y < -20f){
			HP = 0;
		}
		/**
		// Create an array of all the colliders in front of the enemy.
		Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);
		
		// Check each of the colliders.
		foreach(Collider2D c in frontHits)
		{
			// If any of the colliders is an Obstacle...
			if(c.tag == "Obstacle")
			{
				// ... Flip the enemy and stop checking the other colliders.
				Flip ();
				break;
			}
		}
		
		// Set the enemy's velocity to moveSpeed in the x direction.
		rigidbody2D.velocity = new Vector2(transform.localScale.x * moveSpeed, rigidbody2D.velocity.y);	
		*/


		transform.LookAt(player.transform);
		float MinDist = 0.5f;
		float MaxDist = 0.5f;
		
		if(Vector3.Distance(transform.position, player.transform.position) >= MinDist){

			int multiplier = 1;

			if((transform.position.x < player.transform.position.x) 
			   && (sides.ContainsKey("back"))){
				if(sides["back"].name == "Turbo"){
					multiplier = 4;
				}
			}
			if((transform.position.x > player.transform.position.x) 
			   && (sides.ContainsKey("front"))){
				if(sides["front"].name == "Turbo"){
					multiplier = 4;
				}
			//print (transform.forward*moveSpeed*Time.deltaTime);
			}


			transform.position += transform.forward*moveSpeed*Time.deltaTime*multiplier;

			if(Vector3.Distance(transform.position, player.transform.position) <= MaxDist)
			{

			} 
			
		}

		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1 && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;
		
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
			// ... call the death function.
			Death ();
	}
	
	public void Hurt(string side)
	{
		float damage = 1f;
		// Reduce the number of hit points by one.
		if (sides.ContainsKey (side)) {
			GameObject feature = sides [side];
			if (feature.name == "Shell") {
					damage *= .5f;
			}
		}
		HP -= damage;
	}
	
	void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		
		// Set dead to true.
		dead = true;
		counter.score += value;
		counter.count--;

		Destroy (gameObject);

	}
	
	
	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
