using Assets.Scripts.Actions;
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
            GameObject[] objectList = GameObject.FindGameObjectsWithTag(GameManager.UniversalTag);
            if (objectList.Length != 0)
            {
                // Parse the objects into serializable versions
                var serializableArray = new SerializableObjectArrayWrapper();
                foreach (GameObject item in objectList)
                {
                    switch (item.name)
                    {
                        case GameManager.LineName:
                            serializableArray.lines.Add(new SerializableLine(item));
                            break;
                        case GameManager.Line3DName:
                            serializableArray.lines3d.Add(new SerializableLine3D(item));
                            break;
                        case GameManager.Line3DCubeSegmentName:
                        case GameManager.Line3DCylinderSegmentName:
                            break; // ignore these, they're handled by Line3DName
                        default:
                            if (item.name.Contains(GameManager.PrimitiveObjectName))
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
                Debug.Log("    Lines3D: " + serializableArray.lines3d.Count);
                Debug.Log("    Primitives: " + serializableArray.primitives.Count);
                using (StreamWriter sw = File.CreateText(pathToSaveFile)) // overwrites old save
                {
                    sw.Write(JsonUtility.ToJson(serializableArray));
                }
            }
        }
    }
}