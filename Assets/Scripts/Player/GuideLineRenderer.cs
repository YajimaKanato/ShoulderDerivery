using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GuideLineRenderer : MonoBehaviour
{
    [SerializeField] AnimationCurve _widthCurve = AnimationCurve.Linear(0, 1, 1, 1);
    [SerializeField] float _width = 0.4f;
    Mesh _mesh;
    MeshRenderer _meshRenderer;
    Vector3[] _vertices;
    Vector2[] _uv;
    int[] _triangles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _mesh = new Mesh();
        _mesh.name = "Guide";
        _meshRenderer = GetComponent<MeshRenderer>();

        GetComponent<MeshFilter>().mesh = _mesh;
    }

    /// <summary>
    /// 頂点の計算をするメソッド
    /// </summary>
    /// <param name="points">頂点の大まかな位置の配列</param>
    public void SetPoints(IReadOnlyList<Vector3> points)
    {
        // 一つの頂点につき幅を取るための二つの頂点を生成する
        int vertexCount = points.Count * 2;

        // 幅を持った時の頂点数に応じて配列を生成
        _vertices = new Vector3[vertexCount];
        _uv = new Vector2[vertexCount];

        // 頂点とuvを計算
        for (int i = 0; i < points.Count; i++)
        {
            // 現在の頂点をローカル座標に直す
            var local = transform.InverseTransformPoint(points[i]);

            // 頂点の流れ全体のうちどのくらいにいるかを計算
            float t = i / (points.Count - 1f);
            // その割合に合わせた幅を計算
            float currentWidth = _widthCurve.Evaluate(t) * _width;

            // 幅を取ったときの座標を計算
            _vertices[i * 2] = local - Vector3.up * currentWidth * 0.5f;
            _vertices[i * 2 + 1] = local + Vector3.up * currentWidth * 0.5f;

            // 幅を取ったときの頂点のuvを計算
            _uv[i * 2] = new Vector2(t, 0);
            _uv[i * 2 + 1] = new Vector2(t, 1);
        }

        // 三角形の頂点を計算
        _triangles = new int[(points.Count - 1) * 6];
        int index = 0;
        for (int i = 0; i < points.Count - 1; i++)
        {
            // 一つの頂点につき２つの三角形を生成
            int v = i * 2;

            _triangles[index++] = v;
            _triangles[index++] = v + 2;
            _triangles[index++] = v + 1;

            _triangles[index++] = v + 1;
            _triangles[index++] = v + 2;
            _triangles[index++] = v + 3;
        }

        EnableMesh();
    }

    /// <summary>
    /// メッシュの描画を止めるメソッド
    /// </summary>
    public void DisableMesh()
    {
        _meshRenderer.enabled = false;
    }

    /// <summary>
    /// メッシュを描画するメソッド
    /// </summary>
    void EnableMesh()
    {
        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;

        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();

        _meshRenderer.enabled = true;
    }
}
