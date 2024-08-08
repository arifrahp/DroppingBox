using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float speed = 5f;
    public Collider objectDeleter;
    public Collider mainBoxCollider;
    public enum ObjectType
    {
        redObject,
        greenObject,
        coinObject
    }
    [SerializeField] ObjectType objectType = new ObjectType();

    private PlayManager playManager;
    private void Start()
    {
        playManager = FindAnyObjectByType<PlayManager>();
        GameObject deleter = GameObject.Find("Object Deleter");
        GameObject mainBox = GameObject.Find("Main Box");

        objectDeleter = deleter.GetComponent<Collider>();
        mainBoxCollider = mainBox.GetComponent<Collider>();

    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == objectDeleter)
        {
            Destroy(this.gameObject);
        }

        if(other == mainBoxCollider)
        {
            switch (objectType)
            {
                case ObjectType.redObject:
                    playManager.RedObjectCollide();
                    break;
                
                case ObjectType.greenObject:
                    playManager.GreenObjectCollied();
                    break;
                
                case ObjectType.coinObject:
                    playManager.CoinObjectCollide();
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
