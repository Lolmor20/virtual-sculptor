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
			Pyramid, 
			Cone, 
			Wedge
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

			case ObjectType.Wedge:
				gameObject.name = "Wedge";

				vertices = new Vector3[6] {
					new Vector3 (0, 0, 0),
					new Vector3 (2, 0, 0), 
					new Vector3 (1, 0, 2),
					new Vector3 (0, 3, 0),
					new Vector3 (2, 3, 0), 
					new Vector3 (1, 3, 2),
				};

				triangles = new int[8 * 3] {
					0, 1, 2,
					5, 4, 3,
					0, 4, 1, 
					3, 4, 0, 
					5, 2, 1, 
					1, 4, 5, 
					3, 0, 2,
					2, 5, 3
				};

				break;

			case ObjectType.Cone:

				gameObject.name = "Cone";

				int subdivisions = 20;
				int radius = 1;
				int height = 3;

				vertices = new Vector3[subdivisions + 2];

				triangles = new int[(subdivisions * 2) * 3];

				vertices [0] = Vector3.zero;
				for (int i = 0, n = subdivisions - 1; i < subdivisions; i++) {
					float ratio = (float)i / n;
					float r = ratio * (Mathf.PI * 2f);
					float x = Mathf.Cos (r) * radius;
					float z = Mathf.Sin (r) * radius;
					vertices [i + 1] = new Vector3 (x, 0f, z);
				}
				vertices [subdivisions + 1] = new Vector3 (0f, height, 0f);

				for (int i = 0, n = subdivisions - 1; i < n; i++) {
					int offset = i * 3;
					triangles [offset] = 0; 
					triangles [offset + 1] = i + 1; 
					triangles [offset + 2] = i + 2; 
				}

				int bottomOffset = subdivisions * 3;
				for (int i = 0, n = subdivisions - 1; i < n; i++) {
					int offset = i * 3 + bottomOffset;
					triangles [offset] = i + 1; 
					triangles [offset + 1] = subdivisions + 1; 
					triangles [offset + 2] = i + 2; 
				}

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