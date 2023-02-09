using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject slot_panel;
    [SerializeField] GameData data;
    [SerializeField] AudioSource sound;
    [SerializeField] Slider sound_bar;
    bool load;
    int sitings;
    // Update is called once per frame
    private void Start()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameData>();
        sound = GameObject.FindGameObjectWithTag("GameData").transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void NewGameBtn(int slot)
    {
        if (!load)
        {
            data.NewGame(slot);
            slot_panel.SetActive(false);
            SceneTransitor.SwitchToScene("lvl_0", "sceneNewGame");
        }
        else if (load)
        {
            if (PlayerPrefs.HasKey("game_lvl" + slot))
            {
                data.LoadData(slot);
                Debug.Log("Load Game  " + data.game_lvl);
                slot_panel.SetActive(false);
                SceneTransitor.SwitchToScene("lvl_" + data.game_lvl, "sceneNewGame");
            }
        }
    }
    public void GameBtnPanel(bool load_btn)
    {
        load = load_btn;
        if (slot_panel.activeSelf)
        {
            sitings = 0;
            slot_panel.SetActive(false);
        } else 
        {
            sitings = 0;
            slot_panel.SetActive(true);
            slot_panel.transform.GetChild(3).gameObject.SetActive(false);
            slot_panel.transform.GetChild(4).gameObject.SetActive(false);
            slot_panel.transform.GetChild(0).gameObject.SetActive(true);
            slot_panel.transform.GetChild(1).gameObject.SetActive(true);
            slot_panel.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void Sitings_btn(int sitings_btn)
    {
        sitings += sitings_btn;
        if (slot_panel.activeSelf && sitings>=2)
        {
            slot_panel.transform.GetChild(3).gameObject.SetActive(false);
            slot_panel.transform.GetChild(4).gameObject.SetActive(false);
            slot_panel.transform.GetChild(0).gameObject.SetActive(true);
            slot_panel.transform.GetChild(1).gameObject.SetActive(true);
            slot_panel.transform.GetChild(2).gameObject.SetActive(true);
            slot_panel.SetActive(false);
            sitings = 0;

        }
        else
        {
            slot_panel.SetActive(true);
            slot_panel.transform.GetChild(3).gameObject.SetActive(true);
            slot_panel.transform.GetChild(4).gameObject.SetActive(true);
            slot_panel.transform.GetChild(0).gameObject.SetActive(false);
            slot_panel.transform.GetChild(1).gameObject.SetActive(false);
            slot_panel.transform.GetChild(2).gameObject.SetActive(false);
        }

    }
    public void OnChangeSlider()
    {
        sound.volume = sound_bar.value;
    }
    public void AutorBtn()
    {
        slot_panel.SetActive(false);
        SceneTransitor.SwitchToVideo("AutorVideo");
    }
}
