using UnityEngine;
using UnityEngine.UI;


namespace Interactive360
{

    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance = null;
        public Button[] m_buttonsInScene; //A reference to all the buttons in the scene that would load new scenes
        public GameObject m_brands; //A reference to the menu being rendered
        public GameObject m_categories; //A reference to the button that toggles the video content to play

        [SerializeField] string m_oculusMenuToggle = "Button4"; //The name of the oculus button input that will toggle the scene on and off

        private AudioSource m_menuToggleAudio; //Audio clip to play when the menu is closed


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
        // on Start, bind all buttons to their respective scenes and call DontDestroyOnLoad to keep the Main Menu in every scene
        void Start()
        {
            m_menuToggleAudio = GetComponent<AudioSource>();
        }

        //call the checkForInput method once per fram
        void Update()
        {
            checkForInput();
        }

        //if the menu is active, turn it off. If it is inactive, turn it on
        public void toggleMenu()
        {
            if (m_brands.activeInHierarchy)
            {
                m_brands.SetActive(false);
                m_categories.SetActive(true);
            }
            else if (m_categories.activeInHierarchy)
            {
                m_categories.SetActive(false);
            }
            else
            {
                m_brands.SetActive(true);
            }
        }

        //If we detect input, call the toggleMenu method 
        private void checkForInput()
        {
            //check for input from specified Oculus Touch button or the App button on Google Daydream Controller
            if (Input.GetButtonDown(m_oculusMenuToggle))
            {
                toggleMenu();

                //if we have an audio source to play with menu toggle, play it now
                if (m_menuToggleAudio)
                    m_menuToggleAudio.Play();
            }

        }
    }
        
    

}

