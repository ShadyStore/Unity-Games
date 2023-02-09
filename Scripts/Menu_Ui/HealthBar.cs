using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider health_bar, exp_bar, enemy_bar, sound_bar;
    [SerializeField] GameObject enmy_bar_obj;
    public GameObject panel;
    [SerializeField] AudioSource sound;
    float taimer_enemy_bar;

    private void Start()
    {
        // Важно включаем кнопку "Продолжить" продолжить должна быть 6
        sound = GameObject.FindGameObjectWithTag("GameData").transform.GetChild(0).GetComponent<AudioSource>();
        sound.Play(0);
        GameObject resume = transform.GetChild(6).GetChild(0).gameObject;
        resume.SetActive(true);
    }
    void Update()
    {
        if (taimer_enemy_bar > 0 && enemy_bar.value > 0)
        {
            taimer_enemy_bar -= Time.deltaTime;
            enmy_bar_obj.SetActive(true);
        }
        else
        {
            enmy_bar_obj.SetActive(false);
        }
    }
    public void OnChangeSlider()
    {
        sound.volume = sound_bar.value;
    }
    public void SetMaxValuetBar(int hp, int exp)
    {
        health_bar.maxValue = hp;
        exp_bar.maxValue = exp;
    }

    public void SetHealthBar(int hp)
    {
        health_bar.value = hp;
    }
    public void SetExpBar(int exp)
    {
        exp_bar.value = exp;
    }
    public void SetEnemyBar(int maxHp,int hp)
    {
        enemy_bar.maxValue = maxHp;
        enemy_bar.value = hp;

        if (enemy_bar.value <= 0)
        {
            enmy_bar_obj.SetActive(false);
        }
        else
        {
            taimer_enemy_bar = 2f;
        }
    }

}
