﻿using System.Collections;
using UnityEngine;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Managers
{
    public class FlystickManager : MonoBehaviour
    {
        public static FlystickManager Instance;

        public GameObject Flystick;
        public GameObject MultiTool;
        // przykladowe
        void HandleInput(string input)
        {
            if (input == "trigger_down")
            {
                GameManager.Instance.CurrentAction.HandleTriggerDown();
            }
            else if (input == "trigger_up")
            {
                GameManager.Instance.CurrentAction.HandleTriggerUp();
            }
        }

        void Update()
        {
            string input = "";
            //for monoscopic mode
			if(Input.GetButtonDown("Trigger")) {
				input = "trigger_down";
			}

			if(Input.GetButtonUp("Trigger")) {
				input = "trigger_up";
			}

            HandleInput(input);
            GameManager.Instance.CurrentAction.Update();
        }

        void Awake()
        {
            Instance = this;
        }

        //test
        void Start()
        {
            //GameManager.Instance.CurrentAction = new Selecting();
            GameManager.Instance.CurrentAction = new LineDrawing();
            //GameManager.Instance.CurrentAction.HandleTriggerDown();
        }
    }
}