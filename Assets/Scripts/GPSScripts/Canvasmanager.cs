using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GPSScripts
{
    public class Canvasmanager : MonoBehaviour
    {
        [FormerlySerializedAs("m_LocationText")] [SerializeField]
        public Text m_distanceText;

        public Text DistanceText
        {
            get { return m_distanceText; }
            set { m_distanceText = value; }
        }
    
        [SerializeField]
        public Text m_LocationText;
        public Text LocationText
        {
            get { return m_LocationText; }
            set { m_LocationText = value; }
        }
        [SerializeField]
        public Text m_LogText;

        public Text logText
        {
            get { return m_LogText; }
            set { m_LogText = value; }
        }
    
        public void Log(string message)
        {
            m_LogText.text += $"{message}\n";
        }
    

    }
}
