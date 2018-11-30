using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData> {
    protected GameData() { }

    public int GameQuality = 5;
    public float BgmVolume = 1.0f;
    public bool BgmOn = true;
}
