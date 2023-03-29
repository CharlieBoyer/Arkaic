using System;
using UnityEngine;

namespace Managers
{
    public class SceneLoader: MonoBehaviour
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static SceneLoader instance;

        private static bool AltF4 => Input.GetKey(KeyCode.LeftControl) &&
                                     Input.GetKey(KeyCode.LeftAlt) &&
                                     Input.GetKey(KeyCode.F4);

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(this.gameObject);
        }

        private void Update()
        {
            if (AltF4)
            {
                Exit();
            }
            
            if (Input.GetButtonDown("Cancel") && !UIManager.pauseActive)
                UIManager.instance.Pause();
            else if (Input.GetButtonDown("Cancel") && UIManager.pauseActive)
                UIManager.instance.Resume();
        }

        public static void Exit()
        {
            Application.Quit();
            // UnityEditor.EditorApplication.ExitPlaymode();
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}
