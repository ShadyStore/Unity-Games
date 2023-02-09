using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAtack : MonoBehaviour
{

    [SerializeField] public float atack_time;
    public GameObject w_player;
    private void Start()
    {
        w_player = transform.root.gameObject;
        atack_time = w_player.GetComponent<Character_controller>().atack_time;
    }
    private void Update()
    {

        if (atack_time > 0)
        {
            atack_time -= Time.deltaTime;
        }
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.CompareTag("Enemy") && atack_time > 0)
         {
             Debug.Log("Dem");
             int playerDam = w_player.GetComponent<PlayerStats>().player.myatack_power;
             collision.gameObject.GetComponent<EnemyStats>().enemy.TakeDamadge(playerDam);
             collision.gameObject.GetComponent<EnemyStats>().enemy.Debugger();
             Debug.Log("Enemy");
             atack_time = 0;
         }
     }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && atack_time > 0) {
            GameObject body = collision.gameObject;
            Rigidbody2D bodyrigidbody = body.GetComponent<Rigidbody2D>();
            Debug.Log("Dem");
        int playerDam = w_player.GetComponent<PlayerStats>().player.myatack_power;
        playerDam = Mathf.RoundToInt(playerDam * Random.Range(0.9f, 1.1f));
            body.GetComponent<EnemyStats>().enemy.TakeDamadge(playerDam);
            body.GetComponent<EnemyStats>().enemy.Debugger();
            if (bodyrigidbody != null)
            {
                bodyrigidbody.velocity = Vector3.zero;
                bodyrigidbody.angularVelocity = 0;
            }
            body.GetComponent<EnemyAI>().TakeAtack(1);
        Debug.Log("Enemy");
        atack_time = 0;
    }
        if (collision.gameObject.CompareTag("Box") && atack_time > 0)
        {
            GameObject body = collision.gameObject;
            int playerDam = w_player.GetComponent<PlayerStats>().player.myatack_power;
            playerDam = Mathf.RoundToInt(playerDam * Random.Range(0.9f, 1.1f));
            body.GetComponent<ObjectToDestoy>().TakeDamadge(playerDam);
        }
        }

}