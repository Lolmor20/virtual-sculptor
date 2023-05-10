using UnityEngine;
using System.Linq;
using Assets.Scripts.Managers;
using Parabox.CSG;
using System;

namespace Assets.Scripts.Actions
{
    public class Sculpting : Action
    {
        private readonly GameObject multiTool = FlystickManager.Instance.MultiTool;
        private BooleanOperation currentOperation = BooleanOperation.SUBTRACT;
        private GameObject[] gameObjects;
        private bool sculpting = false;

        private enum BooleanOperation
        {
            UNION,
            INTERSECT,
            SUBTRACT
        }

        public override void HandleTriggerDown()
        {
            sculpting = true;
        }

        public override void HandleTriggerUp()
        {
            sculpting = false;
        }

        public override void Update()
        {
            if (sculpting)
            {
                Bounds multiToolBounds = multiTool.GetComponent<Collider>().bounds;
                var intersectingObjects = from item
                                          in gameObjects
                                          where ((item.GetComponent<Collider>() != null) && (multiToolBounds.Intersects(item.GetComponent<Collider>().bounds)))
                                          select item;
                var list = intersectingObjects.ToList();

                foreach (var obj in list)
                {
                    try
                    {
                        Model result = CSG.Subtract(obj, multiTool);
                        obj.GetComponent<MeshFilter>().mesh = result.mesh;
                        
                        obj.GetComponent<MeshCollider>().sharedMesh = result.mesh;
                    } catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
        }

        public override void Init()
        {
            gameObjects = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
        }

        public override void Finish()
        {
        }
    }
}