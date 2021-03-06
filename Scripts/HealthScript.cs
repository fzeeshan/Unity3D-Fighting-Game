﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour {

    public float health = 100f;
    private float x_death = -90f;
    private float death_smooth = 0.9f;
    private float rotate_time = 0.23f;
    private bool playerDied;

    public bool isPlayer;
    
    [SerializeField]
    private Image health_UI;

    void Update()
    {
        if(playerDied)
        {
            RotateDeath();
        }
    }

    public void ApplyDamage(float damage)
    {
        health = health - damage;

        if(health_UI != null)
        {
            health_UI.fillAmount = health / 100f;

        }

        if(health <= 0)
        {
            GetComponent<Animator>().enabled = false;
            StartCoroutine(AllowRotate());
            
            if(isPlayer)
            {
                GetComponent<PlayerMove>().enabled = false;
                GetComponent<PlayerAttackInput>().enabled = false;
                //yield return new WaitForSeconds(5);
                SceneManager.LoadScene(6);
            }
            else
            {
                GetComponent<EnemyController>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
                //yield return new WaitForSeconds(5);
                changeScene();
            }
        //    print("character died");
        }
    }

    private void changeScene()
    {
        if(SceneManager.GetActiveScene().buildIndex == 5)
        {
            SceneManager.LoadScene(7);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    public void RotateDeath()
    {
        transform.eulerAngles = new Vector3(
            Mathf.Lerp(transform.eulerAngles.x, x_death, Time.deltaTime * death_smooth),
            transform.eulerAngles.y, transform.eulerAngles.z);
    }

    IEnumerator AllowRotate()
    {
        playerDied = true;

        yield return new WaitForSeconds(rotate_time);

        playerDied = false;
    }
}





