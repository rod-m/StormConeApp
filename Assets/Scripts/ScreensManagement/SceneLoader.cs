using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace ScreensManagement
{
    public class SceneLoader : MonoBehaviour
    {
        private string GetActiveScene()
        {
            return SceneManager.GetActiveScene().name;
        }

        private void Start()
        {
            GetActiveScene();

            switch (GetActiveScene())
            {
                case "Splash Screen": StartCoroutine(SplashScreen());
                    break;
                
                case "Welcome Screens":
                    break;
            }
            
        }


        private IEnumerator SplashScreen()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Welcome Screens");
        }


        public void SwitchScene(String sceneName) 
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
