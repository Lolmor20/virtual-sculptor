using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ObjectAdding : Action
    {
        private PrimitiveType type { get; set; }
        private readonly GameObject tool = FlystickManager.Instance.MultiTool;
        GameObject newObject;
        public float objectSize { get; set; } = GameManager.Instance.MinObjectSize;
        private readonly float transformationScale = 1.5f;

        public override void Init()
        {
            // Nothing
        }

        public override void Finish()
        {
            // Nothing
        }

        public override void Update()
        {
            // Nothing
        }

        public override void HandleTriggerDown()
        {
            newObject = GameObject.CreatePrimitive(type);
            newObject.name = GameManager.PrimitiveObjectName + type.ToString();
            newObject.tag = GameManager.UniversalTag;
            newObject.GetComponent<Renderer>().material.color = GameManager.Instance.CurrentColor;
            newObject.transform.localScale = (new Vector3(objectSize, objectSize, objectSize));
            newObject.transform.position = tool.transform.position + tool.transform.forward * transformationScale;
            newObject.transform.rotation = tool.transform.rotation;
            newObject.transform.SetParent(tool.transform);
        }

        public override void HandleTriggerUp()
        {
            if (newObject != null)
            {
                newObject.transform.SetParent(null);
            }
        }

        public void SetObjectType(PrimitiveType objectType)
        {
            this.type = objectType;
        }
    }
}