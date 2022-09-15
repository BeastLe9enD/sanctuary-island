using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI { 
    public class PopupManager : MonoBehaviour {
        private class Task {
            public readonly string Name;
            public readonly Action Action;

            public Task(string name, Action action) {
                Name = name;
                Action = action;
            }
        }

        private Queue<Task> _tasks = new();

        private Task _current = null;

        private void SetCurrent(Task current) {
            _current = current;

            _text.text = _current.Name;
        }
   
        private Text _text;
        private Button _button;
        private GameObject _panel;

        public void Enqueue(string name, Action action = null) {
            _tasks.Enqueue(new Task(name, action));
        }

        private void Start() {
            _text = GameObject.Find("PopupText").GetComponent<Text>();
            _button = GameObject.Find("PopupButton").GetComponent<Button>();

            _panel = GameObject.Find("PopupPanel");
            _panel.SetActive(false);

            _button.onClick.AddListener(() => {
                if (_current != null) {
                    if (_current.Action != null) {
                        _current.Action();
                    }
                    
                    _current = null;
                    _panel.SetActive(false);
                }
            });
        }

        private void Update() {
            if (_current == null && _tasks.Count > 0) {
                SetCurrent(_tasks.Dequeue());
                _panel.SetActive(true);
            }
        }
    }
}