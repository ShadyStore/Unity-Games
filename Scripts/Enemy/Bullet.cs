using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int atak_power;
    [SerializeField] float life_time;

    private void Start()
    {
        life_time = 2;
    }
    private void FixedUpdate()
    {
        life_time -= Time.deltaTime;
        if (life_time < 0)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(speed, 0, 0);
    }

}
