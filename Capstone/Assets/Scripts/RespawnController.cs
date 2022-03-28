using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;

    private Vector3 respawnPoint;
    [SerializeField] private float waitToRespawn;
    private GameObject player;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        player = PlayerHealth.instance.gameObject;
        respawnPoint = player.transform.position;

    }
    public void SetSpawn(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    IEnumerator RespawnCo()
    {
        player.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        player.transform.position = respawnPoint;
        player.SetActive(true);
        PlayerHealth.instance.RefillHealth();
    }
}
