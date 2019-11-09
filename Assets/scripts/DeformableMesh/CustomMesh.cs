using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class CustomMesh : MonoBehaviour {

    public int x_size, y_size;
    //public float x_cell_size, y_cell_size; // no need for this as we can just use tran

    private Vector3[] vertices;
    private Mesh mesh;

    void Start() {
        GenerateMesh();

    }

    void Update() {
        
    }

    private void GenerateMesh() {
        //Generate mesh array
        vertices = new Vector3[(x_size + 1) * (y_size + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        for (int i = 0, y = 0; y <= y_size; y++) {
            for (int x = 0; x <= x_size; x++, i++) {
                vertices[i] = new Vector3(x, y, Random.Range(-0.2f,0.2f));
                uv[i] = new Vector2((float)x/x_size, (float)y/y_size);
            }
        }

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = vertices;

        
        //Generate triangles
        int[] triangles = new int[x_size * y_size * 6];
        for (int y = 0, tri = 0, count = 0; y < y_size; y++, count++) { 

            for (int x = 0; x < x_size; x++, tri += 6, count++) {
                //triangle1
                triangles[tri] = count;
                triangles[tri + 1] = x_size + count + 1;
                triangles[tri + 2] = count + 1;

                //triangle2
                triangles[tri + 3] = count + 1;
                triangles[tri + 4] = x_size + count + 1;
                triangles[tri + 5] = x_size + count + 2;
            }
        }

        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = uv;
        


    }

    private void OnDrawGizmos() {
        if (vertices == null) { return; }
        Vector3 t_pos = transform.position;
        Gizmos.color = Color.blue;
        foreach (Vector3 pos in vertices) {
            if (pos == null) { continue; }
            Gizmos.DrawSphere(new Vector3(t_pos.x+pos.x,t_pos.y+pos.y,t_pos.z+pos.z), 0.05f);
        }
    }
}
