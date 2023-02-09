using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScript : MonoBehaviour
{
    [SerializeField] GameObject[] start_pos;
    [SerializeField] Sprite[] clouds;
    [SerializeField] float last_posX, last_posY, last_posZ;
    [SerializeField] int restart_X_pos;



    // Облака по очереде текут налево и потом возвращаются на последнюю позицию,
    // ВАЖНО сделать крайнюю позицию массива, правой крайней точкой
    // restart_X_pos -> выставляем позицию обновления обачков

    private void Start()
    {
        for (int i = 0; i < start_pos.Length; i++)
        {

            start_pos[i].GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length)];
            if (i == start_pos.Length-1)
            {
                last_posX = start_pos[i].transform.position.x;
                last_posY = start_pos[i].transform.position.y;
                last_posZ = start_pos[i].transform.position.z;
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < start_pos.Length; i++)
        {
            if (start_pos[i].transform.position.x > -restart_X_pos)
            {
                start_pos[i].transform.position += new Vector3(-0.2f * Time.deltaTime, 0, 0);
            }
            else
            {
                start_pos[i].GetComponent<SpriteRenderer>().sprite = clouds[Random.Range(0, clouds.Length)];
                start_pos[i].transform.position = new Vector3 (last_posX,
                    last_posY + Random.Range(-1f,2f),
                    last_posZ);

            }
        }
    }
}
