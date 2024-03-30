using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Difficulty : MonoBehaviour
{
    //difficulty button
    private Button diffButton;
    private GameManager gameManager;
    public int difficulty;
    [SerializeField]  
    private AudioSource sliceEffectSound;

    // Start is called before the first frame update
    void Start()
    {
        diffButton = GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
        diffButton.onClick.AddListener(SetDifficulty);
    }

    private void ClickButton()
    {
        Debug.Log("button is clicked");
    }

    void SetDifficulty ()
    {
       Debug.Log("difficulty level has been chosen");
       gameManager.StartGame(difficulty);
        sliceEffectSound.Play();

    }
}
