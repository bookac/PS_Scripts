using UnityEngine;
using System.Collections.Generic;

public class PlayerAttackControl : MonoBehaviour {

	public RandomizeEnemy enemy;
	public GameObject EnemyToDie;

	void OnTriggerEnter(Collider collision){
		// If occurre collision with some enemy object do this
		if(collision.gameObject == EnemyToDie){
			PlayerAttack(EnemyToDie);
		}
	}

	void PlayerAttack(GameObject obj){
		enemy.listOfEnemys.Remove(obj);
		enemy.listOfCollidedEnemys.Remove(obj);
		enemy.listOfDedlineEnemys.Remove(obj);
		DieEnemy(obj);
	}
	
	void DieEnemy(GameObject obj){
		Destroy(obj);
	}
}
