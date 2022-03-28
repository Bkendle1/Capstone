using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoorController : MonoBehaviour
{
    [SerializeField] private float distanceToOpen;
    [SerializeField] private string levelToLoad;
    [SerializeField] private Transform doorPos;
    [SerializeField] private LayerMask whoIsPlayer;
    [SerializeField] private float height, width;
    private bool playerDetected;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        playerDetected = Physics2D.OverlapBox(doorPos.position, new Vector2(width, height), 0, whoIsPlayer);

        if (playerDetected)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(UseDoorCo());
            }
        }

        //opens or closes door depending on player pos
        if (Vector3.Distance(transform.position, PlayerHealth.instance.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        }
        else
        {
            anim.SetBool("doorOpen", false);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(doorPos.position, new Vector3(width, height, 1));
    }

}
