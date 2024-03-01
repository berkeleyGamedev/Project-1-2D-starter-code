using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Movement_variables
    public float movespeed;
    private float newMovespeed;
    float x_input;
    float y_input;
    #endregion

    #region Physics_components
    Rigidbody2D PlayerRB;
    #endregion

    #region Health_variables
    public float maxHealth;
    float currHealth;
    public Slider HPSlider;
    #endregion

    #region Stam_variables
    public float maxStam;
    public float maxSprintDelay;
    float currStam;
    public Slider StamSlider;
    float sprintDelay;
    #endregion

    #region Unity_functions
    private void Awake() {
        PlayerRB = GetComponent<Rigidbody2D>();
        attackTimer = 0;
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
        currStam = maxStam;
        HPSlider.value = currHealth / maxHealth;
        StamSlider.value = currStam / maxStam;
        sprintDelay = 0;
    }
    private void Update() {
        if (isAttacking)
        {
            return;
        }
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");
        move();

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
        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currDirection, Vector2.one, 0f, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if(hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Tons of Damage");
                hit.transform.GetComponent<Enemy>().TakeDamage(Damage);
            }
        }
        yield return new WaitForSeconds(hitboxtiming);
        isAttacking = false;
    }
    
    #endregion

    #region Movement_functions
    private void move()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currStam > 0.1 && sprintDelay <= 0)  
        {
            newMovespeed = movespeed * 2;
            currStam -= Time.deltaTime;
            sprintDelay -= Time.deltaTime;
            StamSlider.value = currStam / maxStam;
        }
        else
        {
            // Check if the current stamina is less than 0.5, if so, put it on cooldown
            if (currStam < 0.1)
            {
                sprintDelay = maxSprintDelay;
            }


            // subtract cooldown
            sprintDelay -= Time.deltaTime;


            newMovespeed = movespeed;
            currStam += Time.deltaTime;

            // Check if the current stamina is more than the max stamina, if so, set it to max stamina
            if (currStam > maxStam)
            {
                currStam = maxStam;
            }
            StamSlider.value = currStam / maxStam;
        }

        
        anim.SetBool("Moving", true);
        if (x_input > 0)
        {
            PlayerRB.velocity = Vector2.right * newMovespeed;
            currDirection = Vector2.right;
            
        }
        else if (x_input < 0)
        {
            PlayerRB.velocity = Vector2.left * newMovespeed;
            currDirection = Vector2.left;

        }
        else if (y_input > 0)
        {
            PlayerRB.velocity = Vector2.up * newMovespeed;
            currDirection = Vector2.up;
        }
        else if (y_input < 0)
        {
            PlayerRB.velocity = Vector2.down * newMovespeed;
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
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        currHealth -= value;
        Debug.Log("health is now " + currHealth.ToString());
        HPSlider.value = currHealth / maxHealth;
        if (currHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float value)
    {
        currHealth += value;
        currHealth = Mathf.Min(currHealth, maxHealth);
        HPSlider.value = currHealth / maxHealth;
        Debug.Log("health is now " + currHealth.ToString());
    }

    public void Die()
    {
        FindObjectOfType<AudioManager>().Play("PlayerDeath");
        Destroy(this.gameObject);

        GameObject gm = GameObject.FindWithTag("GameController");
        gm.GetComponent<GameManager>().LoseGame();
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


    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
