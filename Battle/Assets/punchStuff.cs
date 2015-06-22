using UnityEngine;
using System.Collections;

public class punchStuff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Destroy the rocket after seconds if it doesn't get destroyed before then.
		Destroy(gameObject, 0.5f);
	}
	
	void OnCollisionEnter2D (Collision2D col) 
	{
		print ("We hit something");
		float s = col.transform.position.x - transform.root.transform.position.x;
		string side;
		if (s > 0){
			side = "back";
		}else{
			side = "front";
		}
		// If it hits an enemy...
		if(col.gameObject.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Baddy>().Hurt(side);

			
			// Destroy the rocket.
			Destroy (gameObject);
		}
		// Otherwise if it hits a spikes
		else if(col.gameObject.tag == "Spikes")
		{
			// 
			col.gameObject.transform.root.GetComponent<Baddy>().Hurt(side);

			gameObject.transform.root.GetComponent<HeroHealth>().TakeDamage(col.gameObject.transform.root, 2f);
			
			// Destroy.
			Destroy (gameObject);
		}
	}
}
