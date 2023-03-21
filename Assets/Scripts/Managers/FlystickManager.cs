﻿using Assets.Scripts.Menus;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class FlystickManager : MonoBehaviour
    {
        public static FlystickManager Instance;
        public GameObject Flystick;
        public GameObject MultiTool;

        void HandleInput(string input)
        {
            switch (input)
            {
                case "trigger_down":
                    GameManager.Instance.CurrentAction.HandleTriggerDown();
                    break;
                case "trigger_up":
                    GameManager.Instance.CurrentAction.HandleTriggerUp();
                    break;
                case "button3":
                    toggleAction();
                    break;
                default:
                    break;
            }
        }

        void Update()
        {
            string input = "";
            //for monoscopic mode
            if (Input.GetButtonDown(Button.TRIGGER))
            {
                input = "trigger_down";
            }

            if (Input.GetButtonUp(Button.TRIGGER))
            {
                input = "trigger_up";
            }

            if (Input.GetButtonDown(Button.REDO))
            {
                input = "button1";
            }

            if (Input.GetButtonDown(Button.UNDO))
            {
                input = "button2";
            }

            if (Input.GetButtonDown(Button.SELECTING_MODE))
            {
                input = "button3";
            }

            HandleInput(input);
            GameManager.Instance.CurrentAction.Update();
        }

        void Awake()
        {
            Instance = this;
        }

        private void toggleAction()
        {
            ToolsMenu toolsMenu = MenuManager.Instance.ToolsMenu;
            if (toolsMenu.PreviouslySelectedIcon != toolsMenu.selectingIcon && toolsMenu.SelectedIcon != toolsMenu.selectingIcon)
            {
                toolsMenu.PreviouslySelectedIcon = toolsMenu.selectingIcon;
            }
            Debug.Log(GetType().Name + ": deselecting " + toolsMenu.SelectedIcon.gameObject.name + " selecting " + toolsMenu.PreviouslySelectedIcon.gameObject.name);
            var tmp = toolsMenu.PreviouslySelectedIcon;
            toolsMenu.SelectedIcon.Deselect();
            tmp.Select();
        }
    }
}