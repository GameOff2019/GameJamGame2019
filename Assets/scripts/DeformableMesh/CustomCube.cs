using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class CustomCube : MonoBehaviour {

    public int x_size, y_size, z_size;
    public int roundness;

    private Vector3[] vertices;
    private Vector3[] normals;
    private Mesh mesh;

    void Awake() {
        GenerateMesh();
    }


    void Update() {
        
    }

    private void GenerateMesh() {
        //WaitForSeconds wait = new WaitForSeconds(0.05f);

        //Determine array size
        int corners = 8;
        int edges = 4*(x_size + y_size + z_size - 3);
        int faces = 2*((x_size - 1) * (y_size - 1) + (x_size - 1) * (z_size - 1) + (y_size - 1) * (z_size - 1));

        vertices = new Vector3[corners + edges + faces];
        normals = new Vector3[vertices.Length];

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.vertices = vertices;
        mesh.normals = normals;

        //Set vertice position
        int c = 0;
        for (int y = 0; y <= y_size; y++) {
            for (int x = 0; x < x_size; x++) {
                vertices[c++] = new Vector3(x, y, 0);
            }
            for (int z = 0; z <= z_size; z++) {
                vertices[c++] = new Vector3(x_size, y, z);
            }
            for (int x = x_size - 1; x > 0; x--) {
                vertices[c++] = new Vector3(x, y, z_size);
            }
            for (int z = z_size; z > 0; z--) {
                vertices[c++] = new Vector3(0, y, z);
            }
        }
        //add top and bottom verticies
        for (int z = 1; z < z_size; z++) {
            for (int x = 1; x < z_size; x++) {
                vertices[c++] = new Vector3(x, 0, z);
            }
        }
        for (int z = 1; z < z_size; z++) {
            for (int x = 1; x < z_size; x++) {
                vertices[c++] = new Vector3(x, y_size, z);
            }
        }

        int[] triangles = GenerateTriangles();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    /*
    private void SetVertex(int c, int x, int y, int z) {
        vertices[c] = new Vector3(x, y, z);
    }
    */

    private int[] GenerateTriangles() {
        //Generate Triangles
        int[] triangles = new int[6 * 2 * (x_size * y_size + x_size * z_size + y_size * z_size)];

        int ring_size = 2 * (x_size + z_size);

        int tri = 0, v = 0; //counter to keep track of index
        for (int y = 0; y < y_size; y++, v++) {
            for (int x = 0; x < ring_size - 1; x++, v++) {
                tri = SetQuad(triangles, tri, v, v + 1, v + ring_size, v + ring_size + 1);
            }
            tri = SetQuad(triangles, tri, v, v - ring_size + 1, v + ring_size, v + 1); //go back a ring to seamlessely wrap it around
        }

        tri = CreateTopFace(triangles, tri, ring_size);
        tri = CreateBottomFace(triangles, tri, ring_size);

        return triangles;
    }

    //This code was directly taken from https://catlikecoding.com/ 
    private int CreateTopFace(int[] triangles, int t, int ring) {   

        //first row of top face
        int v = ring * y_size;
        for (int x = 0; x < x_size - 1; x++, v++) {
            t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
        }
        t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);

        //Middle portion of top face
        int vMin = ring * (y_size + 1) - 1;
        int vMid = vMin + 1;
        int vMax = v + 2;

        for (int z = 1; z < z_size - 1; z++, vMin--, vMid++, vMax++) {
            t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + x_size - 1);
            for (int x = 1; x < x_size - 1; x++, vMid++) {
                t = SetQuad(
                    triangles, t,
                    vMid, vMid + 1, vMid + x_size - 1, vMid + x_size);
            }

            t = SetQuad(triangles, t, vMid, vMax, vMid + x_size - 1, vMax + 1);
        }

        //Last row of top face
        int vTop = vMin - 2;
        t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);
        for (int x = 1; x < x_size - 1; x++, vTop--, vMid++) {
            t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop - 1);
        }
        t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop - 1);

        return t;
    }

    private int CreateBottomFace(int[] triangles, int t, int ring) {
        int v = 1;
        int vMid = vertices.Length - (x_size - 1) * (z_size - 1);
        t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
        for (int x = 1; x < x_size - 1; x++, v++, vMid++) {
            t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
        }
        t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);

        int vMin = ring - 2;
        vMid -= x_size - 2;
        int vMax = v + 2;

        for (int z = 1; z < z_size - 1; z++, vMin--, vMid++, vMax++) {
            t = SetQuad(triangles, t, vMin, vMid + x_size - 1, vMin + 1, vMid);
            for (int x = 1; x < x_size - 1; x++, vMid++) {
                t = SetQuad(
                    triangles, t,
                    vMid + x_size - 1, vMid + x_size, vMid, vMid + 1);
            }
            t = SetQuad(triangles, t, vMid + x_size - 1, vMax + 1, vMid, vMax);
        }

        int vTop = vMin - 1;
        t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
        for (int x = 1; x < x_size - 1; x++, vTop--, vMid++) {
            t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
        }
        t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);

        return t;
    }

    private static int SetQuad(int[] triangles, int tri, int v00, int v10, int v01, int v11) {
        //triangle1
        triangles[tri] = v00;
        triangles[tri + 1] = v01;
        triangles[tri + 2] = v10;

        //triangle2
        triangles[tri + 3] = v10;
        triangles[tri + 4] = v01;
        triangles[tri + 5] = v11;
        return tri + 6;
    }

    private void OnDrawGizmos() {
        if (vertices == null) { return; }
        Vector3 t_pos = transform.position;
        for (int i = 0; i < vertices.Length; i++) {
            Vector3 pos = vertices[i];
            Vector3 norm = normals[i];
            //if (pos == null) { continue; }
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(t_pos.x + pos.x, t_pos.y + pos.y, t_pos.z + pos.z), 0.05f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(pos, norm);
        }
    }
}
