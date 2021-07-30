


using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class NewAI : Enemy
{
    public GameObject hpBar;

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
    public bool isCar;
    private float acel;
    public NavMeshAgent agent;
    public bool chasingTarget;
    public Transform target;
    
    public AudioSource aSource;

    public bool isLooking;

    public bool attacking;

    public State activeState;

    public enum State { idle, chasing, attacking, running, looking };
    public bool canSee;
    public enum Sounds { attack, idle ,chase, dmg, runAway }

    [Tooltip("{ attack, idle ,chase, dmg, runAway }")]

    
    public AudioClip[] clips;

    public GameObject spawnsystem;


    public GameObject drop;

    public NavMeshPath path;

    public bool hasrun;
    public int maxhp;
    public bool aSourceReady;
    public float timer;
    void Start()
    {
        maxhp = (int)hp;
            hasrun = false;
       
        if (gameObject.transform.localScale.z != 0)
        {
            
            aSourceReady = true;
            attacking = false;
            aSource = GetComponent<AudioSource>();

            path = new NavMeshPath();

            //Debug.Log("statata");
            spawnsystem = GameObject.FindGameObjectWithTag("spawnsystem");
            agent = GetComponent<NavMeshAgent>();

            player = GameObject.FindGameObjectWithTag("spawnsystem").GetComponent<SpawnScript>().currentTarget;
            //Debug.Log("statat2222222a");
            activeState = State.idle;

            target = spawnsystem.GetComponent<SpawnScript>().waypoints
                [Random.Range(0, spawnsystem.GetComponent<SpawnScript>().waypoints.Count - 1)];
            hasrun = true;
        }

    }
    void Update()
    {
       

        if (gameObject.transform.localScale.z != 0 )
        {
            if (hpBar != null)
            {
                hpBar.transform.localScale = Vector3.Lerp(new Vector3(hpBar.transform.localScale.x, hpBar.transform.localScale.y, hpBar.transform.localScale.z),
                    new Vector3(1 * (hp / maxhp), hpBar.transform.localScale.y, hpBar.transform.localScale.z),Time.deltaTime* 4f);
               
                hpBar.GetComponent<Renderer>().material.SetColor("_Color", Color.green * (hp / maxhp));
            }
            if (!hasrun)
            {
                Start();
            }
            //Debug.Log("dead!");
            timer += Time.deltaTime;

            if (dead)
            {
                Destroy(gameObject);
                if (drop != null)
                {
                    Instantiate(drop, gameObject.transform.position, gameObject.transform.rotation);
                }
                else
                {
                    Instantiate(drop, gameObject.transform.position, gameObject.transform.rotation);
                    // if(rand.Next(11) % 2 == 0)
                    //     Instantiate(Resources.Load("Prefabs/Scrap"), gameObject.transform.position, gameObject.transform.rotation);
                }



            }
        }
    }

    void FixedUpdate()
    {
        if (hp <= 0)
        {
            dead = true;
        }
        bool parentscalezero = true;
        if (transform.parent != null)
        {
            parentscalezero = false;
        }

        if (gameObject.transform.localScale.z != 0 )
        {
            if (activeState == State.attacking)
            {
                RotateTowards(target);
            }
            if (timer > 5)
            {
                if (agent.velocity.magnitude == 0 && activeState == State.idle)
                {
                    activeState = State.looking;

                }
                timer = 0;
            }


            NavMeshHit hit;
            if(target == null)
            {
                target = spawnsystem.GetComponent<SpawnScript>().waypoints
                [Random.Range(0, spawnsystem.GetComponent<SpawnScript>().waypoints.Count - 1)];
            }
            if (agent.isOnNavMesh)
            {
                if (!agent.Raycast(target.position, out hit) || dist < hearDist)
                {
                    canSee = true;
                }
                else
                {
                    canSee = false;
                }
            } else
            {
                agent.Warp(transform.position);
            }


            dist = Vector3.Distance(transform.position, player.position);
            if ((dist < sightDist && canSee || dist < hearDist) && activeState != State.attacking && dist > runawayDist)
            {
                StartCoroutine(PlayClip((int)Sounds.chase));
                if (activeState != State.chasing)
                {
                    //Debug.Log("calling chase" + activeState);
                    activeState = State.chasing;
                    StartCoroutine(Chase());
                    return;
                }

            }
            if (dist < attackRange)
            {
                if (!runAway)
                {

                    //Debug.Log("calling attack" + activeState);
                    activeState = State.attacking;
                    StartCoroutine(Attack());
                    return;

                }
                else if (dist > runawayDist)
                {
                    //Debug.Log("calling attack" + activeState);
                    activeState = State.attacking;
                    StartCoroutine(Attack());
                    return;
                }
                else
                {
                    activeState = State.running;

                    StartCoroutine(Run());
                    return;
                }

            }
            if (dist > attackRange && activeState == State.attacking)
            {

                if (activeState != State.chasing)
                {
                    //Debug.Log("calling chase" + activeState);
                    activeState = State.chasing;
                    StartCoroutine(Chase());
                    return;
                }

            }
            if (dist < runawayDist && runAway)
            {

                if (activeState != State.running && activeState != State.attacking)
                {
                    //Debug.Log("calling run" + activeState);
                    activeState = State.running;

                    StartCoroutine(Run());
                    return;

                }
            }
            if (!canSee && activeState == State.chasing)
            {
                if (activeState != State.looking)
                {
                    activeState = State.looking;
                    StartCoroutine(Look());
                    return;
                }


            }
            if (dist > sightDist)
            {
                if (activeState != State.idle)
                {

                    target = FindWaypoint();



                    agent.CalculatePath(target.position, path);
                    agent.SetPath(path);
                    activeState = State.idle;
                    StartCoroutine(Idle());
                    return;
                }
            }

            if (isCar)
            {
                if (activeState != State.idle)
                {

                    target = FindWaypoint();



                    agent.CalculatePath(target.position, path);
                    agent.SetPath(path);
                    activeState = State.idle;
                    StartCoroutine(Idle());
                    return;
                }
            }
        }
    }

    Transform FindWaypoint()
    {
       
        if (isCar == true)
        {
            aSource.Play();
            return spawnsystem.GetComponent<SpawnScript>().carwaypoints
          [Random.Range(0, spawnsystem.GetComponent<SpawnScript>().carwaypoints.Count - 1)];
        } else
        {
            if (aSource != null)
            {
                aSource.Play();
            }
            return spawnsystem.GetComponent<SpawnScript>().waypoints
           [Random.Range(0, spawnsystem.GetComponent<SpawnScript>().waypoints.Count - 1)];
            
        }
       
    }
    public void runPoison()
    {
        StartCoroutine(PoisonDmg());
    }
    IEnumerator PoisonDmg()
    {
        yield return new WaitForSeconds(Random.Range(.1f,1f));
        hp -= 1;
        yield return new WaitForSeconds(Random.Range(.1f, 1f));
        hp -= 1;
    }
        IEnumerator PlayClip(int index)
    {
        if (aSourceReady == true)
        {
            if (index < clips.Length)
            {
                aSourceReady = false;
                if (clips[index] != null)
                {
                    aSource.clip = clips[index];
                    aSource.volume = Random.Range(0f, .5f);
                    aSource.pitch = Random.Range(0.7f, 1.3f);
                    aSource.maxDistance = Random.Range(0f, 90f);
                    aSource.Play();
                }
                yield return new WaitForSeconds(Random.Range(2f, 18f));
                aSourceReady = true;
            }
        }
       


    }

  




        IEnumerator Idle()
    {

        if (activeState == State.idle)
        {
            //Debug.Log("idling");
            float waypointDist = Vector3.Distance(transform.position, target.position);
            ////Debug.Log(waypointDist);

            if (waypointDist < 1)
            {
                //Debug.Log("new waypouint idle");
                target = FindWaypoint();


                agent.CalculatePath(target.position, path);
                agent.SetPath(path);


            }

            yield return new WaitForSeconds(3f);
            StartCoroutine(PlayClip((int)Sounds.idle));
            StartCoroutine(Idle());

        }
    }
    IEnumerator Run()
    {
        if (activeState == State.running)
        {

            StartCoroutine(PlayClip((int)Sounds.runAway));
            if (agent.remainingDistance < 2)
            {
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
                agent.CalculatePath(newTarget, path);
                agent.SetPath(path);
                //Debug.Log("running");
                yield return new WaitForSeconds(.8f);
            }
            yield return new WaitForSeconds(.8f);
            StartCoroutine(Run());

        }
    }
    IEnumerator Chase()
    {
        if (activeState == State.chasing)
        {

            target = player;
            agent.CalculatePath(player.position, path);
            agent.SetPath(path);
            //Debug.Log("chasing");
            yield return new WaitForSeconds(.2f);
            StartCoroutine(Chase());

        }

    }
    IEnumerator Attack()
    {
       
            if (attacking == false)
            {
                attacking = true;
                target = player;
                agent.speed = 0;
                GetComponent<AiAttack>().RunAttack(index);
                yield return new WaitForSeconds(attackTime);
                agent.speed = speed;
                agent.CalculatePath(target.position, path);
                agent.SetPath(path);
                StartCoroutine(Attack());
                attacking = false;
            StartCoroutine(PlayClip((int)Sounds.attack));
            //Debug.Log("attacjing");
            }
        

    }
    IEnumerator Look()
    {
        for (int i = 0; i < 7; i++)
        {
            Vector3 newTarget = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));
            agent.CalculatePath(newTarget, path);
            agent.SetPath(path);
            //Debug.Log("looking");
            
            yield return new WaitForSeconds(1.5f);
            if (i == 6)
            {
                activeState = State.idle;
                target = FindWaypoint();


                agent.CalculatePath(target.position, path);
                agent.SetPath(path);
                StartCoroutine(Idle());
            }
        }

    }
    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChipProjectile"))
        {
            //Debug.Log("HITTTTT");
            Chip c = other.GetComponent<ChipProjectile>().getAssociatedChip();
            if (c.canDestroy)
            {
                //Debug.Log("hit for " + c.getDamage());
                hp -= c.getDamage();
                StartCoroutine(PlayClip((int)Sounds.dmg));
               

                if (hp <= 0)
                {
                    dead = true;
                }
            }
        }
    }

}
