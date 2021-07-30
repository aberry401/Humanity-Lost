


using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class AI : Enemy
{
    public Transform player;
    public int index;
    public float hp = 100;
    public float attackRange;
    public float attackTime;
    public bool runAway;
    public float runawayDist;
    public float sightDist;
    public float giveUpBuffer;
    public float speed = 7;
    public float dist;
    public float hearDist;

    private float acel;
    public NavMeshAgent agent;
    public bool chasingTarget;
    public Vector3 target;
    public bool idle;
    public bool attack;
    public bool canSee;
    public bool attackMode;
    

        

    void Start()
    {
        attack = false;
        target = transform.position;
        
        chasingTarget = false;
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(transform.position);
        player = GameObject.FindGameObjectWithTag("spawnsystem").GetComponent<SpawnScript>().currentTarget;

        acel = agent.acceleration;

    }

    void Update()
    {
        NavMeshHit hit;
        if (!agent.Raycast(target, out hit) || dist < hearDist)
        {
            canSee = true;
        } else
        {
            canSee = false;
        }










        attackMode = false;

        dist = Vector3.Distance(transform.position, player.position);


        if (Vector3.Distance(transform.position, player.position) < attackRange && Vector3.Distance(transform.position, player.position) > runawayDist)
        {
            
            
            if (canSee)
            {
                chasingTarget = false;
                agent.speed = 0.00f;
                target = player.transform.position;
                Vector3 lookPos = target - transform.position;
                lookPos.y = 0;
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 20);
                attackMode = true;
                if (attack == false)
                {
                    agent.speed = 0.01f;
                    StartCoroutine(Attack());
                }
            } else
            {
                attackMode = false;
            }
        }
        if (Vector3.Distance(transform.position, player.position) < runawayDist)
        {
            if (runAway)
            {
                if (idle == false && attackMode == false)
                {
                    chasingTarget = false;
                    StartCoroutine(Idle());
                }
            }
        }
        if (Vector3.Distance(transform.position, player.position) < sightDist && !idle && (Vector3.Distance(transform.position, player.position) > attackRange))
        {
            chasingTarget = true;
            //Debug.Log("chasing");
            agent.acceleration = acel;
            target = player.transform.position;


        }
        if (Vector3.Distance(transform.position, player.position) > sightDist + giveUpBuffer)
        {
            if (idle == false && attackMode == false)
            {
                chasingTarget = false;
                StartCoroutine(Idle());
            }
        }
       


    }
    void FixedUpdate()
    {
        agent.speed = speed;
        if (canSee)
        {
            agent.SetDestination(target);
        } else
        {
            agent.speed = speed;
            if (idle == false && attackMode == false)
            {
                chasingTarget = false;
                StartCoroutine(Idle());
            }
        }
    }

    IEnumerator Idle()
    {
        if (!attack)
        {
            agent.acceleration = acel;
            if (runAway && Vector3.Distance(transform.position, player.position) < runawayDist)
            {
                agent.speed = speed;

                idle = true;
                int x, y;
                if (player.position.x > 0)
                {
                    x = Random.Range(-40, -20);
                }
                else
                {
                    x = Random.Range(20, 40);
                }


                if (player.position.y > 0)
                {
                    y = Random.Range(-40, -20);
                }
                else
                {
                    y = Random.Range(20, 40);
                }


                Vector3 newTarget = new Vector3(player.position.x + x, player.position.y, player.position.z + y);
                target = newTarget;
                //Debug.Log("running");
                agent.acceleration = 100;
                agent.SetDestination(newTarget);
                yield return new WaitForSeconds(2.5f);

                idle = false;
            }
            else
            {
                agent.acceleration = acel;
                agent.speed = speed;

                idle = true;
                Vector3 newTarget = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));
                target = newTarget;
                //Debug.Log("idling");
                agent.SetDestination(newTarget);
                yield return new WaitForSeconds(1.5f);
                idle = false;
            }
        }
        
    }
    IEnumerator Attack()
    {
        agent.speed = 0.01f;
        if (!idle)
        {
            agent.speed = 0.01f;
            agent.acceleration = acel;
            attack = true;
            agent.acceleration = acel;
            GetComponent<AiAttack>().RunAttack(index);
            yield return new WaitForSeconds(attackTime);
            //Debug.Log("attacking");
            attack = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChipProjectile"))
        {

            Chip c = other.GetComponent<ChipProjectile>().getAssociatedChip();
            if (c.canDestroy)
            {
                Debug.Log("hit for " + c.getDamage());
                hp -= c.getDamage();

                if (hp <= 0)
                {
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
    }

}
