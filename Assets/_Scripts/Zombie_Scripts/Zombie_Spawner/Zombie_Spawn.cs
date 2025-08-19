using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zombie_Spawn : MonoBehaviour
{
    [Header("Setup")]
    public GameObject[] zombiePrefabs; // Kéo 4 prefab vào Inspector
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int startZombieCount = 3;        // Số zombie ở wave 1
    public float timeBetweenWaves = 5f;     // Thời gian nghỉ giữa các wave
    public float spawnDelay = 0.5f;         // Delay giữa các zombie spawn trong cùng 1 wave

    [Header("UI")]
    public TextMeshProUGUI waveText;        // UI Text hiển thị wave

    private int currentWave = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        currentWave++;
        int zombieToSpawn = startZombieCount + (currentWave - 1) * 2;

        // Hiển thị thông báo wave
        waveText.text = $"WAVE {currentWave}";
        waveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);  // Hiển thị 2 giây
        waveText.gameObject.SetActive(false);

        // Bắt đầu spawn zombie
        isSpawning = true;

        for (int i = 0; i < zombieToSpawn; i++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnDelay);
        }

        isSpawning = false;

        // Đợi cho tới khi tất cả zombie bị tiêu diệt
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Zombie").Length == 0);

        // Nghỉ trước khi sang wave tiếp theo
        yield return new WaitForSeconds(timeBetweenWaves);

        StartCoroutine(StartNextWave());
    }

    void SpawnZombie()
    {
        if (spawnPoints.Length == 0 || zombiePrefabs.Length == 0) return;

        // Chọn vị trí spawn ngẫu nhiên
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Chọn prefab zombie ngẫu nhiên
        int prefabIndex = Random.Range(0, zombiePrefabs.Length);
        GameObject selectedZombie = zombiePrefabs[prefabIndex];

        // Spawn
        GameObject zombie = Instantiate(selectedZombie, spawnPoint.position, spawnPoint.rotation);

        // Multiplier theo wave
        float multiplier = 1f + (currentWave - 1) * 0.2f;

        // Scale
        zombie.transform.localScale *= multiplier;

        // Setup máu
        ZombieHealth health = zombie.GetComponent<ZombieHealth>();
        if (health != null)
        {
            health.SetupHealthMultiplier(multiplier);
        }

        // Setup damage
        ZombieAI ai = zombie.GetComponent<ZombieAI>();
        if (ai != null)
        {
            ai.SetupDamageMultiplier(multiplier);
            // Tìm Player và gán lại
        }
    }
}
