using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshCutter;
using System.Linq;

public enum GameState
{
    Waiting,
    Aiming,
    ScissorMoving,
    ScissorStop,
}

public class GameManager : MonoBehaviour
{
    [TextAreaAttribute(10, 10)] [SerializeField] string document;
    public static GameManager i;
    public GameState GameState { get; set; }

    void Awake()
    {
        i = this;
    }

    void Start()
    {
        GameState = GameState.Waiting;
    }

    public void ProportionCheck()
    {
        if (Variables.screenState != ScreenState.Game) { return; }
        CutterTarget[] cutterTargets = FindObjectsOfType<CutterTarget>();
        Variables.resultDatas = new ResultData[cutterTargets.Length];
        float[] volumes = cutterTargets.Select(c => c.GetComponent<MeshFilter>()).Select(m => VolumeOfMesh(m.mesh)).ToArray();
        //for (int i = 0; i < cutterTargets.Length; i++)
        //{
        //   cutterTargets[i].Rigidbody.SetDensity(1f);
        //}
        //float[] volumes = cutterTargets.Select(c => c.Rigidbody).Select(r => r.mass).ToArray();

        float volumeSum = volumes.Sum(v => v);

        for (int i = 0; i < cutterTargets.Length; i++)
        {
            Variables.resultDatas[i] = new ResultData
            {
                proportion = volumes[i] / volumeSum * 100f,
                worldPos = cutterTargets[i].Renderer.bounds.center,
            };
            cutterTargets[i].Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log(cutterTargets[i].name + " => " + Variables.resultDatas[i].proportion + "%");
        }
        Variables.screenState = ScreenState.Result;
    }

    /// <summary>
    /// Unity メッシュの体積を計算する
    /// https://sleepygamersmemo.blogspot.com/2019/02/unity-calculate-mesh-volume.html
    /// </summary>
    /// <param name="mesh"></param>
    /// <returns></returns>
    float VolumeOfMesh(Mesh mesh)
    {
        if (mesh == null) return 0;

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        float volume = 0;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume);
    }

    float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }
}
