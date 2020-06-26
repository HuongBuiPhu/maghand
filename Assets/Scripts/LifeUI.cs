using UnityEngine;

public class LifeUI : MonoBehaviour {

    [SerializeField]
    private GameObject[] life_icon = null;

    public void Display(int life) {
        for (int i = 0; i < 5; i++) {
            if (i < life) life_icon[i].SetActive(true);
            else life_icon[i].SetActive(false);
        }
    }

}
