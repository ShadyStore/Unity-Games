using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToDestoy : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] GameObject[] drops;
    [SerializeField] bool random_drop;
    public void TakeDamadge(int damadge)
    {
        if (hp <= damadge)
        {
            hp -= damadge;
            Destroy(gameObject);
            BoxDrop();
        }
        else
        {
            hp -= damadge;
        }
    }
    public void BoxDrop()
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
