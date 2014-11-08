using UnityEngine;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {

	public PlayerAttackControl attackEnemy;

	void Awake(){
		attackEnemy = GameObject.Find("TheRunner").GetComponent<PlayerAttackControl>();
	}
	
	void OnMouseDown(){
		attackEnemy.EnemysToDie.Add(gameObject);
	}
}
