﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DefaultEnemyScript : MonoBehaviour
{
    [SerializeField]
    GameObject aux;
    GameObject player;

    Animator anim;

    float angle;
    float health;
    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        health = 5;
    }
	void Update ()
    {
        this.GetComponent<PolyNavAgent>().SetDestination(player.transform.position);
        ChangeRotation();
        if(health <= 0)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }

	}
    void ChangeRotation()
    {
        angle = aux.transform.eulerAngles.z;
        if (angle > 157.5 && angle < 202.5)
        {
            anim.SetFloat("EnemyWalkingFloat", 0);
        }
        else if (angle > 247.5 && angle < 292.5)
        {
            anim.SetFloat("EnemyWalkingFloat", 3);
        }
        else if (angle > 67.5 && angle < 112.5)
        {
           anim.SetFloat("EnemyWalkingFloat", 1);
        }
        else if (angle >= 22.5 && angle <= 67.5)
        {
            anim.SetFloat("EnemyWalkingFloat", 4);
        }
        else if (angle >= 292.5 && angle <= 337.5)
        {
            anim.SetFloat("EnemyWalkingFloat", 5);
        }
        else if (angle >= 112.5 && angle <= 157.5)
        {
            anim.SetFloat("EnemyWalkingFloat", 6);
        }
        else if (angle >= 202.5 && angle <= 247.5)
        {
            anim.SetFloat("EnemyWalkingFloat", 7);
        }
        else if (angle < 22.5 || angle > 337.5)
        {
            anim.SetFloat("EnemyWalkingFloat", 2);
        }
    }
    public void Damaged()
    {
        health--;
        StartCoroutine(Shot());
    }
    IEnumerator Shot()
    {
        anim.SetBool("Shot", true);
        int aux = Convert.ToInt32(anim.GetFloat("EnemyWalkingFloat"));
        Vector3 pos = player.transform.position;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ChangeTimeScale());
        StartCoroutine(ChangeEnemyColor());
        StartCoroutine(GoingBackWhenShot(aux, pos));
    }
    IEnumerator ChangeEnemyColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(0.3f, 0, 0);
        yield return new WaitForSeconds(0.05f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        yield return 0;
    }
    IEnumerator ChangeTimeScale()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.03f);;
        Time.timeScale = 1;
    }
    IEnumerator GoingBackWhenShot(int aux, Vector3 position)
    {
        for (float i = 0; i < 10; i++)
        {
            switch (aux)
            {
                case 0:
                    transform.position += new Vector3(0f, 0.1f, 0f);
                    if (position.x < transform.position.x)
                        anim.SetFloat("EnemyShotFloat", 1);
                    else
                        anim.SetFloat("EnemyShotFloat", 0);
                    break;
                case 1:
                    transform.position += new Vector3(0.1f, 0f, 0f);
                        anim.SetFloat("EnemyShotFloat", 1);                       
                    break;
                case 2:
                    transform.position += new Vector3(0f, -0.1f, 0f);
                    if (position.x < transform.position.x)
                        anim.SetFloat("EnemyShotFloat", 1);
                    else
                        anim.SetFloat("EnemyShotFloat", 0);
                    break;
                case 3:
                    transform.position += new Vector3(-0.1f, 0f, 0f);
                        anim.SetFloat("EnemyShotFloat", 0);
                    break;
                case 4:
                    transform.position += new Vector3(0.1f, -0.1f, 0f);
                    anim.SetFloat("EnemyShotFloat", 1);
                    break;
                case 5:
                    transform.position += new Vector3(-0.1f, -0.1f, 0f);
                    anim.SetFloat("EnemyShotFloat", 0);
                    break;
                case 6:
                    transform.position += new Vector3(0.1f, 0.1f, 0f);
                    anim.SetFloat("EnemyShotFloat", 1);
                    break;
                case 7:
                    transform.position += new Vector3(-0.1f, 0.1f, 0f);
                    anim.SetFloat("EnemyShotFloat", 0);
                    break;
            }
            yield return new WaitForSeconds(0.001f);
        }
        anim.SetBool("Shot", false);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag.Equals("Player"))
        {
            Debug.Log("Tomou dano");
        }
    }
}
