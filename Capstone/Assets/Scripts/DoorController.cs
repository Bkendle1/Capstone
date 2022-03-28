using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [SerializeField] private float distanceToOpen;
    [SerializeField] private string levelToLoad;
    private Animator anim;     

    private void Start()
    {        
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //opens or closes door depending on player pos
        if (Vector3.Distance(transform.position, PlayerHealth.instance.transform.position) < distanceToOpen)
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

    public IEnumerator UseDoorCo()
    {
        yield return new WaitForSeconds(.5f);
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds(1.5f);
        UIController.instance.FadeFromBlack();
        SceneManager.LoadScene(levelToLoad);
    }
   
}
