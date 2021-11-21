using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Button nextLevelButton;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {

        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (playerControllerScript.levelDone)
        {

            nextLevelButton.gameObject.SetActive(true);


        }

    }

    public void LoadNextLevel()
    {

        SceneManager.LoadScene("Level2");

    }

}
