using UnityEngine;	
using System.Collections;
using System.Collections.Generic;

public class RandomizeEnemy : MonoBehaviour {

	public GameObject enemy, player;
	public int enemyCounter, enemyLimit;

	float xPos, zPos;
	GameObject clone;
	List<GameObject> listOfEnemys;

	// Use this for initialization
	void Start () {
		clone = new GameObject ("Clone");
		enemyLimit = 12;
		listOfEnemys = new List<GameObject> ();
		StartCoroutine ("Randomize", 2f);
	}

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

	void AttackPlayer(){
		for (int i=0; i<listOfEnemys.Count; i++) {
			listOfEnemys[i].transform.position = Vector3.MoveTowards(
				listOfEnemys[i].transform.position, player.transform.position, 0.4f * Time.deltaTime);
		}
	}

	// Update is called once per frame
	void Update () {
		Checkpoints ();
		AttackPlayer ();
	}
}
