﻿using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using Assets.Scripts.Serialization;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class SavingMenuIcon : MenuIcon
    {
        private readonly string pathToSaveFile;
        public SavingMenuIcon(GameObject icon, Action action, int iconIndex) : base(icon, action)
        {
            var path = GameManager.Instance.PathToSaveFile.Split(new string[] { ".json" }, System.StringSplitOptions.None);
            pathToSaveFile = path[0] + iconIndex.ToString() + path[1];
        }

        public override void Select()
        {
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
            save();
        }

        private void save()
        {
            // Get all objects created by user
            GameObject[] objectList = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
            if (objectList.Length != 0)
            {
                // Parse the objects into serializable versions
                var serializableArray = new SerializableObjectArrayWrapper();
                foreach (GameObject item in objectList)
                {
                    switch (item.name)
                    {
                        case GlobalVars.LineName:
                            serializableArray.lines.Add(new SerializableLine(item));
                            break;
                        default:
                            if (item.name.Contains(GlobalVars.PrimitiveObjectName))
                            {
                                serializableArray.primitives.Add(new SerializablePrimitive(item));
                            }
                            else
                            {
                                Debug.LogError("Attempted serialization of unsupported GameObject [" + item.name + "]");
                            }
                            break;
                    }
                }

                // Write new save
                Debug.Log("Saving world to file: " + pathToSaveFile);
                Debug.Log("    Lines: " + serializableArray.lines.Count);
                Debug.Log("    Primitives: " + serializableArray.primitives.Count);
                using (StreamWriter sw = File.CreateText(pathToSaveFile)) // overwrites old save
                {
                    sw.Write(JsonUtility.ToJson(serializableArray));
                }
            }
        }
    }
}