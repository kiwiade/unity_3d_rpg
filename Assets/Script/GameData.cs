using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {
    protected GameData() { }

    private int gameQuality = 5;
    private float bgmVolume = 1.0f;
    private bool bgmOn = true;

    public int GameQuality
    {
        get { return gameQuality; }
        set { gameQuality = value; }
    }

    public float BgmVolume
    {
        get { return bgmVolume; }
        set { bgmVolume = value; }
    }

    public bool BgmOn
    {
        get { return bgmOn; }
        set { bgmOn = value; }
    }
}
