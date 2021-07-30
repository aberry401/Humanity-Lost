using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawnPrefab,spawnLoc;

    public GameObject attackHitbox;
    public GameObject attackHitbox2;
    public GameObject particles;
    public bool anim;
    public bool particle;
    public float attackTime;
    public float aliveTime;
    public int damage;

    void Start()
    {
        if (attackHitbox != null)
        {
            attackHitbox.SetActive(false);
        }
        if (attackHitbox2 != null)
        {
            attackHitbox2.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RunAttack(int index)
    {
        Debug.Log("ran an attack");
        switch (index)
        {
            case 0:
               Attack0();
                break;
            case 1:
                StartCoroutine(Attack1());
                break;
            case 2:
                StartCoroutine(Attack2());
                break;
            case 3:
                Attack3();
                break;
        
        }
    }
    void Attack0()
    {
        GameObject g = (Instantiate(spawnPrefab, spawnLoc.transform));
        g.transform.parent = gameObject.transform;
        Destroy(g, aliveTime);
        
    }
    IEnumerator Attack1()
    {
        if (anim)
        {
            GetComponentInChildren<Animator>().Play("attack");
        }
        if (particle)
        {
            Destroy(Instantiate(particles, transform), 1f);
        }
        Debug.Log("attack1!");
        yield return new WaitForSeconds(attackTime);
        attackHitbox.SetActive(true);
        if (attackHitbox2 != null)
        {
            attackHitbox2.SetActive(true);
        }
        yield return new WaitForSeconds(attackTime);
        attackHitbox.SetActive(false);
        if (attackHitbox2 != null)
        {
            attackHitbox2.SetActive(false);
        }

    }
    IEnumerator Attack2()
    {
        if (anim)
        {
            GetComponentInChildren<Animator>().Play("attack");
        }
        if (particle)
        {
            Destroy(Instantiate(particles, transform), 2f);
        }
        Debug.Log("attack2!");
        yield return new WaitForSeconds(attackTime);
        attackHitbox.SetActive(true);
        if (attackHitbox2 != null)
        {
            attackHitbox2.SetActive(true);
        }
        yield return new WaitForSeconds((attackTime / 2));
        attackHitbox.SetActive(false);
        if (attackHitbox2 != null)
        {
            attackHitbox2.SetActive(false);
        }
        Destroy(gameObject, 0.1f);

    }
    void Attack3()
    {
        GameObject g = (Instantiate(spawnPrefab, spawnLoc.transform));
        g.transform.parent = GameObject.Find("empty").transform;
        Destroy(g, 15f);

    }
}
