using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour {

    public void QualityChanging(int value)
    {
        if(value == 0)
            QualitySettings.SetQualityLevel(1);
        else if(value == 1)
            QualitySettings.SetQualityLevel(3);
        else if(value == 2)
            QualitySettings.SetQualityLevel(5);
    }

    public void VolumeChanging(float value)
    {
        GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().volume = value;
    }

    public void VolumeOnOff(bool value)
    {
        if(value == true)
        {
            GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().mute = false;
        }
        else
        {
            GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().mute = true;
        }
    }

    public void CloseWindow()
    {
        JObject root = new JObject();
        JObject option = new JObject();
        
        option.Add("Quality", QualitySettings.GetQualityLevel().ToString());
        var BGM = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        option.Add("BGMVolume", BGM.volume);
        option.Add("BGMOn", !BGM.mute);
        root.Add("Option", option);

        string savePath = Path.Combine(Application.dataPath, "Option.json");
        File.WriteAllText(savePath, root.ToString());

        GameData.Instance.GameQuality = QualitySettings.GetQualityLevel();
        GameData.Instance.BgmVolume = BGM.volume;
        GameData.Instance.BgmOn = !BGM.mute;

        gameObject.SetActive(false);

        if(SceneManager.GetActiveScene().name != "TitleScene")
        {
            GameObject.FindGameObjectWithTag("SaveCanvas").SetActive(false);
        }
    }
}
