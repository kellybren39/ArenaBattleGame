using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CounterBehaviour : MonoBehaviour {

	//Keeps track of the number of enemies in the stage
	public int count;
	public int level;
	public int numTypes;
	public GameObject[] enemies;
	public GameObject[] features;
	public float score;
	
	// Use this for initialization
	void Start () {
		level = 0;
		count = 0;
		numTypes = 2;
		score = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (count == 0) 
		{
			level++;
			//MakeBear (2);
			//count++;
			Spawn ();
		}
	}
	
	// Generates a bunch of new baddies
	void Spawn()
	{
		int enemies = level; //TO-DO: make this some kind of algorithm
		for (int e = 0; e < enemies; e++)
		{
			//print ("butt");
			Random rand = new Random();
			int type = Random.Range(0, numTypes);
			count++;
			//type = 1;
			Make(type);
		}
	}
	
	//Starts generating the new baddy, handing off most work to enemy specific functions
	void Make(int type)
	{
		int feats;
		if (level <= 4) {
				feats = Random.Range (1, 2);
		} else if (level <= 8) {
				feats = Random.Range (1, 3);
		} else {
				feats = Random.Range (2, 3);
		}

		if (type < 1) {
			MakeBear (feats);
		} else if (type < 2) {
			MakeSlime (feats);
		}
	}

	//Makes a bear
	void MakeBear(int feats)
	{
		Dictionary<int, int> taken = new Dictionary<int, int> ();
		GameObject bear = (GameObject) Instantiate (enemies [0]);
		int spawnX = Random.Range (-50, 50);
		int spawnY = 5;
		Vector3 spawnV = new Vector3 (spawnX, spawnY, 0);
		//print ("feats is " + feats);
		for (int f = 0; f < feats; f++) {
			//Index of feature
			int t = Random.Range(0, 3);;
			//t = 0;
			//Random rand = new Random();
			bool take = true;
			while(take){
				if(!taken.ContainsKey(t)){
					taken.Add(t, 1);
					take = false;
				}else{
					t = Random.Range(0, 3);
				}
			}
			int type = Random.Range(0, 3);
			//type = 2;
			//print ("type is " + type);
			//Location of feature
			//Back
			if (type < 1){
				
				GameObject feat = (GameObject) Instantiate (features[t]);
				feat.transform.parent = bear.transform;
				if (t == 1){
					feat.transform.Translate(0, -4, 0);
				}else{
					feat.transform.Translate(-4, 0, 0);
					}
				feat.transform.Rotate(0, 0, 90);
				bear.GetComponent<Baddy>().sides.Add("back", features[t]);
			//Front
			}else if(type < 2){
				GameObject feat = (GameObject) Instantiate (features[t]);
				feat.transform.parent = bear.transform;
				if (t == 1){
					feat.transform.Translate(0, 1, 0);
				}else{
					feat.transform.Translate(1, 0, 0);
				}
				feat.transform.Rotate(0, 0, 270);
				bear.GetComponent<Baddy>().sides.Add("front", features[t]);
			//top
			}else if(type<3){
				GameObject feat = (GameObject) Instantiate (features[t]);
				feat.transform.parent = bear.transform;
				if (t == 1){
					feat.transform.Translate(-1.5f, -1.0f, 0.0f);
				}else{
					feat.transform.Translate(-1.0f, 1.5f, 0.0f);
				}
				bear.GetComponent<Baddy>().sides.Add("top", features[t]);
			}
				
				}
		bear.transform.position = spawnV;
	}

	//Makes a slime
	void MakeSlime(int feats)
	{
		/** I know it says bear instead of slime, but I was abusing copy paste and haven't
		 * bothered to fix it
		 * */
		Dictionary<int, int> taken = new Dictionary<int, int> ();
		GameObject bear = (GameObject) Instantiate (enemies [1]);
		int spawnX = Random.Range (-50, 50);
		int spawnY = 5;
		Vector3 spawnV = new Vector3 (spawnX, spawnY, 0);
		//print ("feats is " + feats);
		feats = 1;
		for (int f = 0; f < feats; f++) {
			
			int t = Random.Range(0, 3);;
			//t = 0;
			//Random rand = new Random();
			bool take = true;
			while(take){
				if(!taken.ContainsKey(t)){
					taken.Add(t, 1);
					take = false;
				}else{
					t = Random.Range(0, 3);
				}
			}
			//t = 0;
			Random rand = new Random();
			int type = Random.Range(0, 3);
			//type = 2;
			//print ("type is " + type);
			if (type < 1){
				
				GameObject feat = (GameObject) Instantiate (features[t]);
				feat.transform.parent = bear.transform;
				if (t == 1){
					feat.transform.Translate(0f, -1.2f, 0f);
				}else{
					feat.transform.Translate(-1.2f, 0f, 0f);
				}
				feat.transform.Rotate(0, 0, 90);
				bear.GetComponent<Baddy>().sides.Add("back", features[t]);
			}else if(type < 2){
				GameObject feat = (GameObject) Instantiate (features[t]);
				feat.transform.parent = bear.transform;
				if (t == 1){
					feat.transform.Translate(-.3f, 1.5f, 0f);
				}else{
					feat.transform.Translate(1.5f, -.3f, 0f);
				}
				feat.transform.Rotate(0, 0, 270);
				bear.GetComponent<Baddy>().sides.Add("front", features[t]);
				//Top
			}else if(type<3){
				GameObject feat = (GameObject) Instantiate (features[t]);
				feat.transform.parent = bear.transform;
				if (t == 1){
					feat.transform.Translate(-.2f, -1.0f, 0.0f);
				}else{
					feat.transform.Translate(-1.0f, 0f, 0.0f);
				}
				bear.GetComponent<Baddy>().sides.Add("top", features[t]);
			}
			
		}
		bear.transform.position = spawnV;
	}
	
}