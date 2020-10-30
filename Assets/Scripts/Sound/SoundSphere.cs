using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSphere : MonoBehaviour
{

    public List<GameObject> trees;
    public GameObject cyllinder;
    public int radius;
    public bool setAudioToRadius;

    private GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(trees[Random.Range(0, trees.Count)], transform);
        instance = Instantiate(cyllinder, transform);
        instance.transform.localScale = new Vector3(radius, instance.transform.localScale.y, radius);
        if (setAudioToRadius)
            GetComponent<AudioSource>().maxDistance = radius / 2;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + radius/2, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + radius/2));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - radius/2, transform.position.y, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - radius/2));
    }
}
