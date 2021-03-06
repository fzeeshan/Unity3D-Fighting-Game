﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour {

    public GameObject attack_point;

    private CharacterAnimations enem_Anim;
    private NavMeshAgent navAgent;

    private Transform playerTarget;
    public float move_speed = 50f;
    public float attack_distance = 50f;
    public float chase_after_Attack_distance = 1f;
    private float wait_before_attack_time = 1f;
    private float attack_timer;
    private EnemyState enemy_state;

    // Use this for initialization
    void Awake() {
        enem_Anim = GetComponent<CharacterAnimations>();
        navAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Start() {
        enemy_state = EnemyState.CHASE;
        attack_timer = wait_before_attack_time;
    }
    

	// Update is called once per frame
	void Update () {
		if(enemy_state == EnemyState.CHASE)
        {
            ChasePlayer();
        }
        if(enemy_state == EnemyState.ATTACK)
        {
            AttackPlayer();
        }
	}

    void ChasePlayer()
    {
//        navAgent.destination = playerTarget.transform.position * Time.deltaTime;
        navAgent.SetDestination(playerTarget.transform.position);
        navAgent.speed = move_speed;

        if(navAgent.velocity.sqrMagnitude == 0f)
        {
       //     enemy_state = EnemyState.ATTACK;
            enem_Anim.Walk(false);
        }
        else
        {
            enem_Anim.Walk(true);
        }
        attack_distance = 35f;
       // print(Vector3.Distance(transform.position, playerTarget.transform.position) + "  helooo beasns  " + attack_distance);
        if (Vector3.Distance(transform.position, playerTarget.transform.position) <= attack_distance)
        {
            enemy_state = EnemyState.ATTACK;
        }
    }
    
    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        enem_Anim.Walk(false);

        attack_timer = Time.deltaTime + 1;
        print(attack_timer + "   " + wait_before_attack_time);
        if (attack_timer > wait_before_attack_time)
        {
            //print("YAHAN ata hi nai");
            if (Random.Range(0,2) > 0)
            {
                enem_Anim.Attack_0();
            }
            else
            {
                enem_Anim.Attack_1();
            }
            attack_timer = 0f;
        }
        if(Vector3.Distance(transform.position, playerTarget.transform.position) > attack_distance + chase_after_Attack_distance)
        {
            navAgent.isStopped = false;
            enemy_state = EnemyState.CHASE;
        }
    }

    void Activate_attack_point()
    {
        attack_point.SetActive(true);
    }

    void DeActivate_attack_point()
    {
        if (attack_point.activeInHierarchy)
        {
            attack_point.SetActive(false);
        }
    }
}
