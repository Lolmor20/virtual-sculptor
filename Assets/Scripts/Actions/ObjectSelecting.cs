using Assets.Scripts.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ObjectSelecting : Action
    {
        private static HashSet<GameObject> SelectedObjects { get; set; } = new HashSet<GameObject>();
        private readonly HashSet<GameObject> toBeSelected = new HashSet<GameObject>();
        private readonly HashSet<GameObject> toBeRemoved = new HashSet<GameObject>();
        private SelectionState CurrentState = SelectionState.STANDBY;
        private SelectionState ToolState = SelectionState.SELECTING;

        private enum SelectionState
        {
            STANDBY,
            SELECTING,
            COPYING,
            MOVING
        }

        public void SetStateCopying()
        {
            ToolState = SelectionState.COPYING;
        }

        public void SetStateMoving()
        {
            ToolState = SelectionState.MOVING;
        }

        public void SetStateChangeColor()
        {
            ChangeSelectionColor();
            ToolState = SelectionState.STANDBY;
        }

        public override void HandleTriggerDown()
        {
            switch (ToolState)
            {
                case SelectionState.SELECTING: // select objects
                    CurrentState = SelectionState.SELECTING;
                    break;
                case SelectionState.COPYING: // copy selected objects relative to flystick position
                    CopySelection();
                    CurrentState = SelectionState.MOVING;
                    MoveObjects();
                    break;
                case SelectionState.MOVING: // move selected objects relative to flystick position
                    CurrentState = SelectionState.MOVING;
                    MoveObjects();
                    break;
                default: // catching bugs
                    ToolState = SelectionState.SELECTING;
                    break;
            }
        }

        public override void HandleTriggerUp()
        {
            switch (CurrentState)
            {
                case SelectionState.SELECTING:
                    ToolState = SelectionState.SELECTING;
                    CurrentState = SelectionState.STANDBY;
                    SelectedObjects.UnionWith(toBeSelected);
                    SelectedObjects.ExceptWith(toBeRemoved);
                    toBeSelected.Clear();
                    toBeRemoved.Clear();
                    break;

                case SelectionState.MOVING:
                    ToolState = SelectionState.SELECTING;
                    CurrentState = SelectionState.STANDBY;
                    StopMovingObjects(deselect: true);
                    break;

                default:
                    break;
            }
        }

        public override void Init()
        {
            // Nothing happens
        }

        public override void Update()
        {
            if (CurrentState == SelectionState.SELECTING)
            {
                var multiTool = FlystickManager.Instance.MultiTool;

                Collider[] hitColliders = Physics.OverlapBox(multiTool.transform.position, multiTool.GetComponent<Renderer>().bounds.size * 0.75f);
                var collidingObjects = from obj in hitColliders
                                       where obj.gameObject.tag == GlobalVars.UniversalTag
                                       select obj.gameObject;

                foreach (GameObject obj in collidingObjects)
                {
                    bool willBeSelected = toBeSelected.Contains(obj);
                    bool willBeRemoved = toBeRemoved.Contains(obj);
                    if (!willBeSelected && !willBeRemoved)
                    {
                        if (SelectedObjects.Contains(obj))
                        {
                            changeColorToDefault(obj);
                            toBeRemoved.Add(obj);
                        }
                        else
                        {
                            changeColorToSelected(obj);
                            toBeSelected.Add(obj);
                        }
                    }
                }
            }
        }

        public override void Finish()
        {
            // Nothing happens
        }

        public void DeleteSelection()
        {
            foreach (var selectedObject in SelectedObjects)
            {
                Object.Destroy(selectedObject);
            }
            SelectedObjects.Clear();
        }

        public void CopySelection()
        {
            var toBeCopied = new HashSet<GameObject>();
            foreach (var oldObj in SelectedObjects)
            {
                GameObject newObj;
                newObj = Object.Instantiate(oldObj);
                newObj.name = oldObj.name;
                var renderer = newObj.GetComponent<MeshRenderer>();
                //renderer.material = new Material(Shader.Find("Sprites/Diffuse"));
                renderer.material.color = oldObj.GetComponent<MeshRenderer>().material.color;
                toBeCopied.Add(newObj);
            }
            DeselectAll();
            SelectedObjects.UnionWith(toBeCopied);
        }

        internal void MoveObjects()
        {
            foreach (var obj in SelectedObjects)
            {
                obj.transform.parent = FlystickManager.Instance.MultiTool.transform;
            }
        }

        private void StopMovingObjects(bool deselect = true)
        {
            foreach (var obj in SelectedObjects)
            {
                obj.transform.parent = null;
            }
            if (deselect) DeselectAll();
        }

        public static void DeselectAll()
        {
            foreach (var obj in SelectedObjects)
            {
                changeColorToDefault(obj);
            }
            SelectedObjects.Clear();
        }

        public void ChangeSelectionColor()
        {
            foreach (var obj in SelectedObjects)
            {
                obj.GetComponent<Renderer>().material.SetColor("_Color", GameManager.Instance.CurrentColor);
                //obj.GetComponent<Renderer>().material. = GameManager.Instance.CurrentColor;
            }
            SelectedObjects.Clear();
        }

        public void ChangeSelectionScale(Vector3 scale)
        {
            foreach (var obj in SelectedObjects)
            {
                obj.transform.localScale = scale;
            }
        }

        private static void changeColorToDefault(GameObject obj)
        {
            Material mat = obj.GetComponent<Renderer>().material;
            toOpaqueMode(mat);
            Color color = mat.GetColor("_Color");
            color += new Color(0f, 0f, 0f, 0.75f);
            mat.SetColor("_Color", color);
        }

        private void changeColorToSelected(GameObject obj)
        {
            Material mat = obj.GetComponent<Renderer>().material;
            toFadeMode(mat);
            Color color = mat.GetColor("_Color");
            color -= new Color(0f, 0f, 0f, 0.75f);
            mat.SetColor("_Color", color);
        }

        private static void toOpaqueMode(Material material)
        {
            material.SetOverrideTag("RenderType", "");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = -1;
        }

        private static void toFadeMode(Material material)
        {
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
    }
}