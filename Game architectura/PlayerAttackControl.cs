using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttackControl : MonoBehaviour {

	public RandomizeEnemy enemy;
	public GameObject EnemyToDie;
	int scoreCounter;

	List<GameObject> listOfDedlineEnemys;
	bool gameOver;

	void Start(){
		listOfDedlineEnemys = new List<GameObject> ();
		gameOver = false;
		scoreCounter = 0;
	}

	void OnTriggerEnter(Collider collision){
		// If occurre collision with some enemy object do this
		if(collision.gameObject == EnemyToDie){
			PlayerAttack(EnemyToDie);
			scoreCounter = scoreCounter +1;
		}
		// If enemy tuch player count 0.4 sec and if 
		// player dot kill the enemy or avoid collision
		// GAME OVER!!
		for (int i=0; i<enemy.listOfEnemys.Count; i++) {
			if(enemy.listOfEnemys[i] == collision.gameObject){
				gameOver = true;
				listOfDedlineEnemys.Add(collision.gameObject);
			}
		}

		if(gameOver){
			StartCoroutine("GameOver", 0.4f);
		}
	}

	void OnTriggerExit(Collider collision){
		listOfDedlineEnemys.Remove(collision.gameObject);
		if(listOfDedlineEnemys.Count == 0){
			gameOver = false;
			listOfDedlineEnemys.Clear();
		}
	}

	void PlayerAttack(GameObject obj){
		enemy.listOfEnemys.Remove(obj);
		listOfDedlineEnemys.Remove(obj);
		DieEnemy(obj);
	}
	
	void DieEnemy(GameObject obj){
		Destroy(obj);
	}

	IEnumerator GameOver(float waitTime){
		yield return new WaitForSeconds(waitTime);
		if(gameOver){
			if(listOfDedlineEnemys.Count != 0){
				print("GameOver");

				if(scoreCounter > PlayerPrefs.GetInt("SCORE"))
					PlayerPrefs.SetInt("SCORE", scoreCounter);
				PlayerPrefs.Save();

				print(PlayerPrefs.GetInt("SCORE").ToString());
				Application.LoadLevel(0);
			}
		}
		StopCoroutine("GameOver");
	}	
}
