using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour {

    public GameObject SettingCanvas = null;

	// Use this for initialization
	void Start () {
        LoadOption();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartButton()
    {
        SceneManager.LoadScene("main");
    }
    
    public void OptionButton()
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

            var BGM = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
            SettingCanvas.GetComponentInChildren<Slider>().value = GameData.Instance.BgmVolume;
            SettingCanvas.GetComponentInChildren<Toggle>().isOn = GameData.Instance.BgmOn;
        }
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public static void LoadOption()
    {
        string savePath = Path.Combine(Application.dataPath, "Option.json");
        if (File.Exists(savePath))
        {
            StreamReader read = File.OpenText(savePath);
            string text = read.ReadToEnd();
            JObject root = JObject.Parse(text);
            JObject option = root["Option"] as JObject;
            string strQuality = option["Quality"].ToString();
            string strVolume = option["BGMVolume"].ToString();
            string strBgmOn = option["BGMOn"].ToString();

            GameData.Instance.GameQuality = int.Parse(strQuality);
            GameData.Instance.BgmVolume = float.Parse(strVolume);
            GameData.Instance.BgmOn = bool.Parse(strBgmOn);

            read.Close();
        }

        QualitySettings.SetQualityLevel(GameData.Instance.GameQuality);
        var BGM = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        BGM.volume = GameData.Instance.BgmVolume;
        BGM.mute = !GameData.Instance.BgmOn;
    }
}
