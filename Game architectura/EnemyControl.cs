using UnityEngine;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {

	public PlayerAttackControl attackEnemy;
	public bool haveCollision;

	void Awake(){
		attackEnemy = GameObject.Find("TheRunner").GetComponent<PlayerAttackControl>();
		haveCollision = false;
	}

	void OnTriggerEnter(Collider collision){
		haveCollision = true;
	}

	void OnTriggerExit(Collider collision){
		haveCollision = false;			
	}

	void OnMouseDown(){
		attackEnemy.EnemyToDie = gameObject;
	}
}
