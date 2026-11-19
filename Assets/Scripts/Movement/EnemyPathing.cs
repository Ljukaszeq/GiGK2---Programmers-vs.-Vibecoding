using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [Header("Ustawienia Wroga")]
    public Transform[] waypoints; // Tablica punktów trasy
    public float moveSpeed = 3f;  // Prędkość poruszania się
    public float rotationSpeed = 5f; // Jak szybko wróg się obraca

    private int currentWaypointIndex = 0;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    void Update()
    {
        if (waypoints.Length == 0 || currentWaypointIndex >= waypoints.Length)
        {
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // 1. Poruszanie się w stronę celu
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // 2. Obracanie się w stronę celu
        Vector3 direction = targetWaypoint.position - transform.position;
        direction.y = 0; // Blokujemy pochylanie w pionie
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 3. Sprawdzanie, czy wróg dotarł do punktu
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            currentWaypointIndex++;

            // Jeśli dotarliśmy do ostatniego waypointa, spróbujmy skierować wroga bezpośrednio na bazę (jeśli jest na scenie)
            if (currentWaypointIndex >= waypoints.Length)
            {
                GameObject baseObj = GameObject.FindWithTag("Baza"); // Szukamy bazy po tagu
                if (baseObj != null)
                {
                    // Możemy dodać bazę jako ostatni cel lub obsłużyć to w OnTrigger w bazie
                }
            }
        }
    }
}