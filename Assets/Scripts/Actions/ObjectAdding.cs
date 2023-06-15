using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    public class ObjectAdding : Action
    {
		public enum ObjectType
		{
			Sphere,
			Cube, 
			Capsule,
			Cylinder, 
			Triangle,
			Square, 
			Pyramid
		}


        private ObjectType type { get; set; }
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
            newObject = CreateObject(type);
            newObject.name = GlobalVars.PrimitiveObjectName + type.ToString();
            newObject.tag = GlobalVars.UniversalTag;
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

        public void SetObjectType(ObjectType objectType)
        {
            this.type = objectType;
        }

		private GameObject CreateObject(ObjectType objectType)
		{
			switch (type) 
			{
			case ObjectType.Capsule:
				return GameObject.CreatePrimitive (PrimitiveType.Capsule);
			case ObjectType.Cube:
				return GameObject.CreatePrimitive (PrimitiveType.Cube);
			case ObjectType.Sphere:
				return GameObject.CreatePrimitive (PrimitiveType.Sphere);
			case ObjectType.Cylinder:
				return GameObject.CreatePrimitive (PrimitiveType.Cylinder);
			default:
				return CreateCustomObject (type);

			}
				

		}

		private GameObject CreateCustomObject(ObjectType type)
		{
			Mesh mesh = new Mesh();
			Vector3[] vertices;
			int[] triangles;
			GameObject gameObject = new GameObject();

			switch (type)
			{
			case ObjectType.Triangle:
				
				gameObject.name = "Triangle";

				vertices = new Vector3[3] {
					new Vector3 (0, 0, 0),
					new Vector3 (2, 0, 0),
					new Vector3 (1, 2, 0)
				};

				triangles = new int[3] {
					2, 1, 0
				};

				break;

			case ObjectType.Square:

				gameObject.name = "Square";

				vertices = new Vector3[4] {
					new Vector3 (0, 0, 0),
					new Vector3 (2, 0, 0),
					new Vector3 (2, 2, 0),
					new Vector3 (0, 2, 0)
				};

				triangles = new int[6] {
					2, 1, 0,
					3, 2, 0
				};

				break;

			case ObjectType.Pyramid:

				gameObject.name = "Pyramid";

				vertices = new Vector3[5] {
					new Vector3 (0, 0, 0),
					new Vector3 (2, 0, 0),
					new Vector3 (2, 0, 2), 
					new Vector3 (0, 0, 2),
					new Vector3 (1, 2, 1)
				};

				triangles = new int[6 * 3] {
					0, 2, 1,
					3, 2, 0,
					4, 0, 1,
					4, 1, 2,
					4, 2, 3,
					4, 3, 0
				};

				break;

			default:
				gameObject.name = "UndefinedCustomObject";
				vertices = new Vector3[0];
				triangles = new int[0];
				break;

			}
			 
			mesh.vertices = vertices;
			mesh.triangles = triangles;

			gameObject.AddComponent<MeshRenderer> ();
			gameObject.AddComponent<MeshFilter> ();
			gameObject.GetComponent<MeshFilter> ().mesh = mesh;
			gameObject.AddComponent<MeshCollider>().sharedMesh = mesh;

			return gameObject;

		}
    }
}