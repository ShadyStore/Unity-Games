using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agro_range, action_range, jump_taimer;
    [SerializeField] float movespeed, hit_fors;
    [SerializeField] int movemode;
    [SerializeField] bool is_turn_Fase, is_Grounded;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bullet;
    Rigidbody2D rb2d;
    float dist_to_player,scale_x, scale_y;


    private void Start()
    {
        scale_x = transform.localScale.x;
        scale_y = transform.localScale.y;
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform;
        anim = this.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        
        if (CanSeePlayer(agro_range)||CanSeePlayerPopa(agro_range))
        {
            if (movemode == 0)
            {
                if (anim != null) anim.SetTrigger("Walk");
                ChasePlayer();
            }
            else if (movemode == 1)
            {
                ChasePlayerMode1();
            }
            else if (movemode == 2)
            {
                ChasePlayerMode2();
            }
        }
        else
        {
            //stop followin
            if(anim !=null) anim.SetTrigger("Idle");
            if (is_Grounded) StopChasePlayer(); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_Grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            is_Grounded = false;
        }
    }
        bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
        if (is_turn_Fase)
        {
            castDist = -distance;
        }
        Vector2 endPos = transform.GetChild(0).position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(transform.GetChild(0).position, endPos);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player")|| hit.collider.gameObject.CompareTag("Weapon"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(transform.GetChild(0).position, endPos, Color.red);
        }
        else { Debug.DrawLine(transform.GetChild(0).position, endPos, Color.green); }
        return val;
    }
    bool CanSeePlayerPopa(float distance)
    {
        bool val = false;
        float castDist = distance;
        if (is_turn_Fase)
        {
            castDist = -distance;
        }
        Vector2 endPos = transform.GetChild(1).position + Vector3.left * castDist;
        RaycastHit2D hit = Physics2D.Linecast(transform.GetChild(1).position, endPos);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("Weapon"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            Debug.DrawLine(transform.GetChild(1).position, endPos, Color.red);
        }
        else { Debug.DrawLine(transform.GetChild(1).position, endPos, Color.green); }
        return val;
    }
    void ChasePlayer()
    {
        dist_to_player = Vector2.Distance(transform.position, player.position);


        if (transform.position.x < player.transform.position.x)
        {
            is_turn_Fase = false;
            transform.localScale = new Vector2(scale_x, scale_y);
            if (dist_to_player < action_range)
            {
              if (rb2d.velocity.magnitude < movespeed * 2.3f)
            {
                rb2d.velocity += new Vector2(movespeed * 2 / 8, 0);
            }
            }           
            if (rb2d.velocity.magnitude < movespeed)
            {
                rb2d.velocity += new Vector2(movespeed / 8, 0);
            }

        }
        else if (transform.position.x > player.transform.position.x)
        {
            is_turn_Fase = true;
            transform.localScale = new Vector2(-scale_x, scale_y);

            if (dist_to_player < action_range)
            {
                if (rb2d.velocity.magnitude < movespeed*2.3f)
                {
                    rb2d.velocity += new Vector2(-movespeed * 2 / 8, 0);
                }
            }

            if (rb2d.velocity.magnitude < movespeed)
            {
                rb2d.velocity += new Vector2(-movespeed / 8, 0);
            }

        }
    }
    void ChasePlayerMode1()
    {
        dist_to_player = Vector2.Distance(transform.position, player.position);
        jump_taimer -= Time.deltaTime;
    
        
            if (transform.position.x < player.transform.position.x)
        {
            if (dist_to_player < action_range & jump_taimer <= 0)
            {
                Jump(2f);
            }
            is_turn_Fase = false;
            transform.localScale = new Vector2(scale_x, scale_y);
            if (rb2d.velocity.magnitude < movespeed)
            {
                rb2d.velocity += new Vector2(movespeed / 8, 0);
            }
            
        }
        else if (transform.position.x > player.transform.position.x)
        {
            if (dist_to_player < action_range & jump_taimer <= 0)
            {
                Jump(-2f);
            }
            is_turn_Fase = true;
            transform.localScale = new Vector2(-scale_x, scale_y);
            if (rb2d.velocity.magnitude < movespeed)
            {
                rb2d.velocity += new Vector2(-movespeed / 8, 0);
            }
            
        }
    }
    void ChasePlayerMode2()
    {
        dist_to_player = Vector2.Distance(transform.position, player.position);
        jump_taimer -= Time.deltaTime;

        if (transform.position.x < player.transform.position.x)
        {
            is_turn_Fase = false;
            transform.localScale = new Vector2(scale_x, scale_y);
            if (dist_to_player < action_range & jump_taimer <= 0)
            {
                jump_taimer = 0.5f;
                GameObject new_bullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90));
                new_bullet.GetComponent<Bullet>().speed = 0.1f;
                new_bullet.GetComponent<Bullet>().atak_power = gameObject.GetComponent<EnemyStats>().enemy.myatack_power;
            }
        }
        else if (transform.position.x > player.transform.position.x)
        {
            is_turn_Fase = true;
            transform.localScale = new Vector2(-scale_x, scale_y);
            if (dist_to_player < action_range & jump_taimer <= 0)
            {
                jump_taimer = 0.5f;
                GameObject new_bullet = Instantiate(bullet, transform.position, Quaternion.Euler(0,0,90));
                new_bullet.GetComponent<Bullet>().speed = -0.1f;
                new_bullet.GetComponent<Bullet>().atak_power = gameObject.GetComponent<EnemyStats>().enemy.myatack_power;
            }
        }
    }
    void Jump(float x)
    {
        rb2d.velocity += new Vector2(x, 8);
        jump_taimer = 5;
    }
    void StopChasePlayer()
    {
        
        rb2d.velocity = new Vector2(0, 0);
    }
    public void TakeAtack(float mod_fors)
    {
        if (rb2d != null)
        {
            if (transform.position.x < player.transform.position.x)
            {
                //rb2d.AddForce(new Vector2(-1, 1) * jump_fors, ForceMode2D.Impulse);
                rb2d.velocity += new Vector2(-hit_fors * mod_fors, 3);
            }
            else if (transform.position.x > player.transform.position.x)
            {
                // rb2d.AddForce(new Vector2(1, 1) * jump_fors, ForceMode2D.Impulse);
                rb2d.velocity += new Vector2(hit_fors * mod_fors, 3);
            }
        }
    }
}
