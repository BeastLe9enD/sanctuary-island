using System;
using Objects;
using Objects.Animals;
using UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Utils;

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
        
        #region ANIMALS

        public GameObject Rabbit;
        public GameObject Bird;
        
        #endregion
        
        #region STATES

        public bool FirstAnimalTamed;
        public bool FirstTreeCutDown;
        #endregion

        private PopupManager _popupManager;
        
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

            _popupManager = FindObjectOfType<PopupManager>();
            _popupManager.Enqueue("Hey! Welcome to the island.");
            _popupManager.Enqueue("At the moment you are alone, but there are many great animals that you are responsible for.");
            _popupManager.Enqueue("You can collect weed and put it into the feed jug to attract rabbits.");
            _popupManager.Enqueue("And don't forget to sleep. Click on the tree house after 10am sleep until morning.");
            _popupManager.Enqueue("The rabbits will already be waiting for you the next day.");
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
            HandleBirds();
        }
        
        #region RABBITS

        private void HandleRabbits()
        {
            var rabbits = FindObjectsOfType<RabbitStorage>();
            if (rabbits.Length > 0)
            {
                return;
            }
            
            var weedStorage = FindObjectOfType<WeedStorage>();
            if (weedStorage.Slot == null)
            {
                return;
            }

            var random = RamdomUtils.GetRandom();
            var sourcePos = weedStorage.transform.position;
            
            for (var i = 0; i < 2; i++)
            {
                weedStorage.Clear();
                
                var targetPos = sourcePos + new Vector3(random.NextFloat(-2, 2), random.NextFloat(-2, 2));

                Instantiate(Rabbit, targetPos, Quaternion.identity);
            }

            ShowRabbitMessage();
        }

        private void ShowRabbitMessage()
        {
            _popupManager.Enqueue("Oh, the rabbits are here. However, they are not yet tamed.");
            _popupManager.Enqueue("You can tame the animals by crafting Wild Food.");
            _popupManager.Enqueue("To craft Wild Food, you must combine Weeds with a Bag.");
            _popupManager.Enqueue("You make a bag from two weeds.");
            _popupManager.Enqueue("You can open the crafting menu by pressing E");
        }
        
        private void HandleBirds()
        {
            var birds = FindObjectsOfType<BirdStorage>();
            if (birds.Length > 0)
            {
                return;
            }

            var birdHouse = FindObjectOfType<BirdHouseStorage>();
            if (birdHouse == null)
            {
                return;
            }
            
            var random = RamdomUtils.GetRandom();
            var sourcePos = birdHouse.transform.position;
            
            for (var i = 0; i < 3; i++)
            {
                var targetPos = sourcePos + new Vector3(random.NextFloat(-2, 2), random.NextFloat(-2, 2));

                Instantiate(Bird, targetPos, Quaternion.identity);
            }
            
            ShowBirdMessage();
        }

        private void ShowBirdMessage()
        {
            _popupManager.Enqueue("Yaaay, the birds are here!");
            _popupManager.Enqueue("You can give them wild food and they will give you seeds in exchange.");
        }
        
        #endregion
    }
}