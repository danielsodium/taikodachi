using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public GameObject songList;
    public GameObject difficulties;

    public GameObject difficultyButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void goBack() {
        if (songList.activeSelf == true) {
            SceneManager.LoadScene (sceneName:"Start");
        } else {
            //Go back to songs
            difficulties.transform.parent.gameObject.SetActive(false);
            songList.SetActive(true);
        }

    }
    public void chooseDifficulty(List<Dictionary<string, string>> data) {
        songList.SetActive(false);
        foreach (Transform child in difficulties.transform) {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Dictionary<string, string> level in data) {
            Vector3 pos = new Vector3(difficulties.transform.position.x, difficulties.transform.position.y, difficulties.transform.position.z);
            GameObject button = Instantiate(difficultyButton, pos, Quaternion.identity, difficulties.transform);
            TextMeshProUGUI songTitle = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            songTitle.text = level["Version"];
            button.GetComponent<DifficultyButton>().data = level;
        }
        


        difficulties.transform.parent.gameObject.SetActive(true);
    }
}
