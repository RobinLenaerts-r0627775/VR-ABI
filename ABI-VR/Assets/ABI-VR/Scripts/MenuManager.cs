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
            screen.transform.parent = GameObject.FindGameObjectWithTag("MenuHolder").transform;
            Vector3 temppos = screen.transform.localPosition;
            Quaternion temprot = screen.transform.localRotation;
            temppos.x = 0;
            temppos.y = 0;
            temppos.z = 0;
            temprot.x = 0;
            temprot.y = 0;
            temprot.z = 0;
            screen.transform.localPosition = temppos;
            screen.transform.localRotation = temprot;
            toggleMenu(screen);

        }

        public void popMenuScreen()
        {
            m_menuScreens[m_menuScreens.Count - 1].SetActive(false);
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

            if (screen == null)
            {
                return;
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
                int menuindex = m_menuScreens.IndexOf(activemenu) + 1;
                if (menuindex == m_menuScreens.Count)
                {
                    toggleMenu(null);
                    return;
                }
                if (menuindex > m_menuScreens.Count) menuindex -= m_menuScreens.Count;
                toggleMenu(m_menuScreens[menuindex]);
            }
        }
    }
}

