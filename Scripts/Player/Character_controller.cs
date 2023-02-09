using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character_controller : MonoBehaviour
{
    [SerializeField] public float speed, jump_fors, rb_drag, rb_gravity;
    public Rigidbody2D pers_rb;
    
    [SerializeField] private bool is_Grounded, on_whater, on_stair;
    public bool have_mel, pause;
    [SerializeField] private Animator anim;
    public int atack_Power;
    [SerializeField] public float atack_time;
    public GameObject weapon, hud_panel;
    void Start()
    {
        have_mel = false;
        anim = this.GetComponent<Animator>();
        pers_rb = GetComponent<Rigidbody2D>();
        rb_drag = pers_rb.drag;
        rb_gravity = pers_rb.gravityScale;
        speed = 4;
        jump_fors = 60;
        hud_panel = GameObject.FindGameObjectWithTag("Hud").GetComponent<HealthBar>().panel;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&is_Grounded)
        {
            //Jump
            if (is_Grounded) anim.SetTrigger("Jump");
            pers_rb.AddForce(Vector2.up * jump_fors, ForceMode2D.Impulse);
            
        }
        if (Input.GetMouseButtonDown(0))
        {
            //Atack
            Atack();
            weapon.GetComponent<WeaponAtack>().atack_time = 0.5f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (is_Grounded) anim.SetTrigger("Walk");
        }
        if(Input.GetKey(KeyCode.D))
        {
            if (is_Grounded) anim.SetTrigger("Walk");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }
    void FixedUpdate()
    {
        MoveLeft_Right();
        Stoper();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject body = collision.gameObject;
        Rigidbody2D bodyrigidbody = body.GetComponent<Rigidbody2D>();
        if (body.CompareTag("Enemy"))
        {
            if (bodyrigidbody != null)
            {
                bodyrigidbody.velocity = Vector3.zero;
                bodyrigidbody.angularVelocity = 0;
            }
            body.GetComponent<EnemyAI>().TakeAtack(0.5f);
            int enemyAtack = body.GetComponent<EnemyStats>().enemy.myatack_power;
            GetComponent<PlayerStats>().PersTakeDamadge(enemyAtack);

        }
       
        if (body.CompareTag("Drop"))
        {
            DropParametrs drop_par = body.GetComponent<DropParametrs>();
            if (drop_par.mel && drop_par.can_take)
            {
                have_mel = true;
                Destroy(body.gameObject);
            }
            if (drop_par.health_potion && drop_par.can_take)
            {
                GetComponent<PlayerStats>().PersTakeHeal(drop_par.health_val);
                Destroy(body.gameObject);
            }
        }
    }
    // логика столкновений и получения урона лаз по лестнице
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject body = collision.gameObject;

        if (body.CompareTag("Stairs"))
        {
            on_stair = true;
            if (!is_Grounded)
            {
                pers_rb.drag = 15;
                pers_rb.gravityScale = 0;
            }
        }
        if (body.CompareTag("Bullet"))
        {

            int enemyAtack = body.GetComponent<Bullet>().atak_power;
            GetComponent<PlayerStats>().PersTakeDamadge(enemyAtack);
            DestroyObject(body);

        }
        if (body.CompareTag("Ground"))
        {
            is_Grounded = true;
        }
        if (body.CompareTag("Whater"))
        {
            on_whater = true;
        }
        
            if (body.CompareTag("Portal")&&have_mel)
        {
            GetComponent<PlayerStats>().game_lvl += 1;
            GetComponent<PlayerStats>().SavePlayerToData();
            GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>().SaveData();
            SceneManager.LoadScene("lvl_" + GetComponent<PlayerStats>().game_lvl);
            Debug.Log("Сохранил уровень " + GetComponent<PlayerStats>().game_lvl);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
        {
            is_Grounded = false;
        }
        if (collision.gameObject.CompareTag("Whater"))
        {
            on_whater = false;
        }
        if (collision.gameObject.CompareTag("Stairs"))
        {
            on_stair = false;
            pers_rb.drag = rb_drag;
            pers_rb.gravityScale = rb_gravity;
        }
        }
    void Pause()
    {
        if (!pause)
        {
            pause = true;
            hud_panel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause = false;
            hud_panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void Pause_btn()
    {
        Pause();
    }
    public void Restart_btn()
    {
        SceneManager.LoadScene("lvl_" + GetComponent<PlayerStats>().game_lvl);
        Time.timeScale = 1;
    }
    void Atack()
    {
        anim.Play("Atack");
    }

    void MoveLeft_Right()
    {
        if (Input.GetKey(KeyCode.A))
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.GetChild(0).localScale = new Vector2(1, 1);
            }
            else { transform.GetChild(0).localScale = new Vector2(-1, 1); }
            // pers_rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            if (pers_rb.velocity.magnitude < speed)
            {
                pers_rb.velocity += new Vector2(-speed/4, 0);
            }
            
        }
        if (Input.GetKey(KeyCode.D))
        {
           
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.GetChild(0).localScale = new Vector2(-1, 1);
            }
            else { transform.GetChild(0).localScale = new Vector2(1, 1); }
            if (pers_rb.velocity.magnitude < speed)
            {
                pers_rb.velocity += new Vector2(speed / 4, 0);
            }
            // pers_rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.W)&& (on_stair || on_whater))
        {
            pers_rb.velocity = new Vector2(0, speed);
        }
        if (Input.GetKey(KeyCode.S) && (on_stair || on_whater))
        {
            pers_rb.velocity = new Vector2(0, -speed);
            //pers_rb.AddForce(Vector2.down * 2 * speed, ForceMode2D.Impulse);
        }
    }
    void Stoper()
    {

        if (Input.GetKeyUp(KeyCode.A) && is_Grounded)
        {
            pers_rb.velocity = Vector3.zero;
            pers_rb.angularVelocity = 0;
        }
        if (Input.GetKeyUp(KeyCode.D) && is_Grounded)
        {
            pers_rb.velocity = Vector3.zero;
            pers_rb.angularVelocity = 0;
        }

    }
}
