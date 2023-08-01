using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LogicManager : MonoBehaviour
{
    public int player_score;
    public Text score_text;
    public GameObject game_over_screen;
    public bool alive = true;

    public void add_score() {
        player_score += 1;
        score_text.text = player_score.ToString();
    }

    public void restart() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void game_over(){
        game_over_screen.SetActive(true);
        alive = false;
    }
    
}
