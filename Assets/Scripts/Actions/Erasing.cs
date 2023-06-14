using Assets.Scripts.Managers;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class Erasing : Action
    {
        private bool erasing = false;
        private GameObject[] gameObjects;

        public override void HandleTriggerDown()
        {
            erasing = true;
            gameObjects = GameObject.FindGameObjectsWithTag(GlobalVars.UniversalTag);
        }

        public override void HandleTriggerUp()
        {
            erasing = false;
        }

        public override void Update()
        {
            if (erasing)
            {
                var multiTool = FlystickManager.Instance.MultiTool;

                Collider[] hitColliders = Physics.OverlapBox(multiTool.transform.position, multiTool.GetComponent<Renderer>().bounds.size * 0.75f);
                var collidingObjects = from obj in hitColliders
                                       where obj.gameObject.tag == GlobalVars.UniversalTag
                                       select obj.gameObject;

                foreach (GameObject objToDelete in collidingObjects)
                {
                    Object.Destroy(objToDelete);
                }
            }
        }

        public override void Init()
        {
            // Nothing happens
        }

        public override void Finish()
        {
            // Nothing happens
        }
    }
}