using UnityEngine;	
using System.Collections;
using System.Collections.Generic;

public class RandomizeEnemy : MonoBehaviour {
	#region PUBLIC OBJECTS
	public GameObject enemy, player;
	public int enemyCounter, enemyLimit;
	public List<GameObject> listOfEnemys;
	#endregion

	#region LOCAL PRIVATE OBJECTS
	float xPos, zPos;
	GameObject clone;
	#endregion

	// Use this for initialization
	void Start () {
		clone = new GameObject ("Clone");
		enemyLimit = 12;
		listOfEnemys = new List<GameObject> ();
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
			listOfEnemys[i].transform.position = Vector3.MoveTowards(
				listOfEnemys[i].transform.position, player.transform.position, 0.4f * Time.deltaTime);

			listOfEnemys[i].transform.LookAt(player.transform.position);
		}
	}
	#endregion

	// Update is called once per frame
	void Update () {
		Checkpoints ();
		EnemyAttackPlayer ();
	}
}
