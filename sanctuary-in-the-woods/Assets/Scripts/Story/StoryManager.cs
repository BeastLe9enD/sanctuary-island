using System;
using Objects;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Story
{
    public class StoryManager : MonoBehaviour
    {
        private const float _maxVolume = 0.6f;
        
        public double Time;
        public double TimeScale = 1.0f;
        public int Day = 0;
        
        private Volume _volume;
        
        #region UI
        
        private Text _dayText;
        private Text _timeText;

        #endregion
        
        private void UpdateTime()
        {
            Time += 0.001 * TimeScale;
            if (Time >= 24.0)
            {
                Time = 0.0;
                ++Day;
            }
            
            var value = Math.Clamp((Math.Pow(Time - 13.9, 6.0) / 980000.0), 0.0, 1.0);
            _volume.weight = (float)value * _maxVolume;
            
            _dayText.text = $"Tag {Day}";
            _timeText.text = $"{(int) Time} Uhr";
        }

        void Start()
        {
            _volume = FindObjectOfType<Volume>();
            _dayText = GameObject.Find("DayText").GetComponent<Text>();
            _timeText = GameObject.Find("TimeText").GetComponent<Text>();
        }
        
        void FixedUpdate()
        {
            UpdateTime();
        }

        public bool CanSleep()
        {
            return Time < 6.0 || Time >= 10;
        }
        public void HandleMorning()
        {
            if (Time > 8.0f) {
                ++Day;
            }
            Time = 8.0f;

            HandleRabbits();
        }
        
        #region RABBITS

        private void HandleRabbits()
        {
            //TODO: if rabits exists, return
            
            var weedStorage = FindObjectOfType<WeedStorage>();
            if (weedStorage.Slot == null)
            {
                return;
            }
            
            
        }
        
        #endregion
    }
}