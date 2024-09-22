using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class StartupSceneLoader
{
    private static bool TurnOff = false;

    static StartupSceneLoader()
    {
        EditorApplication.playModeStateChanged += OnPlayStateChange;
    }

    public static void OnPlayStateChange(PlayModeStateChange mode)
    {
        if(TurnOff) return;

        if (mode == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        if (mode == PlayModeStateChange.EnteredPlayMode)
        {
            if (EditorSceneManager.GetActiveScene().name != GameManager.SPLASHSCREEN_SCENE)
            {
                EditorSceneManager.LoadScene(GameManager.SPLASHSCREEN_SCENE);
            }
        }
    }
}
