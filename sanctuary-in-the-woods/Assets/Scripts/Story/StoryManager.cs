using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Story
{
    public class StoryManager : MonoBehaviour
    {
        private const float _maxVolume = 0.6f;
        
        public double Time;
        public double TimeScale = 1.0f;
        
        private Volume _volume;

        private void UpdateTime()
        {
            Time += 0.001 * TimeScale;
            if (Time >= 24.0)
            {
                Time = 0.0;
            }
            
            var value = Math.Clamp((Math.Pow(Time - 13.9, 6.0) / 980000.0), 0.0, 1.0);
            _volume.weight = (float)value * _maxVolume;
            
            Debug.Log(Time + ":" + _volume.weight);
        }

        void Start()
        {
            _volume = FindObjectOfType<Volume>();
        }
        
        void FixedUpdate()
        {
            UpdateTime();
        }
    }
}