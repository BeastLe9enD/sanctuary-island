using System;
using Animals.Mole;
using Objects;
using Objects.Animals;
using UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
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
        public GameObject Bear;
        public GameObject BerryBush;
        public GameObject Mole;
        public GameObject Beaver;
        public GameObject Flamingo;
        public GameObject Elephant;
        public GameObject PalmTree;
        
        #endregion
        
        #region STATES

        public bool FirstAnimalTamed;
        public bool FirstTreeCutDown;
        public bool PondPlaced;
        public bool SecondPondPlaced;
        public bool FirstCakeCrafted;
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
            HandleBears();
            HandleMoles();
            HandleBeavers();
            HandleFlamingos();
            HandleElephants();
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
            _popupManager.Enqueue("You can grow the seeds to make the bear appear on the next day.");
        }

        private void HandleBears()
        {
            var bears = FindObjectsOfType<BearStorage>();
            if (bears.Length > 0)
            {
                return;
            }

            var berryPlants = FindObjectsOfType<BerryPlantStorage>();

            var numPlants = berryPlants.Length;
            if (numPlants == 0)
            {
                return;
            }
            
            if (numPlants >= 4)
            {
                numPlants = 4;
            }

            for (var i = 0; i < berryPlants.Length; i++)
            {
                var berryPlant = berryPlants[i];

                var targetPos = berryPlant.transform.position;

                Destroy(berryPlant.gameObject);

                Instantiate(BerryBush, targetPos, Quaternion.identity);

                if (i <= numPlants)
                {
                    Instantiate(Bear, targetPos, Quaternion.identity);
                }
            }
        }

        private void ShowBearMessage()
        {
            _popupManager.Enqueue("Oh, the bear is here!");
            _popupManager.Enqueue("He can destroy little rocks if you feed him with berry feed.");
            _popupManager.Enqueue("You can use the bear to clean the sand hill in the north east from stones.");
            _popupManager.Enqueue("If you cleaned the sand hill, on the next day, the mole will appear there.");
        }

        private void HandleMoles()
        {
            var moles = FindObjectsOfType<MoleStorage>();
            if (moles.Length > 0)
            {
                return;
            }

            var tilemap = GameObject.Find("TopSolid").GetComponent<Tilemap>();
            
            for (var j = -28; j <= 20; j++)
            {
                for (var i = 62; i <= 73; i++)
                {
                    var position = new Vector3Int(i, j, 0);
                    if (tilemap.GetTile(position) != null)
                    {
                        return;
                    }
                }
            }

            var worldPos = tilemap.CellToWorld(new Vector3Int(67, -24, 0));
            Instantiate(Mole, worldPos, Quaternion.identity);
            
            ShowMoleMessage();
        }

        private void ShowMoleMessage()
        {
            _popupManager.Enqueue("The mole appeared on the sand hill!");
            _popupManager.Enqueue("You can tame him wit seed feed.");
            _popupManager.Enqueue("After the mole has been tamed, you can give him 6 seed feed to build a pond.");
            _popupManager.Enqueue("After sleeping, the beaver will appear on the pond build by the mole.");
        }
        
        private void HandleBeavers() {
            var beavers = FindObjectsOfType<BeaverStorage>();
            if (beavers.Length > 0)
            {
                return;
            }

            if (!PondPlaced)
            {
                return;
            }
            
            Instantiate(Beaver, MolePondBuildState.POND_POSITION, Quaternion.identity);

            ShowBeaverMessage();
        }

        private void ShowBeaverMessage()
        {
            _popupManager.Enqueue("Yay! The beaver has arrived!");
            _popupManager.Enqueue("So you can tame the beaver with berry feed.");
            _popupManager.Enqueue("After being tamed, the beaver can be fed with 2 berry feed to build the bridge.");
            _popupManager.Enqueue("This allows you to pass the river, where the kangaroo is already waiting for you.");
            _popupManager.Enqueue("If you feed the kangaroo with all seed variants, it will give you a cake.");
        }

        private void HandleFlamingos()
        {
            var flamingos = FindObjectsOfType<FlamingoStorage>();
            if (flamingos.Length > 0)
            {
                return;
            }

            if (!SecondPondPlaced)
            {
                return;
            }
            
            Instantiate(Flamingo, MolePondBuildState.SECOND_POND_POSITION, Quaternion.identity);

            ShowFlamingoMessage();
        }

        private void ShowFlamingoMessage()
        {
            _popupManager.Enqueue("The flamingo arrived!");
            _popupManager.Enqueue("You can tame the flamingo with berry feed.");
            _popupManager.Enqueue("Feed the flamingo 4 berry feed and it will make the oasis green again.");
            _popupManager.Enqueue("When the oasis has been greened, the elephants will appear the next day.");
        }

        private void HandleElephants()
        {
            var elephants = FindObjectsOfType<ElephantStorage>();
            if (elephants.Length > 0)
            {
                return;
            }

            var palmTrees = FindObjectsOfType<PalmTreeStorage>();
            if (palmTrees.Length == 0)
            {
                return;
            }

            Instantiate(Elephant, new Vector3(23.01f, -90.0f, 1.0f), Quaternion.identity);
            Instantiate(Elephant, new Vector3(30.28f, -95.0f, 1.0f), Quaternion.identity);
            Instantiate(Elephant, new Vector3(37.64f, -100.0f, 1.0f), Quaternion.identity);
            
            ShowElephantMessage();
        }

        private void ShowElephantMessage()
        {
            _popupManager.Enqueue("The elephants have arrived!");
            _popupManager.Enqueue("You can tame the elephants with berry feed.");
            _popupManager.Enqueue("You can feed the elephants with berry feed to make them destroy the big rocks in the north.");
        }

        #endregion
    }
}