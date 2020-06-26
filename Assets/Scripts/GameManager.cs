using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private int score, life_count;
    private Level level;
    private float current_box_speed;
    private List<BoxScript> all_box = new List<BoxScript>();

    [SerializeField]
    private GameObject start_button = null;
    [SerializeField]
    private Text score_text = null, level_text = null;
    [SerializeField]
    private LifeUI life = null;

    private void Start() { 
        StartGame();
    }

    public void StartGame() {
        this.score = 0;
        this.life_count = 3;
        this.level = Level.CreateDefaultLevel();
        this.current_box_speed = 1.0f;
        StartCoroutine(AutoGennerateBox());
        StartCoroutine(LevelCheck());
        StartCoroutine(LifeCheck());
        DisplayLife();
        DisplayScore();
        DisplayLevel();

        // tắt nút start
        start_button.SetActive(false);
        // GameObject explosion_game_object = Spawner.INSTANCE.DequeueObjectFromPool(Explosion.TAG);
        // explosion_game_object.transform.localPosition = start_button.transform.localPosition;
        // Explosion exp = explosion_game_object.GetComponent<Explosion>();
        // exp.do_after_boom = () => Spawner.INSTANCE.EnqueueObjectToPool(explosion_game_object, Explosion.TAG);
        // Color color = start_button.GetComponent<Renderer>().material.color;
        // explosion_game_object.GetComponent<Renderer>().material.color = color;
        // explosion_game_object.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
        // exp.Boom();
    }

    private IEnumerator LevelCheck() {
        while (true) {
            yield return null;
            if (level.IsLeveUp(score)) {
                while (all_box.Count > 0) all_box[0].boom_action();
                current_box_speed++;
                life_count++;
                DisplayLife();
                DisplayScore();
                DisplayLevel();
            }
        }
    }

    private IEnumerator LifeCheck() {
        while (true) {
            yield return null;
            if (life_count <= 0) {
                while (all_box.Count > 0) all_box[0].boom_action();
                start_button.SetActive(true);
                StopAllCoroutines();
            }
        }
    }

    private void DisplayScore() {
        score_text.text = "" + score;
    }

    private void DisplayLife() {
        life.Display(life_count);
    }

    private void DisplayLevel() {
        level_text.text = level.ToString();
    }

    private IEnumerator AutoGennerateBox() {
        while (true) {
            // lấy box ra khỏi pool
            GameObject box_game_object = Spawner.INSTANCE.DequeueObjectFromPool(BoxScript.TAG);
            BoxScript box_script = box_game_object.GetComponent<BoxScript>();
            all_box.Add(box_script);
            box_script.speed = this.current_box_speed;
            box_script.RandomState();
            box_script.bonus_score_action = () => {
                this.score++;
                DisplayScore();
            };
            box_script.minus_life_action = () => {
                this.life_count--;
                DisplayLife();
            };
            box_script.boom_action = () => {
                all_box.Remove(box_script);

                // lấy explosion ra khỏi pool
                GameObject explosion_game_object = Spawner.INSTANCE.DequeueObjectFromPool(Explosion.TAG);
                explosion_game_object.transform.localPosition = box_game_object.transform.localPosition;
                Explosion exp = explosion_game_object.GetComponent<Explosion>();
                exp.do_after_boom = () => Spawner.INSTANCE.EnqueueObjectToPool(explosion_game_object, Explosion.TAG);
                Color color = box_game_object.GetComponent<Renderer>().material.color;
                explosion_game_object.GetComponent<Renderer>().material.color = color;
                explosion_game_object.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
                exp.Boom();

                // làm mới box rồi đưa quay trở lại pool
                box_script.Clear();
                Spawner.INSTANCE.EnqueueObjectToPool(box_game_object, BoxScript.TAG);
            };
            yield return new WaitForSeconds(1f);
        }
    }
}