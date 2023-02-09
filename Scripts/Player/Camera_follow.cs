using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField]  float camera_Speed;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        transform.position = new Vector3()
        {
            x = player.transform.position.x + 3,
            y = player.transform.position.y + 3f,
            z = player.transform.position.z - 10,
        };
        camera_Speed = 8f;
 
    }
    private void FixedUpdate()
    {
        if (player)
        {
            Vector3 target = new Vector3()
            {
                x = player.transform.position.x + 3,
                y = player.transform.position.y + 3f,
                z = player.transform.position.z - 10,
            };

            transform.position = Vector3.Lerp(transform.position, target, camera_Speed * Time.deltaTime);
        }
    }

}
