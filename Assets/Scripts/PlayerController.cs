using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    float x_input;
    float y_input;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Health_variables

    #endregion

    #region Unity_functions
    private void Awake() {
        /* TODO: Set HPSlider.value to a ratio between the 
            player's current health and maximum health. */
        PlayerRB = GetComponent<Rigidbody2D>();
        attackTimer = 0;
        anim = GetComponent<Animator>();
    }
    private void Update() {
        if (isAttacking)
        {
            return;
        }
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");
        Move();

        if(Input.GetKeyDown(KeyCode.J) && attackTimer <= 0)
        {
            Attack();
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Interact();
        }
        
    }
    #endregion

    #region Animation_components
    Animator anim;
    #endregion

    #region Attack_variables
    public float Damage;
    public float attackspeed = 1 ;
    float attackTimer;
    public float hitboxtiming;
    public float endanimationtiming;
    bool isAttacking;
    Vector2 currDirection;
    #endregion

    #region Attack_functions
    private void Attack()
    {

        Debug.Log("Attacking now");
        Debug.Log(currDirection);
        attackTimer = attackspeed;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        PlayerRB.velocity = Vector2.zero;
        anim.SetTrigger("Attack");

        FindObjectOfType<AudioManager>().Play("PlayerAttack");

        yield return new WaitForSeconds(hitboxtiming);
        Debug.Log("Casting hitbox now");
        //change this to a box cast in front of the player
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, Vector2.one, 0f, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Tons of Damage");
                /* TODO: Call TakeDamage() inside of the enemy's Enemy script using
                the "hit" reference variable */
            }
        }
        yield return new WaitForSeconds(hitboxtiming);
        isAttacking = false;
    }
    
    #endregion

    #region Movement_functions
    private void Move()
    {
        anim.SetBool("Moving", true);
        if (x_input > 0)
        {
            PlayerRB.velocity = Vector2.right;
            currDirection = Vector2.right;
            
        }
        else if (x_input < 0)
        {
            PlayerRB.velocity = Vector2.left;
            currDirection = Vector2.left;

        }
        else if (y_input > 0)
        {
            PlayerRB.velocity = Vector2.up;
            currDirection = Vector2.up;
        }
        else if (y_input < 0)
        {
            PlayerRB.velocity = Vector2.down;
            currDirection = Vector2.down;
        }
        else
        {
            PlayerRB.velocity = Vector2.zero;
            anim.SetBool("Moving", false);
        }

        anim.SetFloat("DirX", currDirection.x);
        anim.SetFloat("DirY", currDirection.y);

    }
    #endregion

    #region Health_functions

    public void TakeDamage(float value)
    {
        /* TODO: Adjust currHealth when the player takes damage
        IMPORTANT: What happens when the player's health reaches 0? */

        /* TODO: Update the value of HPSlider after the player's health changes. */
    }

    public void Heal(float value)
    {
        /* TODO: Adjust currHealth when the player heals
        IMPORTANT: What happens when the player's health surpasses their max health? Should currHealth be above maxHealth?*/

        /* TODO: Update the value of HPSlider after the player's health changes. */
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
    #endregion

    #region Interact_functions
    private void Interact()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, new Vector2(0.5f, 0.5f), 0f, Vector2.zero, 0f);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Chest"))
            {
                hit.transform.GetComponent<Chest>().Interact();
            }
        }
    }
    #endregion
}
