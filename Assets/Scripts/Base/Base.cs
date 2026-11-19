using UnityEngine;

public class Base : MonoBehaviour
{
    public float checkDistance = 1.5f;
    private bool isGameOver = false; // Flaga zabezpieczająca przed spamem

    void Update()
    {
        if (isGameOver) return; // Jeśli gra już się skończyła, nic nie rób

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance <= checkDistance)
                {
                    isGameOver = true;
                    Debug.Log("KONIEC GRY! Wróg dotarł do bazy.");
                    Time.timeScale = 0f; // Zatrzymanie gry
                    break;
                }
            }
        }
    }
}