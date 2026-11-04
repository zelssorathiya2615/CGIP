// using UnityEngine;

// public class SaturnRings : MonoBehaviour
// {
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }



using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SaturnRings : MonoBehaviour
{
    [Header("Ring Dimensions")]
    [Min(1.01f)]
    public float innerRadius = 1.25f;

    [Min(1.02f)]
    public float outerRadius = 2.27f;

    [Range(64, 512)]
    public int segments = 256;

    private void OnEnable()
    {
        GenerateRings();
    }

    private void OnValidate()
    {
        innerRadius = Mathf.Max(1.01f, innerRadius);
        outerRadius = Mathf.Max(innerRadius + 0.01f, outerRadius);
        segments = Mathf.Max(64, segments);

        GenerateRings();
    }

    private void GenerateRings()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Saturn Ring Mesh";

        Vector3[] vertices = new Vector3[(segments + 1) * 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[segments * 6];

        for (int i = 0; i <= segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2f;

            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);

            int index = i * 2;

            vertices[index] =
                new Vector3(x * innerRadius, 0f, z * innerRadius);

            vertices[index + 1] =
                new Vector3(x * outerRadius, 0f, z * outerRadius);

            uv[index] = new Vector2((float)i / segments, 0f);
            uv[index + 1] = new Vector2((float)i / segments, 1f);

            if (i < segments)
            {
                int t = i * 6;

                // triangles[t] = index;
                // triangles[t + 1] = index + 1;
                // triangles[t + 2] = index + 2;

                // triangles[t + 3] = index + 1;
                // triangles[t + 4] = index + 3;
                // triangles[t + 5] = index + 2;


                triangles[t] = index;
                triangles[t + 1] = index + 2;
                triangles[t + 2] = index + 1;

                triangles[t + 3] = index + 1;
                triangles[t + 4] = index + 2;
                triangles[t + 5] = index + 3;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        MeshFilter filter = GetComponent<MeshFilter>();
        filter.sharedMesh = mesh;
    }
}
