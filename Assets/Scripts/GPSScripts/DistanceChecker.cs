using UnityEngine;

namespace GPSScripts
{
    public class DistanceChecker : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private Camera camera;


        private Vector3 cameraPosition;
    
        private float distance;
    
        private bool userIsLeaving;
        private bool reachedArea;

        private bool userIsOutsideBounds;
    
        // Start is called before the first frame update
        void Start()
        {
            userIsLeaving = true;
            reachedArea = false;

        }

        private void Update()
        {
            cameraPosition = camera.transform.position;

            userIsLeaving = true;
        
            distance = Vector3.Distance(target.transform.position, this.transform.position);
            if (distance <= 25)
            {
                userIsLeaving = false;
                reachedArea = true;
            }

            if (distance > 30)
            {
                userIsLeaving = true;
                reachedArea = false;
            }

        }

        public bool UserIsOutsideArea() => userIsLeaving;

        public bool UserIsInsideArea() => reachedArea;


    
    }
}
