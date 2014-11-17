using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomizeEnemy : MonoBehaviour {
	#region PUBLIC OBJECTS
	public GameObject enemy, player, GameOver_PopUp, score_Label;
	public GameObject EnemyToDie; // Init in EnemyControl class.

	public int enemyCounter, enemyLimit, scoreCounter;
	public bool PlayerIsDead{get; set;}

	public List<GameObject> listOfEnemys;
	#endregion

	#region LOCAL PRIVATE OBJECTS
	int realCounter;
	float xPos, zPos, xNegPos, zNegPos;
	GameObject clone;
	#endregion

	// Use this for initialization
	void Start () {
		clone = new GameObject ("Clone");
		PlayerIsDead = false;
		enemyLimit = 30;
		listOfEnemys = new List<GameObject> ();

		scoreCounter = 0; // count kills
		score_Label.GetComponent<UILabel> ().text = scoreCounter.ToString(); // display number of dead enemys

		StartCoroutine ("Randomize", 2f);
		StartCoroutine ("Checkpoints", 100f);
	}

	#region ENEMY AI
	Vector3 RandomGenerator(){
		int rand = Random.Range(0,2);
		xPos = Random.Range (2f + player.transform.position.x, 5f);
		zPos = Random.Range (3f + player.transform.position.x,5f);
		xNegPos = Random.Range (-5f, player.transform.position.x - 2f);
		zNegPos = Random.Range (-5f, player.transform.position.x - 3f);

		if(rand == 0) return new Vector3 (xPos, 0, zPos);
		else return new Vector3 (xNegPos, 0, zNegPos);
	}

	IEnumerator Randomize(float waitTime) {
		yield return new WaitForSeconds(waitTime);

		GameObject enemyClone = Instantiate (enemy, RandomGenerator(), Quaternion.LookRotation(player.transform.position)) as GameObject;
		enemyClone.transform.LookAt (player.transform.position);
		enemyClone.transform.parent = clone.transform;
		enemyClone.name = enemyClone.name + realCounter.ToString();
		enemyClone.AddComponent<EnemyControl>();
		listOfEnemys.Add (enemyClone);
		enemyCounter++; realCounter++;

		if(enemyCounter < enemyLimit)
			StartCoroutine ("Randomize", 3f);
	}

	IEnumerator Checkpoints(float waitTime){
		yield return new WaitForSeconds(waitTime);
		enemyLimit += 5;
	}
	#endregion
}