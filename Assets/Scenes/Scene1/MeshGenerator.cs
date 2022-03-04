using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    [SerializeField][Range(0, 5f)] float _timeSpeed;
    [SerializeField] int _row;
    [SerializeField] int _col;
    Mesh _mesh;
    MeshFilter _meshFilter;
    Vector3[] _vertices;
    int[] _triangles;

    void Start()
    {
        _mesh = new Mesh();
        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
        //_meshFilter.mesh.SetIndices(_meshFilter.mesh.GetIndices(0), MeshTopology.LineStrip, 0);
        //GetComponent<MeshRenderer>().material.color = Color.white;
        _vertices = new Vector3[(_row + 1) * (_col + 1)];
        _triangles = new int[_row * _col * 6];
    }

    void Update()
    {
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        for (int i = 0; i <= _row; i++)
        {
            for (int j = 0; j <= _col; j++)
            {
                _vertices[i*(_col+1)+j] = new Vector3(j, Mathf.PerlinNoise(i * .3f + Time.time * _timeSpeed, j * .3f + Time.time * _timeSpeed) * 2f, i);
            }
        }
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _col; j++)
            {
                _triangles[(i * _col + j) * 6] = j + i * (_col + 1);
                _triangles[(i * _col + j) * 6 + 1] = j + (i + 1) * (_col + 1);
                _triangles[(i * _col + j) * 6 + 2] = j + i * (_col + 1) + 1;
                _triangles[(i * _col + j) * 6 + 3] = j + (i + 1) * (_col + 1);
                _triangles[(i * _col + j) * 6 + 4] = j + (i + 1) * (_col + 1) + 1;
                _triangles[(i * _col + j) * 6 + 5] = j + i * (_col + 1) + 1;
            }
        }
    }

    void UpdateMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;

        //_meshFilter.mesh.SetIndices(_meshFilter.mesh.GetIndices(0), MeshTopology.LineStrip, 0);

        _mesh.RecalculateNormals();
    }
}
