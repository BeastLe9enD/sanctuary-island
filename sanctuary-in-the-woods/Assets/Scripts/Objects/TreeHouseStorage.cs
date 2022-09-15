using Story;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class TreeHouseStorage : MonoBehaviour
    {
        private Player _player;
        private StoryManager _storyManager;
        private Image _sleepImage;
        
        private bool _sleeping = false;
        private float _sleepTime;
        
        private void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var length = (_player.transform.position - transform.position).magnitude;
            if (length <= 2)
            {
                return;
            }

            if (!_storyManager.CanSleep())
            {
                return;
            }

            _sleeping = true;
            _sleepTime = Time.time;

            _sleepImage.enabled = true;
        }

        void Start()
        {
            _player = FindObjectOfType<Player>();
            _sleepImage = GameObject.Find("SleepPanel").GetComponent<Image>();
            _storyManager = FindObjectOfType<StoryManager>();
        }
        
        void Update()
        {
            if (_sleeping && Time.time - _sleepTime >= 2.0f)
            {
                _player.transform.position = transform.position - new Vector3(3.5f, 6);
                
                _storyManager.HandleMorning();
                
                _sleeping = false;
                _sleepImage.enabled = false;
            }
        }
    }
}