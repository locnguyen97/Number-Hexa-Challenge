using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectMoveByDrag : MonoBehaviour
{
    [SerializeField] List<GameObject> particleVFXs;

    private Vector3 startPos;
    private Transform target;

    private void OnEnable()
    {
        startPos = transform.position;
    }
    public void ShowData(Color color, int val)
    {
        var spriteRen = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        var text = spriteRen.transform.GetChild(0).GetComponent<TextMeshPro>();
        spriteRen.color = color;
        text.text = val.ToString();
    }

    public void CheckOnMouseUp()
    {
        //transform.position = startPos;
        if (target)
        {
            GameObject explosion = Instantiate(particleVFXs[Random.Range(0,particleVFXs.Count)], transform.position, transform.rotation);
            Destroy(explosion, .75f);
            GameManager.Instance.CheckStep();
            Destroy(gameObject);
        }
        else
        {
            transform.position = startPos;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.CompareTag("NotUse")) return;
        if (gameObject.tag == collision.gameObject.tag)
        {
            target = collision.transform;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.CompareTag("NotUse")) return;
        if (gameObject.tag == collision.gameObject.tag)
        {
            target = null;
        }
    }
}
