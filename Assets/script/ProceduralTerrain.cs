using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class ProceduralTerrain : MonoBehaviour
{
    [Header("Terrain Settings")]
    public int width = 100;
    public int depth = 100;
    public float scale = 10f;
    public float heightMultiplier = 5f;
    public int seed = 0;

    [Header("Decoration Settings")]
    public GameObject[] rockPrefabs;
    public GameObject[] cactusPrefabs;
    public GameObject[] ruinPrefabs;
    public GameObject[] clearobjectprefab;
    public int objectCount = 50;

    [Header("Weapon Settings")]
    public GameObject[] weaponPrefabs;
    public Camera playerCamera;
    private GameObject spawnedWeapon;


    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public int minEnemies = 3;
    public int maxEnemies = 10;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private Mesh mesh;
    private Vector3[] vertices;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private GameObject wallParent;

    void Start()
    {
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        // 古いオブジェクト削除
        foreach (var obj in spawnedObjects)
            Destroy(obj);
        spawnedObjects.Clear();

        // メッシュ生成
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        System.Random prng = new System.Random(seed);
        float offsetX = prng.Next(-100000, 100000);
        float offsetZ = prng.Next(-100000, 100000);

        vertices = new Vector3[(width + 1) * (depth + 1)];

        for (int z = 0, i = 0; z <= depth; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                float y = Mathf.PerlinNoise((x + offsetX) / scale, (z + offsetZ) / scale) * heightMultiplier;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        int[] triangles = new int[width * depth * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + width + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + width + 1;
                triangles[tris + 5] = vert + width + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = mesh;

        // オブジェクト配置
        SpawnObjects();
        SpawnEnemies(FindAnyObjectByType<PlayerController>().transform);
        SpawnWeapon();
        GenerateWalls();
    }

    void SpawnObjects()
    {
        bool spawnClear = (Random.value < 0.2f); // 20％で true
        bool clearSpawned = false;

        for (int i = 0; i < objectCount; i++)
        {
            float x = Random.Range(0, width);
            float z = Random.Range(0, depth);
            float y = GetHeightAtPosition(x, z);
            float yOffset = 0.5f;
            Vector3 pos = new Vector3(x, y + yOffset, z) + transform.position;

            GameObject prefab = null;

            // まだスポーンしていない ＆ 20%判定に当たっているときだけ
            if (spawnClear && !clearSpawned)
            {
                prefab = clearobjectprefab[Random.Range(0, clearobjectprefab.Length)];
                clearSpawned = true;
                Debug.Log("Clear object spawned: \" + prefab.name");
            }
            else
            {
                // 他のオブジェクト
                float rand = Random.value;
                if (rand < 0.6f && rockPrefabs.Length > 0)
                    prefab = rockPrefabs[Random.Range(0, rockPrefabs.Length)];
                else if (rand < 0.85f && cactusPrefabs.Length > 0)
                    prefab = cactusPrefabs[Random.Range(0, cactusPrefabs.Length)];
                else if (ruinPrefabs.Length > 0)
                    prefab = ruinPrefabs[Random.Range(0, ruinPrefabs.Length)];
            }

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 360), 0), transform);
                float scale = Random.Range(0.8f, 1.5f);
                obj.transform.localScale *= scale;

                spawnedObjects.Add(obj);
            }
        }
    }



    public void SpawnEnemies(Transform player)
    {
        // 古い敵を消す
        foreach (var e in spawnedEnemies)
            Destroy(e);
        spawnedEnemies.Clear();

        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            float x = Random.Range(0, width);
            float z = Random.Range(0, depth);
            float y = GetHeightAtPosition(x, z) + 0.5f;

            Vector3 spawnPos = new Vector3(x, y, z) + transform.position;

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            var ai = enemy.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.player = player;
                ai.terrain = this;
            }

            
            spawnedEnemies.Add(enemy);
        }
    }

    public float GetHeightAtPosition(float x, float z)
    {
        // 範囲外ならClamp
        x = Mathf.Clamp(x, 0, width);
        z = Mathf.Clamp(z, 0, depth);

        // 4頂点補間
        int x0 = Mathf.FloorToInt(x);
        int z0 = Mathf.FloorToInt(z);
        int x1 = Mathf.Min(x0 + 1, width);
        int z1 = Mathf.Min(z0 + 1, depth);

        float tx = x - x0;
        float tz = z - z0;

        float y00 = vertices[z0 * (width + 1) + x0].y;
        float y10 = vertices[z0 * (width + 1) + x1].y;
        float y01 = vertices[z1 * (width + 1) + x0].y;
        float y11 = vertices[z1 * (width + 1) + x1].y;

        float y0 = Mathf.Lerp(y00, y10, tx);
        float y1 = Mathf.Lerp(y01, y11, tx);

        return Mathf.Lerp(y0, y1, tz);
    }

    public float GetSmoothedHeightAtPosition(float x, float z, int smoothRadius = 1)
    {
        float totalHeight = 0f;
        int count = 0;

        for (int dz = -smoothRadius; dz <= smoothRadius; dz++)
        {
            for (int dx = -smoothRadius; dx <= smoothRadius; dx++)
            {
                int ix = Mathf.Clamp(Mathf.RoundToInt(x) + dx, 0, width);
                int iz = Mathf.Clamp(Mathf.RoundToInt(z) + dz, 0, depth);
                int index = iz * (width + 1) + ix;
                totalHeight += vertices[index].y;
                count++;
            }
        }

        return totalHeight / count;
    }
    void SpawnWeapon()
    {
        if (weaponPrefabs == null || weaponPrefabs.Length == 0) return;

       
        if (spawnedWeapon != null)
            Destroy(spawnedWeapon);

        
        GameObject weaponPrefab = weaponPrefabs[Random.Range(0, weaponPrefabs.Length)];

        float x = Random.Range(0, width);
        float z = Random.Range(0, depth);
        float y = GetHeightAtPosition(x, z) + 0.2f;

        Vector3 pos = new Vector3(x, y, z) + transform.position;

        spawnedWeapon = Instantiate(weaponPrefab, pos, Quaternion.identity);
    }

    void GenerateWalls()
    {
        // 既存の壁削除
        if (wallParent != null) Destroy(wallParent);
        wallParent = new GameObject("Walls");

        float thickness = 1f;     // 壁の厚み
        float height = 10f;       // 壁の高さ

        // 地形の中心座標
        Vector3 basePos = transform.position;

        // 4方向に壁を生成
        // ◆ 北（奥）
        CreateWall(new Vector3(width / 2f, height / 2f, depth) + basePos,
                   new Vector3(width, height, thickness));

        // ◆ 南（手前）
        CreateWall(new Vector3(width / 2f, height / 2f, 0) + basePos,
                   new Vector3(width, height, thickness));

        // ◆ 東（右）
        CreateWall(new Vector3(width, height / 2f, depth / 2f) + basePos,
                   new Vector3(thickness, height, depth));

        // ◆ 西（左）
        CreateWall(new Vector3(0, height / 2f, depth / 2f) + basePos,
                   new Vector3(thickness, height, depth));
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.SetParent(wallParent.transform);
        wall.transform.position = position;
        wall.transform.localScale = scale;

        wall.GetComponent<Renderer>().material.color = Color.gray; // 適当な色
    }

}
