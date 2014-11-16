using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyControl : MonoBehaviour {
	public RandomizeEnemy enemyControl;
	public GameObject player;

	float playerDistance;
	bool gameOver;

	void Awake(){
		player = GameObject.Find("TheRunner");
		enemyControl = GameObject.Find("Main Camera").GetComponent<RandomizeEnemy>();
	}

	void OnMouseDown(){
		enemyControl.EnemyToDie = gameObject;
	}

	void PlayerAttack(GameObject obj){
		enemyControl.enemyCounter --;
		enemyControl.listOfEnemys.Remove(obj);
		DieEnemy(obj);
	}
	
	void DieEnemy(GameObject obj){
		print("die enemy");
		Destroy(obj);
		enemyControl.scoreCounter = enemyControl.scoreCounter +1;
		enemyControl.score_Label.GetComponent<UILabel> ().text = "" + enemyControl.scoreCounter;
	}

	IEnumerator GameOver(float waitTime){
		yield return new WaitForSeconds(waitTime);
		if(gameOver){
			print("GameOver");
			if(!enemyControl.deadPlayer){
				enemyControl.GameOver_PopUp.SetActive(true);
				enemyControl.GameOver_PopUp.animation.Play ("PopUp_Open");
				enemyControl.deadPlayer = true;
			}
			
			if(enemyControl.scoreCounter > PlayerPrefs.GetInt("SCORE"))
				PlayerPrefs.SetInt("SCORE", enemyControl.scoreCounter);
			PlayerPrefs.Save();
			print(PlayerPrefs.GetInt("SCORE").ToString());
		}
		StopCoroutine("GameOver");
	}	

	void Update(){
		transform.position = Vector3.MoveTowards(
			transform.position, player.transform.position, 0.4f * Time.deltaTime);		
		transform.LookAt(player.transform.position);

		playerDistance = Vector3.Distance(player.transform.position, transform.position);
		if(playerDistance < 0.35f){
			if(enemyControl.EnemyToDie != gameObject){
				gameOver = true;
				StartCoroutine("GameOver", 0.6f);
			}
			else{
				PlayerAttack(gameObject);
				gameOver = false;
			}
		}
		else{
			gameOver = false;
		}
	}
}
