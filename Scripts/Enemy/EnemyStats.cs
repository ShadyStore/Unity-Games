using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int hp, atack_power, exp_for_player;
    [SerializeField] bool random_drop;
    [SerializeField] GameObject[] drops;
    public HeroStatsClass.EnemyStatsClass enemy;

    private void Start()
    {
        enemy = new HeroStatsClass.EnemyStatsClass(hp, atack_power);
        enemy.boring = this;
    }

    public void EnemyDeath()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().player.TakeExp(exp_for_player);
        EnemyDrop();
        Destroy(gameObject);
    }

   public void EnemyDrop()
    {
        Debug.Log("Drop - ");

        Vector3 drop_pos = transform.position;
        if (!random_drop)
        {
            for (int i = 0; i < drops.Length; i++)
            {
                Debug.Log("Drop - " + drops[i]);
                GameObject newdrop = Instantiate(drops[i], drop_pos, Quaternion.identity);
               newdrop.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(-3f, 3f), 3);
            }
        }
    }
}
