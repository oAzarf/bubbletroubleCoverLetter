using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    public float length=1;
    public int numberOfVertices;
    public Material mat;
    private int[] trianglesArray;
    private List<int> triangles;

    // Use this for initialization
    void Start () {

       Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[numberOfVertices];
        
        var teta = 2 * Mathf.PI / numberOfVertices ;
        float spot= teta;
        int quadranteSin=1;
        int quadranteCos=1;
        for (int i = 0; i < numberOfVertices; i++)
        {
            if (spot<= Mathf.PI/2)
            {
                quadranteSin = 1;
                quadranteCos = 1;
            }
            else if (spot <= Mathf.PI)
            {
                quadranteCos = -1;
                quadranteSin = 1;
            }
            else if (spot <= Mathf.PI*3/2)
            {
                quadranteSin = -1;
                quadranteCos = -1;
            }
            else if (spot <= 2*Mathf.PI)
            {
                quadranteSin = -1;
                quadranteCos = 1;
            }
            vertices[i] = new Vector3( Mathf.Cos(spot)*length,   Mathf.Sin(spot) *length);
            spot += teta;
        }


        mesh.vertices = vertices;

        triangles = new List<int>();
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);
        int aux = 2;
        for (int i = 0; aux+1 < numberOfVertices; i++)
        {
            triangles.Add(aux);
            triangles.Add(aux+1);
            triangles.Add(0);
            aux++;
        }

        
        var iteration = (numberOfVertices - 2) * 3;
        trianglesArray = new int[iteration];
        aux = 0;
        foreach (var item in triangles)
        {
            trianglesArray[aux] = item;
            aux++;
        }

        mesh.triangles = trianglesArray ;

       




        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

    }
	


}
