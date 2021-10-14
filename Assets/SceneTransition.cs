using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;
    public float lengthWait;

    public void Restart()
    {
        StartCoroutine(LoadLevelAgain());
    }

    IEnumerator LoadLevelAgain()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(lengthWait);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
