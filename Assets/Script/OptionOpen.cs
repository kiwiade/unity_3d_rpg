using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionOpen : MonoBehaviour {

    [SerializeField]
    private GameObject SettingCanvas = null;
    [SerializeField]
    private GameObject SaveCanvas = null;
    [SerializeField]
    private GameObject SpellCanvas = null;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SettingCanvas.activeSelf == false)
            {
                SettingCanvas.SetActive(true);

                if (GameData.Instance.GameQuality == 1)
                    SettingCanvas.GetComponentInChildren<Dropdown>().value = 0;
                else if (GameData.Instance.GameQuality == 3)
                    SettingCanvas.GetComponentInChildren<Dropdown>().value = 1;
                else if (GameData.Instance.GameQuality == 5)
                    SettingCanvas.GetComponentInChildren<Dropdown>().value = 2;

                SettingCanvas.GetComponentInChildren<Slider>().value = GameData.Instance.BgmVolume;
                SettingCanvas.GetComponentInChildren<Toggle>().isOn = GameData.Instance.BgmOn;

                SaveCanvas.SetActive(true);
            }
            else
            {
                SettingCanvas.GetComponent<Option>().CloseWindow();
            }
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            if (SpellCanvas.activeSelf == false)
                SpellCanvas.SetActive(true);
            else
                SpellCanvas.SetActive(false);
        }
    }
}
