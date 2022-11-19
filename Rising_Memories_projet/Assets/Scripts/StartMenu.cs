using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private const double DEADZONE = 0.3;
    private string currbtn;
    private string currpanel;
    private Dictionary<string, Dictionary<string, string>> currbtns = new Dictionary<string, Dictionary<string, string>>();

    private Dictionary<string, Dictionary<string, string>> panels = new Dictionary<string, Dictionary<string, string>>()
    {
        { "MainMenu", new Dictionary<string,string>(){ { "MainMenu/Option","OptionMenu" } } },
        { "OptionMenu", new Dictionary<string, string>(){ {"OptionMenu/Back","MainMenu"} } }
    };

    private Dictionary<string, Dictionary<string, string>> startMenu = new Dictionary<string, Dictionary<string, string>>() {
        { "MainMenu/Start", new Dictionary<string, string>{ {"up", "MainMenu/Quit" },{"down", "MainMenu/Option" } } },
        { "MainMenu/Option", new Dictionary<string, string>{ {"up", "MainMenu/Start" },{"down", "MainMenu/Quit" } } },
        { "MainMenu/Quit", new Dictionary<string, string>{ {"up", "MainMenu/Option" },{"down", "MainMenu/Start" } } }
    };
    private Dictionary<string, Dictionary<string, string>> optionMenu = new Dictionary<string, Dictionary<string, string>>() {
        { "OptionMenu/MusicSlider", new Dictionary<string, string>{ {"up", "OptionMenu/Back" },{"down", "OptionMenu/FxSlider" } } },
        { "OptionMenu/FxSlider", new Dictionary<string, string>{ {"up", "OptionMenu/MusicSlider" },{"down", "OptionMenu/Control" } } },
        { "OptionMenu/Control", new Dictionary<string, string>{ {"up", "OptionMenu/FxSlider" },{"down", "OptionMenu/Back" } } },
        { "OptionMenu/Back", new Dictionary<string, string>{ {"up", "OptionMenu/Control" },{"down", "OptionMenu/MusicSlider" } } }
    };


    void SwitchBtn(string direction)
    {
        EventSystem.current.SetSelectedGameObject(null);
        currbtn = currbtns[currbtn][direction];
        EventSystem.current.SetSelectedGameObject(GameObject.Find(currbtn));
    }

    // Start is called before the first frame update
    void Start()
    {

        currbtn = "MainMenu/Start";
        currbtns = startMenu;
        currpanel = "MainMenu";
        GameObject.Find("OptionMenu").GetComponent<Canvas>().enabled = false;
        EventSystem.current.SetSelectedGameObject(GameObject.Find(currbtn));
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Vertical")> DEADZONE)
        {
            SwitchBtn("up");
        }
        if (Input.GetAxis("Vertical") < -DEADZONE)
        {
            SwitchBtn("down");
        }
    }

    public void SwitchPanel()
    {
        string prevpanel = currpanel;
        currpanel = panels[currpanel][currbtn];

        if (currpanel == "MainMenu")
        {
            currbtns = startMenu;
            currbtn = "MainMenu/Start";
        }
        if (currpanel == "OptionMenu")
        {
            currbtns = optionMenu;
            currbtn = "OptionMenu/MusicSlider";
        }

        EventSystem.current.SetSelectedGameObject(GameObject.Find(currbtn));
        GameObject.Find(prevpanel).GetComponent<Canvas>().enabled = false;
        GameObject.Find(currpanel).GetComponent<Canvas>().enabled = true;

    }

    public void startGame()
    {
        SceneManager.LoadScene("Map");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
