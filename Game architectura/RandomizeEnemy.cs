using UnityEngine;	
using System.Collections;
using System.Collections.Generic;

public class RandomizeEnemy : MonoBehaviour {
	#region PUBLIC OBJECTS
	public GameObject enemy, player;
	public int enemyCounter, enemyLimit;
	public List<GameObject> listOfEnemys, listOfCollidedEnemys, listOfDedlineEnemys;
	#endregion

	#region LOCAL PRIVATE OBJECTS
	float xPos, zPos, timeTolerance, startCountdown;
	bool GameOver;
	GameObject clone;
	#endregion

	// Use this for initialization
	void Start () {
		clone = new GameObject ("Clone");
		enemyLimit = 12; timeTolerance = 0.3f; GameOver = false; startCountdown = 0f;
		listOfEnemys = new List<GameObject> ();
		listOfCollidedEnemys = new List<GameObject> ();
		listOfDedlineEnemys = new List<GameObject> ();
		StartCoroutine ("Randomize", 2f);
	}

	#region ENEMY AI
	Vector3 RandomGenerator(){
		xPos = Random.Range (-5f,5f);
		zPos = Random.Range (-5f,5f);
		return new Vector3 (xPos, 0, zPos);
	}

	IEnumerator Randomize(float waitTime) {
		yield return new WaitForSeconds(waitTime);

		GameObject enemyClone = Instantiate (enemy, RandomGenerator(), Quaternion.LookRotation(player.transform.position)) as GameObject;
		enemyClone.transform.LookAt (player.transform.position);
		enemyClone.transform.parent = clone.transform;
		enemyClone.name = enemyClone.name + enemyCounter.ToString();
		enemyClone.GetComponent<BoxCollider>().isTrigger = true;
		enemyClone.AddComponent<Rigidbody>();
		enemyClone.GetComponent<Rigidbody>().useGravity = false;
		enemyClone.GetComponent<Rigidbody>().isKinematic = false;
		enemyClone.AddComponent<EnemyControl>();
		listOfEnemys.Add (enemyClone);
		enemyCounter++;

		if(enemyCounter < enemyLimit)
			StartCoroutine ("Randomize", 5f);
	}

	void Checkpoints(){
		if (Time.time % 100 == 0) {
			if(Time.time != 0)
				enemyLimit += 5;
		}
	}

	void EnemyAttackPlayer(){
		for (int i=0; i<listOfEnemys.Count; i++) {
			if(listOfEnemys[i].GetComponent<EnemyControl>().haveCollision){
				listOfCollidedEnemys.Add(listOfEnemys[i]);

				if(i%2 == 0)
					listOfEnemys[i].collider.attachedRigidbody.velocity = new Vector3(0.5f,0,0);
				else
					listOfEnemys[i].collider.attachedRigidbody.velocity = new Vector3(-0.5f,0,0);

			}
			else{
				listOfCollidedEnemys.Remove(listOfEnemys[i]);
				listOfEnemys[i].collider.attachedRigidbody.velocity = new Vector3(0,0,0);
				listOfEnemys[i].transform.position = Vector3.MoveTowards(
					listOfEnemys[i].transform.position, player.transform.position, 0.4f * Time.deltaTime);

				listOfEnemys[i].transform.LookAt(player.transform.position);
			}
		}
	}

	void CorrectEnemyCollision(){
		for (int i=0; i<listOfCollidedEnemys.Count; i++) {
			try{
				if(i%2 == 0)
					listOfCollidedEnemys[i].collider.attachedRigidbody.velocity = new Vector3(0.5f,0,0);
				else
					listOfCollidedEnemys[i].collider.attachedRigidbody.velocity = new Vector3(-0.5f,0,0);
			}catch{}
		}
	}

	void OnCollisionEnter(Collision collision){
		print("enter = " + collision.gameObject.name);
		for (int i=0; i<listOfEnemys.Count; i++) {
			if(listOfEnemys[i] == collision.gameObject){
				GameOver = true; startCountdown = Time.time;
				listOfDedlineEnemys.Add(collision.gameObject);
			}
		}
	}

	void OnCollisionExit(Collision collision){
		for (int i=0; i<listOfDedlineEnemys.Count; i++) {
			if(listOfDedlineEnemys[i] == collision.gameObject){
				listOfDedlineEnemys.Remove(listOfDedlineEnemys[i]);
			}
		}
		print("eneme count = " + listOfDedlineEnemys.Count);
		if(listOfDedlineEnemys.Count == 0){
			GameOver = false;			
		}
	}
	#endregion

	// Update is called once per frame
	void Update () {
		Checkpoints ();
		EnemyAttackPlayer ();
		CorrectEnemyCollision();

		if(GameOver){
			if((Time.time - startCountdown) > timeTolerance){
				print("GameOver");
			}
		}
	}
}
