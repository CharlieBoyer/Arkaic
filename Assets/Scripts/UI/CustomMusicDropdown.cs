using System;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using TMPro;
using Unity.VisualScripting;

namespace UI
{
    public class CustomMusicDropdown: MonoBehaviour
    {
        private TMP_Dropdown _dropdown;
        public List<AudioClip> musics;

        private void Awake() {
            _dropdown = GetComponent<TMP_Dropdown>();
        }

        private void Start()
        {
            _dropdown.ClearOptions();

            foreach (AudioClip clip in musics) {
                _dropdown.options.Add(new TMP_Dropdown.OptionData(clip.name));
            }
            _dropdown.RefreshShownValue();
            _dropdown.onValueChanged.AddListener(delegate { UpdatePlayingClip(_dropdown.value); });
        }

        private void UpdatePlayingClip(int optionIndex)
        {
            if (optionIndex >= 0 || optionIndex < musics.Count)
                AudioManager.instance.ChangeAudioClip(musics[optionIndex]);
        }
    }
}
