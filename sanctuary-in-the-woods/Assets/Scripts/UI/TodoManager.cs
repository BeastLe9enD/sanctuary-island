using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class TodoManager : MonoBehaviour {

        public Text todoText;

        public void UpdateTodo(String todo) {
            todoText.text = todo;
        }
    }
}