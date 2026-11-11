using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [Header("Ustawienia Wroga")]
    public Transform[] waypoints; // Tablica punktów, po których idzie wróg
    public float moveSpeed = 3f;  // Prędkość poruszania się
    public float rotationSpeed = 5f; // Jak szybko wróg się obraca w stronę kolejnego punktu

    private int currentWaypointIndex = 0; // Indeks aktualnego celu

    void Start()
    {
        // Ustawiamy wroga na pozycji startowej (pierwszy punkt)
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        // Jeśli wróg dotarł do końca trasy (nie ma już więcej punktów), kończymy
        if (currentWaypointIndex >= waypoints.Length)
        {
            return;
        }

        // Pobieramy pozycję aktualnego celu
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // 1. Poruszanie się w stronę celu (bez wpływu fizyki)
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // 2. Obracanie się w stronę celu (żeby "patrzył" przodem)
        Vector3 direction = targetWaypoint.position - transform.position;
        if (direction != Vector3.zero) // Zapobiega błędom, gdyby wektory były zerowe
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 3. Sprawdzanie, czy wróg dotarł do celu (z małym marginesem błędu)
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++; // Przechodzimy do kolejnego punktu
        }
    }
}