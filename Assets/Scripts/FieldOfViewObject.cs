using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewObject : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private float _viewAngle = 360f;

    [SerializeField]
    private int scanRayCount = 128;

    [SerializeField]
    private float _viewDistance = 10f;

    private Mesh _mesh;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = new Mesh();

        GetComponent<MeshFilter>().mesh = _mesh;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = Vector3.zero;
        Vector3 rayOrigin = transform.TransformPoint(origin);
        float angle = 0f;
        float angleIncrease = _viewAngle / scanRayCount;

        // every triangle starts from the origin 
        // +1 for the origin
        // +1 for the closing vertex
        Vector3[] vertices = new Vector3[scanRayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[scanRayCount * 3];

        // the origin of the rays is always the first vertex of the triangles
        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;

        // scan the environment with rays from the origin
        for (int rayIndex = 0; rayIndex <= scanRayCount; rayIndex++) 
        {
            RaycastHit hitResult;

            // get the directionVector of the scanning ray
            var directionVector = Quaternion.AngleAxis(angle, transform.up) * transform.forward;

            // check when we hit an object with the ray
            if (Physics.Raycast(rayOrigin, directionVector, out hitResult, _viewDistance, _layerMask))
            {
                vertices[vertexIndex] = hitResult.point;
            }
            else
            {
                vertices[vertexIndex] = rayOrigin + directionVector * _viewDistance;
            }

            // transform back the world coordinates to local coordinates
            vertices[vertexIndex] = transform.InverseTransformPoint(vertices[vertexIndex]);

            // set the triangles when we have enough vertices
            if (rayIndex > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle += angleIncrease;
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
    }
}
