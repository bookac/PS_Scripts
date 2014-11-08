using UnityEngine;
using System.Collections.Generic;

public class PlayerAttackControl : MonoBehaviour {

	public RandomizeEnemy enemy;
	public List<GameObject> EnemysToDie;

	void Start(){
		EnemysToDie = new List<GameObject>();
	}

	void OnTriggerEnter(Collider collision){
		int enemyCounter = 0;
		while (enemyCounter < EnemysToDie.Count){
			// If occurre collision with some enemy object do this
			if(collision.gameObject == EnemysToDie[enemyCounter]){
				PlayerAttack(EnemysToDie[enemyCounter]);
			}
			else enemyCounter++;
		}
	}

	void PlayerAttack(GameObject obj){
		EnemysToDie.Remove(obj);
		enemy.listOfEnemys.Remove(obj);
		Die(obj);
	}
	
	void Die(GameObject obj){
		Destroy(obj);
	}
}
