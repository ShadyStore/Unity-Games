using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatsClass : MonoBehaviour
{
    public class HeroStats
    {
        public int mylvl, myHp, maxHp, myMp, myatack_power, myexp, max_exp, mymoney;

        public HeroStats(int lvl, int exp, int money)
        {
            mylvl = lvl;
            myexp = exp;
            max_exp = 100*lvl;
            mymoney = money;
            myHp = (lvl *5) + 10;
            maxHp = myHp;
            myatack_power = (lvl * 2);
            myMp = (lvl * 2) + 10;
        }
        public void Debugger()
        {
            Debug.Log("Уровень: " + mylvl);
            Debug.Log("Опыт: " + myexp);
            Debug.Log("Деньги: " + mymoney);
            Debug.Log("Хп: " + myHp);
            Debug.Log("Урон: " + myatack_power);
            Debug.Log("Нужно опыта: " + max_exp);
        }

        public void TakeExp(int exp)
        {
            HealthBar _bar = GameObject.FindGameObjectWithTag("Hud").GetComponent<HealthBar>();

            if (myexp + exp < max_exp)
            {
                myexp += exp;
                _bar.SetExpBar(myexp);
                Debug.Log("Опыт " + myexp);
            }
            else
            {
                myexp += exp;
                LvlUp();
                Debug.Log("До следуюущего уровня " + max_exp);
                _bar.SetMaxValuetBar(myHp, max_exp);
                _bar.SetExpBar(myexp);
            }
        }
        public void LvlUp()
        {
            mylvl += 1;
            myexp -= max_exp;
            max_exp = 100 * mylvl;
            myHp = (mylvl * 5) + 10;
            maxHp = myHp;
            myatack_power = (mylvl * 2);
            myMp = (mylvl * 2) + 10;
            Debug.Log("Уровень " + mylvl);       
        }
        public void TakeDamadge(int damadge)
        {
            if (myHp <= damadge)
            {
                myHp -= damadge;
                GameOver();
            }
            else
            {
                myHp -= damadge;
            }
        }
        public void TakeHeal(int heal)
        {
            if (myHp + heal < maxHp)
            {
                myHp += heal;
            }
            else
            {
                myHp = maxHp;
            }
        }
        public void GameOver()
        {
            // Важно выключаем кнопку продолжить должна быть 6

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Character_controller>().Pause_btn();
            HealthBar _bar = GameObject.FindGameObjectWithTag("Hud").GetComponent<HealthBar>();
            GameObject resume = _bar.transform.GetChild(6).GetChild(0).gameObject;
            resume.SetActive(false);
        }
    }
    public class EnemyStatsClass
    {
        public int myHp, maxHp, myatack_power;
        public EnemyStats boring;
        public EnemyStatsClass(int hp, int atack_power)
        {
            myHp = hp;
            maxHp = hp;
            myatack_power = atack_power;

        }
        public void Debugger()
        {
            Debug.Log("Хп: " + myHp);
        }
        public void TakeDamadge(int damadge)
        {

            if (myHp <= damadge)
            {
                myHp -= damadge;
                EnemyBar(maxHp,myHp);
                Death();
            }
            else
            {
                myHp -= damadge;
                EnemyBar(maxHp,myHp);
            }
        }
        void EnemyBar(int maxHp, int hp)
        {
            HealthBar _bar = GameObject.FindGameObjectWithTag("Hud").GetComponent<HealthBar>();
            _bar.SetEnemyBar(maxHp,hp);
        }
        public void Death()
        {
            boring.EnemyDeath();
        }
    }
}
