using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    public float enemiesViewRange;
    public float itemsViewRange;
    private GameObject camHUD;

    RaycastHit hit;
    Ray ray;

    void Awake()
    {
        camHUD = FindObjectOfType<GameObject>(includeInactive:true);
    }

    void Update()
    {
        Score.IsFilming = false;
        RaycastEnemies();
    }

    private void RaycastEnemies()
    {
        if (!PlayerUI.CanFilm) // CanFilm est true lorsque le joueur a activé la caméra (voir PlayerUI.cs)
            return;

        for (int j = -10; j < 10; j+=5)
        {
            for (int i = -20; i < 20; i+=3)
            {
                ray = new Ray(transform.position, transform.forward * enemiesViewRange + transform.right * i + transform.up * j);
                Debug.DrawRay(transform.position, transform.forward * enemiesViewRange + transform.right * i + transform.up * j, Color.green);


                if (Physics.Raycast(ray, out hit, enemiesViewRange) && hit.collider.CompareTag("Enemy"))
                {
                    Score.IsFilming = true;
                    return;
                }
            }
        }
    }
}
