using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private Point topLeft = new Point(-100, 100);
    private Point topRight = new Point(100, 100);
    private Point bottomRight = new Point(100, -100);
    private Point bottomLeft = new Point(-100, -100);

    private Point lastSafePosition = new Point(0,0);
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        Vector2 cameraPosition = new Vector2(position.x, position.z);

        if ((cameraPosition.x < topLeft.X || cameraPosition.y > topLeft.Y )
                                         || (cameraPosition.x > topRight.X || cameraPosition.y > topRight.Y )
                                         || (cameraPosition.x > bottomRight.X || cameraPosition.y < bottomRight.Y )
                                         || (cameraPosition.x < bottomLeft.X || cameraPosition.y < bottomLeft.Y))
        {
          
            SnapCamera(lastSafePosition);
            return;
        }

        
        lastSafePosition = new Point(cameraPosition.x -10, cameraPosition.y -10);
    }

    private void SnapCamera(Point point)
    {
        Vector3 position = new Vector3(point.X, transform.position.y, point.Y);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, position.x, Time.deltaTime * 1.5f), transform.position.y,Mathf.Lerp(transform.position.z, position.z, Time.deltaTime * 1.5f));
    }
}
