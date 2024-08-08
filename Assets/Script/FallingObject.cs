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
    private ObjectSpawner objectSpawner;
    private MainBox mainBox;
    private void Start()
    {
        playManager = FindAnyObjectByType<PlayManager>();
        objectSpawner = FindAnyObjectByType<ObjectSpawner>();
        mainBox = FindAnyObjectByType<MainBox>();
        GameObject deleter = GameObject.Find("Object Deleter");
        GameObject mainBoxObject = GameObject.Find("Main Box");

        objectDeleter = deleter.GetComponent<Collider>();
        mainBoxCollider = mainBoxObject.GetComponent<Collider>();

    }
    void Update()
    {
        float newY = transform.position.y - (speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == objectDeleter)
        {
            HandleDestruction();
        }

        if(other == mainBoxCollider)
        {
            switch (objectType)
            {
                case ObjectType.redObject:
                    if (!mainBox.isImune)
                    {
                        playManager.RedObjectCollide();
                    }
                    break;
                
                case ObjectType.greenObject:
                    playManager.GreenObjectCollied();
                    break;
                
                case ObjectType.coinObject:
                    playManager.CoinObjectCollide();
                    break;
            }
            HandleDestruction();
        }
    }

    public void HandleDestruction()
    {
        if(objectSpawner.activeFallingObjects.Contains(this.gameObject))
        {
            objectSpawner.activeFallingObjects.Remove(this.gameObject);
        }
        Destroy(this.gameObject);
    }
}
