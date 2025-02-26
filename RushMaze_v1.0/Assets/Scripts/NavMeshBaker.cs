using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class NavMeshBaker : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    public MazeGenerator mazeGenerator;

    void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        if (navMeshSurface == null)
        {
            Debug.LogError("⚠️ NavMeshSurface chưa được gán! Hãy kéo nó vào Inspector.");
            return;
        }
    }

    public void BakeNavMesh()
    {
        Debug.Log("⏳ Đang bake lại NavMesh...");
        navMeshSurface.BuildNavMesh();
        Debug.Log("✅ NavMesh đã được bake xong!");
    }
    public NavMeshBaker navMeshBaker; // Kéo object chứa NavMeshSurface vào đây

    void Start()
    {
        void Start()
        {
            if (mazeGenerator != null)
            {
                mazeGenerator.GenerateMaze(); // Gọi GenerateMaze() sau khi đã sửa thành public
            }
            else
            {
                Debug.LogError("⚠️ MazeGenerator chưa được gán! Hãy kéo GameObject chứa MazeGenerator vào Inspector.");
            }
        }
    }

}
