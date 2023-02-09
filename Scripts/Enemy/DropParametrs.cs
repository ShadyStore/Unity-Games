using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropParametrs : MonoBehaviour
{
    public bool mel, health_potion, can_take;
    float time_to_active;
    public int health_val;
    private void Start()
    {
        time_to_active = 0.4f;
    }

    private void FixedUpdate()
    {
        if (!can_take)
        {
            if (time_to_active > 0)
            {
                time_to_active -= Time.deltaTime;
            }
            else { can_take = true; }
        }
    }
}
