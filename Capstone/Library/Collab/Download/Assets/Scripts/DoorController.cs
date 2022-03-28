using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float distanceToOpen;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private string levelToLoad;
    private Animator anim;
    
    private PlayerController player;

        

    private void Start()
    {        
        player = PlayerHealth.instance.GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //is the distance between the player and door less than the distance needed to open the door?
        if(Vector3.Distance(transform.position, player.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        } else
        {
            anim.SetBool("doorOpen", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(UseDoorCo());

        }
    }

    IEnumerator UseDoorCo()
    {
        yield return new WaitForSeconds(.5f);
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        UIController.instance.FadeFromBlack();

        SceneManager.LoadScene(levelToLoad);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToOpen);
    }
}
