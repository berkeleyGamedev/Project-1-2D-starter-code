using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Movement_variables
    public float moveSpeed;
    #endregion

    #region Targeting_variables
    public Transform player;
    #endregion

    #region Health_variables
    public float maxHealth;
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
    }

    private void Update() {
        /* TODO 2.1: Call Move() if player is !null */
    }
    #endregion

    #region Movement_functions
    private void Move()
    { 
        /* TODO 2.1: Move the enemy towards the player */
    }
    #endregion

    #region Attack_functions
    private void Explode()
    {
        /* TODO 2.2: Explode should deal damage to the player if within explosion radius. To simulate a explosion, 
            the enemy game object should be destroyed and spawn a explosion animation in its place. 
            IMPORTANT: Destroy() should be the LAST function executed. Once a game object is destroyed, it will not execute any code beyond that line. */

        /* TODO 3.2: Call TakeDamage() inside of the player's PlayerController script using
            the "hit" reference variable. */
    }

    private void OnCollisionEnter2D(Collision2D other) {
       /* TODO 2.3: Call Explode() if enemy comes in contact with player */
    }
    #endregion


    #region Health_functions
    public void TakeDamage(float value)
    {
       /* TODO 3.1: Adjust currHealth when the enemy takes damage
        IMPORTANT: What happens when the enemy's health reaches 0? */
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion

}