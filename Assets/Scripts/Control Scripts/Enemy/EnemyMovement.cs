using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    private GameObject m_Player;
    private EnemyBattle m_BattleControl;


	// Use this for initialization
	void Start () {
        m_Player = GameObject.Find("Player");

    }

    void FixedUpdate() {
        if (GetComponent<EnemyManager>().IsAlive() && !GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOver() ) {
            // Follow player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_Player.transform.position - transform.position), GetComponent<EnemyManager>().GetTurnSpeed() * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));
            transform.position += transform.forward * GetComponent<EnemyManager>().GetMoveSpeed() * Time.deltaTime;
        }
    }


    public void Knockback() {
        transform.Translate(new Vector3(0, 0, -GetComponent<EnemyManager>().GetKnockbackDist()));
    }




}
