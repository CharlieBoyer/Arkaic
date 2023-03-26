using System;
using UnityEngine;

namespace Managers
{
    public class SceneLoader: MonoBehaviour
    {
        public static SceneLoader instance;

        private static bool AltF4 => Input.GetKeyDown(KeyCode.LeftControl) &&
                                     Input.GetKeyDown(KeyCode.LeftAlt) &&
                                     Input.GetKeyDown(KeyCode.F4);

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(this.gameObject);
        }

        private void Update()
        {
            if (AltF4) { Exit(); }
            
            if (Input.GetButtonDown("Cancel") && !UIManager.pauseActive)
                UIManager.instance.Pause();
            else if (Input.GetButtonDown("Cancel") && UIManager.pauseActive)
                UIManager.instance.Resume();
        }

        public static void Exit() {
            UnityEditor.EditorApplication.ExitPlaymode();
            // Application.Quit();
        }
        
        
    }
}
