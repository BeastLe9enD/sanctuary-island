using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Story
{
    public class StoryManager : MonoBehaviour
    {
        private const float _maxVolume = 0.6f;
        
        public int Time; //von 0 bis 2400

        private Volume _volume;

        private void UpdateVolumeWeight()
        {
            var value = Math.Clamp((Math.Pow(Time / 100.0 - 13.9, 6.0) / 980000.0), 0.0, 1.0);
            _volume.weight = (float)value * _maxVolume;
            
            Debug.Log(Time + ":" + _volume.weight);
        }

        void Start()
        {
            _volume = FindObjectOfType<Volume>();
        }
        
        void FixedUpdate()
        {
            if (++Time == 2400)
            {
                Time = 0;
            }
            
            UpdateVolumeWeight();
        }
    }
}