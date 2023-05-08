using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Actions
{
    public class CreateShapes : Action
    {
        private GameObject createdObject;

        public override void HandleTriggerDown()
        {
            var newVertex = FlystickManager.Instance.MultiTool.transform.position;
            var mesh = createdObject.GetComponent<MeshFilter>().mesh;
            Vector3[] newVertices = new Vector3[mesh.vertexCount + 1];
            mesh.vertices.CopyTo(newVertices, 0);
            newVertices[mesh.vertexCount] = newVertex;
            mesh.vertices = newVertices;
            if (newVertices.Length >= 3)
            {
                var oldTriangles = mesh.triangles;
                int[] newTriangles = new int[3 * (mesh.vertexCount - 2)];
                for(int i = 0; i < mesh.vertexCount - 2; i++)
                {
                    newTriangles[3 * i] = mesh.vertexCount - 1;
                    newTriangles[3 * i + 1] = i;
                    newTriangles[3 * i + 2] = i + 1;
                }
                int[] allTriangles = new int[oldTriangles.Length + newTriangles.Length];
                oldTriangles.CopyTo(allTriangles, 0);
                newTriangles.CopyTo(allTriangles, oldTriangles.Length);
                mesh.triangles = allTriangles;
            }
        }

        public override void HandleTriggerUp()
        {
        }

        public override void Update()
        {
        }

        public override void Init()
        {
            createdObject = instantiateObject();
        }

        public override void Finish()
        {
            createdObject.AddComponent<MeshCollider>().sharedMesh = createdObject.GetComponent<MeshFilter>().mesh;
        }

        private GameObject instantiateObject()
        {
            GameObject newObject = new GameObject(GlobalVars.LineName);
            newObject.tag = GlobalVars.UniversalTag;
            var renderer = newObject.AddComponent<MeshRenderer>();
            renderer.material = new Material(Shader.Find("Sprites/Diffuse"));
            renderer.material.color = GameManager.Instance.CurrentColor;
            newObject.AddComponent<MeshFilter>();

            return newObject;
        }
    }
}