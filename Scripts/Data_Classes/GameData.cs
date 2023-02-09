using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

    public int lvl, game_lvl, exp, money, load_slot;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameData");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    public void NewGame(int slot)
    {
        load_slot = slot;
        game_lvl = 0;
        lvl = 1;
        exp = 0;
        money = 0;
        SaveData();
    }
    public void LoadData(int slot)
    {
            load_slot = slot;
            game_lvl = PlayerPrefs.GetInt("game_lvl" + load_slot);
            lvl = PlayerPrefs.GetInt("lvl" + load_slot);
            exp = PlayerPrefs.GetInt("exp" + load_slot);
            money = PlayerPrefs.GetInt("money" + load_slot);
        
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("load_slot", load_slot);
        PlayerPrefs.SetInt("game_lvl" + load_slot, game_lvl);
        PlayerPrefs.SetInt("lvl" +load_slot, lvl);
        PlayerPrefs.SetInt("exp" + load_slot, exp);
        PlayerPrefs.SetInt("money" + load_slot, money);
    }
}
