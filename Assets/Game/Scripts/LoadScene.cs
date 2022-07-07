using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

    public class LoadScene : MonoBehaviour
    {
        public void TryAgain()
        {
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
