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

        public override void Init()
        {
            // Nothing
        }

        public override void Finish()
        {
            if (newObject != null && newObject.transform.parent != null)
            {
                GameObject.Destroy(newObject);
            }
        }

        public override void Update()
        {
            // Nothing
        }

        public override void HandleTriggerDown()
        {
            newObject = GameObject.CreatePrimitive(type);
            newObject.name = GlobalVars.PrimitiveObjectName + type.ToString();
            newObject.tag = GlobalVars.UniversalTag;
            //newObject.GetComponent<Renderer>().material= new Material(Shader.Find("Sprites/Diffuse"));
            newObject.GetComponent<Renderer>().material.color = GameManager.Instance.CurrentColor;
            newObject.transform.localScale = (new Vector3(objectSize, objectSize, objectSize));
            newObject.transform.position = tool.transform.position + tool.transform.forward * 1.5f;
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