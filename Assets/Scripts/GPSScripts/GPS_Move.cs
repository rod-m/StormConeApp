using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GPSScripts
{
    public class GPS_Move : MonoBehaviour
    {
        public double targetLocationLon;
        public double targetLocationLat;

        public TestLocationService tS;
        public Text oD;

        public bool mercProjection;
        private bool engaged;

        private double gpsX;
        private double gpsZ;
        private double offsetX;
        private double offsetZ;
        private double ppX;
        private double ppZ;
        private double x;
        private double z;

        public Text xT;
        public Text yT;

        // Start is called before the first frame update
        private void Start()
        {
            engaged = false;
            //xT = GameObject.FindGameObjectWithTag("Ppx").GetComponent<Text>();
            //yT = GameObject.FindGameObjectWithTag("Ppy").GetComponent<Text>();
            //tS = GameObject.FindGameObjectWithTag("GameController").GetComponent<TestLocationService>();
            //oD = GameObject.FindGameObjectWithTag("Finish").GetComponent<Text>();
        }

        public void Activate()
        {
        
            StartCoroutine(setOffset());
        }

        // Update is called once per frame
        void Update()
        {
            if (engaged) 
            {
                StartCoroutine(updateGPS());
                engaged = false;
            }
        }

        private void updatePos() 
        {
            gpsX -= offsetX;
            gpsZ -= offsetZ;
            if (mercProjection)
            {
                x = MercatorProjection.lonToX(gpsX);
                z = MercatorProjection.latToY(gpsZ);
            }
            else 
            {
                x = ConvertGPSCoords(gpsX);
                z = ConvertGPSCoords(gpsZ);
            }
        
            transform.position = new Vector3((float)x, 0, (float)z);
            float f = Vector2.Distance(new Vector2((float)x, (float)z), new Vector2(0, 30));
            oD.text = f.ToString();
        }

        private double ConvertGPSCoords(double n) 
        {
            return n*(111190f);
        }

        private IEnumerator setOffset() 
        {
            yield return new WaitForSeconds(3);
            offsetX = tS.GPSX;
            offsetZ = tS.GPSZ;
            ppX = MercatorProjection.lonToX(offsetX - targetLocationLon);
            ppZ = MercatorProjection.latToY(offsetZ - targetLocationLat);
            xT.text = ppX.ToString();
            yT.text = ppZ.ToString();
            engaged = true;
        }

        private void updateObjDistance() {
            ppX = MercatorProjection.lonToX(gpsX - targetLocationLon);
            ppZ = MercatorProjection.latToY(gpsZ - targetLocationLat);
        }

        private IEnumerator updateGPS() 
        {
            yield return new WaitForSeconds(1);
            gpsX = tS.GPSX;
            gpsZ = tS.GPSZ;
            updatePos();
            updateObjDistance();
            StartCoroutine(updateGPS());
        }
    }
}
