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
    #region Unity_functions

    private void Awake() {
        EnemyRB = GetComponent<Rigidbody2D>();
        currhealth = MaxHealth;
    }

    private void Update() {
        /* TODO: Call Move() if player is !null */
    }
    #endregion

    #region Movement_functions
    private void Move()
    { 
        /* TODO: Move the enemy towards the player */
    }
    #endregion

    #region Attack_functions
    private void Explode()
    {
        /* TODO: Explode should deal damage to the player if within explosion radius. To simulate a explosion, 
            the enemy game object should be destroyed and spawn a explosion animation in its place. 
            IMPORTANT: Destroy() should be the LAST function executed. Once a game object is destroyed, it will not execute any code beyond that line. */

        /* TODO: Call TakeDamage() inside of the player's PlayerController script using
            the "hit" reference variable. */
    }

    private void OnCollisionEnter2D(Collision2D other) {
       /* TODO: Call Explode() if enemy comes in contact with player */
    }
    #endregion


    #region Health_functions
    public void TakeDamage(float value)
    {
       /* TODO: Adjust currHealth when the enemy takes damage
        IMPORTANT: What happens when the enemy's health reaches 0? */
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion

}