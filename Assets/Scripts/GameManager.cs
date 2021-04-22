using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject restartMenu;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI distanceText;
    public GameObject[] rockPrefabs;
    public GameObject logPrefab;
    public GameObject repairPrefab;
    public bool isGameActive;
    public int maxHealth;
    [HideInInspector] public int playerHealth;

    private float distance;
    private float xSpawnRange = 8.0f;
    private float rockDelay = 2;
    private float logDelay = 7;
    private float repairDelay = 30;

    public void StartGame(int difficulty)
    {
        rockDelay /= difficulty;
        playerHealth = maxHealth;
        isGameActive = true;
        startMenu.gameObject.SetActive(false);
        livesText.gameObject.SetActive(true);
        livesText.text = "Lives: " + playerHealth + "/" + maxHealth;
        distanceText.gameObject.SetActive(true);
        StartCoroutine(CountMeters());
        StartCoroutine(SpawnRock());
        StartCoroutine(SpawnLog());
        StartCoroutine(SpawnRepair());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        restartMenu.gameObject.SetActive(true);
    }

    IEnumerator CountMeters()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            distance += 5;
            distanceText.text = distance + " m";
        }
    }

    IEnumerator SpawnRock()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(rockDelay);
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);
            int randomIndex = Random.Range(0, rockPrefabs.Length);

            Vector3 spawnPos = new Vector3(randomX, 20, 10);

            Instantiate(rockPrefabs[randomIndex], spawnPos, Quaternion.Euler(new Vector3(0, Random.Range(-60, 60), 0)));
        }
    }

    IEnumerator SpawnLog()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(logDelay);
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);

            Vector3 spawnPos = new Vector3(randomX, 11, 17);

            Instantiate(logPrefab, spawnPos, logPrefab.transform.rotation);
        }
    }

    IEnumerator SpawnRepair()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(repairDelay);
            float randomX = Random.Range(-xSpawnRange, xSpawnRange);

            Vector3 spawnPos = new Vector3(randomX, 12, 17);

            Instantiate(repairPrefab, spawnPos, repairPrefab.transform.rotation);
        }
    }
}
