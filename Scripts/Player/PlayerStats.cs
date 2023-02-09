using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int lvl, game_lvl, exp, money;
    public GameData data;
    public HealthBar _bar;
    public HeroStatsClass.HeroStats player;
    [SerializeField ]float imba_mod_time;
    bool imba;

    void Start()
    {
        imba_mod_time = 0.5f;
        _bar = GameObject.FindGameObjectWithTag("Hud").GetComponent<HealthBar>();
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        GreatePlayer();
        player.Debugger();
    }
    private void Update()
    {

        if (imba_mod_time > 0)
        {
            imba = true;
            imba_mod_time -= Time.deltaTime;
            
        }
        else
        {
            imba = false;
        }
    }
    void GreatePlayer()
    {
        game_lvl = data.game_lvl;
        lvl = data.lvl;
        exp = data.exp;
        money = data.money;
        player = new HeroStatsClass.HeroStats(lvl, exp, money);
        _bar.SetMaxValuetBar(player.maxHp, player.max_exp);
        _bar.SetHealthBar(player.myHp);
        _bar.SetExpBar(player.myexp);
    }

    public void PersTakeDamadge(int damadge)
    {
        if (!imba)
        {
            player.TakeDamadge(damadge);
            _bar.SetHealthBar(player.myHp);
            imba_mod_time = 0.5f;
        }
    }
    public void PersTakeHeal(int heal)
    {
        player.TakeHeal(heal);
        _bar.SetHealthBar(player.myHp);
    }

    //Вероятнее всего стоит сохранять playr.lvl и т.д.
   public void SavePlayerToData()
    {
        data.game_lvl = game_lvl;
        data.lvl = player.mylvl;
        data.exp = player.myexp;
        data.money = player.mymoney;
    }
}
