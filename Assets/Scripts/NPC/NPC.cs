using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public float speed;
    private int index;
    private float initialSpeed;
    private Animator anim;

    public List<Transform> paths = new List<Transform>();
    void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogueControl.instance.IsShowing)
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, paths[index].position) < 0.1)
        {
            if(index < paths.Count -1)
            {
                //seguir index na ordem [1,2,3]
                //index++;

                //andar aleatoriamente no index [1,3,2]
                index = Random.Range(0, paths.Count -1);
            }
            else
            {
                index = 0;
            }
        }

        Vector2 direction = paths[index].position - transform.position;

        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if(direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);

        }
    }
}
