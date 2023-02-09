using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMagick : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform player_transform;
    float dist_to_player;
    Material my_material;

    private void Start()
    {
        my_material = GetComponent<Renderer>().material;
        player = GameObject.FindGameObjectWithTag("Player");
        player_transform = player.GetComponent<Transform>().transform;
        
    }
    // Start is called before the first frame update
    private void Update()
    {
        bool meel = player.GetComponent<Character_controller>().have_mel;
        if (meel)
        {
            dist_to_player = Vector2.Distance(transform.position, player_transform.position);

            if (dist_to_player < 7)
            {
                my_material.SetFloat("_EffectAmount", (dist_to_player / 7) + 0.1f);
            }
        }
    }
}
