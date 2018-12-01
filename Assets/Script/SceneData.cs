using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneData {

    private static string PrevSceneName = "";

    public static void setPrevScene(string SceneName)
    {
        PrevSceneName = SceneName;
    }

    public static string getSceneName()
    {
        return PrevSceneName;
    }
}
