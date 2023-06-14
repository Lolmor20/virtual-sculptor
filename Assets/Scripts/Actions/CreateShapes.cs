using UnityEngine;
using System.Collections.Generic;
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
                List<int> newTriangles = new List<int>();

                int v1 = mesh.vertexCount - 1;
                for (int i = 0; i < mesh.vertexCount - 1; i++)
                {
                    int v2 = i;
                    for (int j = i + 1; j < mesh.vertexCount - 1; j++)
                    {
                        int v3 = j;

                        newTriangles.Add(v1);
                        newTriangles.Add(v2);
                        newTriangles.Add(v3);

                        newTriangles.Add(v3);
                        newTriangles.Add(v2);
                        newTriangles.Add(v1);
                    }
                }

                int[] allTriangles = new int[oldTriangles.Length + newTriangles.Count];
                oldTriangles.CopyTo(allTriangles, 0);
                newTriangles.CopyTo(allTriangles, oldTriangles.Length);
                mesh.triangles = allTriangles;
                mesh.RecalculateNormals();

                createdObject.GetComponent<MeshCollider>().sharedMesh = mesh;

                for (int i = 0; i < newTriangles.Count; i += 3)
                {
                    if (isTriangleInsideMesh(newVertices[newTriangles[i]], newVertices[newTriangles[i + 1]], newVertices[newTriangles[i + 2]]))
                    {
                        int a = 2;
                    }
                }
            }
        }

        private bool isTriangleInsideMesh(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            var meshCollider = createdObject.GetComponent<MeshCollider>();
            var triangleNormal = Vector3.Cross(v2 - v1, v3 - v1);

            var centrePoint = (v1 + v2 + v3) / 3;

            Vector3 direction = triangleNormal;
            Ray ray = new Ray(centrePoint, centrePoint + direction);
            Debug.DrawLine(centrePoint, centrePoint + direction);
            RaycastHit hit;

            var raycastResult = Physics.Raycast(ray, out hit);

            if (!raycastResult)
            {
                return false;
            } else if (hit.collider != meshCollider)
            {
                return false;
            }

            ray = new Ray(centrePoint, centrePoint - direction);
            Debug.DrawLine(centrePoint, centrePoint - direction);

            if (Physics.Raycast(ray, out hit))
            {
                return false;
            } else if (hit.collider != meshCollider)
            {
                return false;
            }

            return true;
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
        }

        private GameObject instantiateObject()
        {
            GameObject newObject = new GameObject(GlobalVars.LineName);
            newObject.tag = GlobalVars.UniversalTag;
            var renderer = newObject.AddComponent<MeshRenderer>();
            renderer.material.color = GameManager.Instance.CurrentColor;
            newObject.AddComponent<MeshFilter>();
            newObject.AddComponent<MeshCollider>();
          //  newObject.AddComponent<LineRenderer>();

            return newObject;
        }
    }
}