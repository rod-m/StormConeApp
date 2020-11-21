using System;
using UnityEngine;
using System.Collections;
using Mapbox.Unity.Utilities;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Serialization;


namespace ScreensManagement
{
    public class PageSlider : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        private Vector3 panelLocation;
        private float percentThreshold = 0.2f;
        public int numberOfPanels = 3;
        [SerializeField] private int _currentPanel = 0;
        public float easing = 0.5f;
        [SerializeField] private bool shouldMove = true;
        private void Start()
        {
            panelLocation = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //see if the panel has been dragged and shouldMove it
            float difference = eventData.pressPosition.x - eventData.position.x;
            // only shouldMove if allowed
            if (ShouldMove(difference))
            {
                transform.position = panelLocation - new Vector3(difference, 0, 0);
            }
        }

        private bool ShouldMove(float diff)
        {
            shouldMove = ((_currentPanel < numberOfPanels && diff > 0f) || (_currentPanel > 1 && diff < 0f));
            return shouldMove;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            if (!shouldMove)
            {
                return;
            }

            float percentages = (eventData.pressPosition.x - eventData.position.x) / Screen.width;

            if (Mathf.Abs(percentages) >= percentThreshold)
            {
                Vector3 newLocation = panelLocation;
                if (percentages > 0)
                {
                    _currentPanel++;
                    newLocation += new Vector3(-Screen.width, 0, 0);
                }
                else if (percentages < 0)
                {
                    _currentPanel--;
                    newLocation += new Vector3(Screen.width, 0, 0);
                }

                StartCoroutine(SmoothMove(transform.position, newLocation, easing));
                panelLocation = newLocation;
            }
            else
            {
                StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
            }
        }

        private IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
        {
            float t = 0;
            while (t <= 1.0f)
            {
                t += Time.deltaTime / seconds;
                transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0, 1, t));
            }

            yield return null;
        }
    }
}