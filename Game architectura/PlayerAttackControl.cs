using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttackControl : MonoBehaviour {

	public RandomizeEnemy enemy;
	public GameObject EnemyToDie;

	List<GameObject> listOfDedlineEnemys;
	bool gameOver;

	void Start(){
		listOfDedlineEnemys = new List<GameObject> ();
		gameOver = false;
	}

	void OnTriggerEnter(Collider collision){
		// If occurre collision with some enemy object do this
		if(collision.gameObject == EnemyToDie){
			PlayerAttack(EnemyToDie);
		}

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
				Application.LoadLevel(0);
			}
		}
		StopCoroutine("GameOver");
	}
}
