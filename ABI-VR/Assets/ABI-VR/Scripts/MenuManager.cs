using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Interactive360
{

    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance = null;
        public Button[] m_buttonsInScene; //A reference to all the buttons in the scene that would load new scenes
        public List<GameObject> m_menuScreens; //A reference to all menu screens 

        [SerializeField] string m_oculusMenuToggle = "Button4"; //The name of the oculus button input that will toggle the scene on and off

        private AudioSource m_menuToggleAudio; //Audio clip to play when the menu is closed
        public GameObject activemenu;

        //make sure that we only have a single instance of the game manager
        void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            activemenu = m_menuScreens[0];
            m_menuToggleAudio = GetComponent<AudioSource>();
        }

        //call the checkForInput method once per fram
        void Update()
        {
            checkForInput();
        }

        public void addMenuScreen(GameObject screen)
        {
            m_menuScreens.Add(screen);
        }

        public void popMenuScreen()
        {
            m_menuScreens.Remove(m_menuScreens[m_menuScreens.Count - 1]);
        }

        //toggle between different menu screens: brands, extra(, voiceover control,...)
        public void toggleMenu(GameObject screen)
        {
            activemenu = screen;
            foreach (GameObject menu in m_menuScreens)
            {
                menu.SetActive(false);
            }

            if (screen == m_menuScreens[0]) 
            {
                m_menuScreens[0].SetActive(true);
                m_buttonsInScene[0].Select();
            }

            else if (screen == m_menuScreens[1])
            {
                //set the categories menu
                m_menuScreens[1].SetActive(true);
                m_buttonsInScene[6].Select();

            }
            else
            {
                screen.SetActive(true);
            }
        }

        //If we detect input, call the toggleMenu method 
        private void checkForInput()
        {
            //check for input from specified Oculus Touch button or the App button on Google Daydream Controller
            if (Input.GetButtonDown(m_oculusMenuToggle))
            {
                int menuindex = m_menuScreens.IndexOf(activemenu) +1;
                if (menuindex >= m_menuScreens.Count) menuindex -= m_menuScreens.Count;
                toggleMenu(m_menuScreens[menuindex]);

                //if we have an audio source to play with menu toggle, play it now
                if (m_menuToggleAudio)
                    m_menuToggleAudio.Play();
            }
        }
    }
}

