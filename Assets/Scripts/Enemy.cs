using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Movement_variables
    public float movespeed;
    #endregion

    #region Targeting_variables
    public Transform player;
    #endregion

    #region Health_variables
    public float MaxHealth;
    float currhealth;
    #endregion

    #region Attack_variables
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionObject;
    #endregion

    #region Physics_components
    Rigidbody2D EnemyRB;
    #endregion

    #region Health_functions
    public void TakeDamage(float value)
    {
        FindObjectOfType<AudioManager>().Play("BatHurt");
        currhealth -= value;
        Debug.Log("Enemy health is now " + currhealth.ToString());

        if(currhealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion

    #region Unity_functions

    private void Awake() {
        EnemyRB = GetComponent<Rigidbody2D>();
        currhealth = MaxHealth;
    }

    private void Update() {
        if (player == null)
        {
            return;
        }
        Move();
    }
    #endregion

    #region Movement_functions
    private void Move()
    { 
        Vector2 direction = player.position - transform.position;

        EnemyRB.velocity = direction.normalized * movespeed;

    }
    #endregion

    #region Attack_functions
    private void Explode()
    {
        FindObjectOfType<AudioManager>().Play("Explosion");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("ouch explosion");

                Instantiate(explosionObject, transform.position, transform.rotation);
                hit.transform.GetComponent<PlayerController>().TakeDamage(explosionDamage);
                Destroy(this.gameObject);
            }
        }
        Destroy(this.gameObject);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Player"))
        {
            Explode();
        }
    }
    #endregion
}