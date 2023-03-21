﻿using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Color CurrentColor { get; set; } = Color.magenta;
        public Action CurrentAction { get; set; }
        public ActionsData ActionsData { get; set; }
        public float CurrentLineThickness { get; } = 0.0f;
        public float MinStrokeWidth { get; } = 0.01f;
        public float MaxStrokeWidth { get; } = 0.5f;
        public float MinObjectSize { get; } = 0.2f;
        public float MaxObjectSize { get; } = 2f;
        public string PathToSaveFile { get; set; }
 

        void Awake()
        {
            Instance = this;
            PathToSaveFile = Application.persistentDataPath + "/save.json";
        }

        void Start()
        {
            ActionsData = new ActionsData();
            CurrentAction = ActionsData.Selecting;
            CurrentAction.Init();
        }

        public void changeCurrentAction(Action action)
        {
            CurrentAction.Finish();
            CurrentAction = action;
            CurrentAction.Init();
        }
    }
}