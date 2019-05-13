using Interactive360.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CMSLoader : MonoBehaviour
{
    private string _computer = Environment.UserName;
    private Dictionary<string, Content> dictionary = new Dictionary<string, Content>();
    private List<string> categories = new List<string>();

    void Start()
    {
        // LOAD ALL IMAGES
        string _path = "C:\\Users\\" + _computer + "\\Desktop\\CMS.txt";
        GetImages(_path);
        int count = CountCategories();

        // DEFAULT SKYBOX IS NEEDED FOR VIDEO
        Material yourMaterial = (Material)Resources.Load("3D_Stella_Filter_1", typeof(Material));
        yourMaterial.EnableKeyword("_Layout");
        yourMaterial.SetFloat("_Layout", 2f);
        RenderSettings.skybox = yourMaterial;

        // GET CANVAS
        var canvas = GameObject.FindWithTag("Canvas");

        float row = -2f;
        // CREATE BUTTONS IN GRID FOR IMAGES AND VIDEOS
        for (int i = 0; i <= categories.Count - 1; i++)
        {
            var categorie = categories.ElementAt(i);
            GameObject gob = new GameObject("Button");
            gob.transform.localPosition = new Vector3(-3.9f, row, 2);
            gob.transform.Rotate(new Vector3(0, -40, 0));
            row += 0.9f;
            gob.layer = 5;
            GameObject tob = new GameObject("Text");
            gob.transform.SetParent(canvas.transform, false);
            tob.transform.SetParent(gob.transform, false);
            var button = gob.AddComponent<Button>();
            ColorBlock colorVar = button.colors;
            colorVar.highlightedColor = Color.green;
            colorVar.normalColor = new Color(255, 255, 255, 0.3f);
            colorVar.selectedColor = Color.green;
            button.colors = colorVar;
            var rect = gob.AddComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 0.5f);
            var image = gob.AddComponent<Image>();
            button.targetGraphic = image;
            image.type = Image.Type.Sliced;
            var text = tob.AddComponent<Text>();
            text.transform.localScale = new Vector2(0.002f, 0.002f);
            text.text = categorie;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.font = Font.CreateDynamicFontFromOSFont("Arial", 15);
            text.color = Color.black;
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 60;
            text.alignment = TextAnchor.MiddleCenter;
            button.onClick.AddListener(() => WrapperDisplayButtons(categorie));
            var script = gob.AddComponent<VRInteractiveItem>();
            var box = gob.AddComponent<BoxCollider>();
            box.size = new Vector3(0.5f, 0.5f, 0.5f);
        }

        // GET VIDEOPLAYER WITH DEFAULT VIDEO
        var vobj = GameObject.FindWithTag("Video");
        var videoplayer = vobj.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoplayer.url = "C:\\Users\\" + _computer + "\\Desktop\\CMS\\videos\\START.mp4";
    }

    public void Wrapper(string code, string tag, string media)
    {
        StartCoroutine(DisplayImage(code, tag, media));
    }

   public void WrapperDisplayButtons(string categorie)
    {
        StartCoroutine(DisplayButtons(categorie));
    }

    public IEnumerator DisplayImage(string code, string tag, string media)
    {
        if (media.Equals("mp4"))
        {
            Material yourMaterial = (Material)Resources.Load("3D_Stella_Filter_1", typeof(Material));
            if (tag.Equals("3D"))
            {
                yourMaterial.EnableKeyword("_Layout");
                yourMaterial.SetFloat("_Layout", 2f);
            }
            else
            {
                yourMaterial.EnableKeyword("_Layout");
                yourMaterial.SetFloat("_Layout", 0f);
            }
            RenderSettings.skybox = yourMaterial;
            var vobj = GameObject.FindWithTag("Video");
            var videoplayer = vobj.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoplayer.url = dictionary[code].File;
        }
        else if (media.Equals("wav"))
        {
            string file = dictionary[code].File;
            WWW www = new WWW(file);
            yield return www;
            var sound = www.GetAudioClip(false, false);
            var aobj = GameObject.FindWithTag("Audio");
            var audiosource = aobj.GetComponent<AudioSource>();
            audiosource.clip = sound;
            audiosource.playOnAwake = true;
            audiosource.Play();
        }
        else
        {
            string file = dictionary[code].File;
            Texture2D texture = LoadImage(file);
            WWW www = new WWW(file);
            yield return www;
            www.LoadImageIntoTexture(texture);
            Material mat = new Material(Shader.Find("Skybox/Panoramic"));
            if (tag.Equals("3D"))
            {
                mat.EnableKeyword("_Layout");
                mat.SetFloat("_Layout", 2f);
            }
            mat.mainTexture = texture;
            RenderSettings.skybox = mat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DisplayButtons(string categorie)
    {
        // DESTORY ALL GO'S
        var objects = GameObject.FindGameObjectsWithTag("CODE");
        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }

        // GET CANVAS
        var canvas = GameObject.FindWithTag("Canvas");

        // GRID
        float rowImages = 1.5f;
        float rowVideos = 1.5f;
        float rowAudios = 1.5f;

        // CREATE BUTTONS IN GRID FOR IMAGES AND VIDEOS
        for (int i = 0; i <= dictionary.Count - 1; i++)
        {
            string code = dictionary.Keys.ElementAt(i);
            if (code.Split('_').First().Equals(categorie))
            {
                string file = dictionary.Values.ElementAt(i).File;
                string tag = dictionary.Values.ElementAt(i).Tag;
                string media = file.Split('.').Last();
                Texture2D texture = LoadImage(file);
                WWW www = new WWW(file);
                yield return www;
                www.LoadImageIntoTexture(texture);
                GameObject gob = new GameObject(code);
                gob.tag = "CODE";
                if (media.Equals("jpg"))
                {
                    gob.transform.localPosition = new Vector3(-1.9f, rowImages, 2.5f);
                    rowImages -= 0.9f;
                }
                else if (media.Equals("wav"))
                {
                    gob.transform.localPosition = new Vector3(0f, rowAudios, 2.5f);
                    rowAudios -= 0.9f;
                }
                else if (media.Equals("mp4"))
                {
                    gob.transform.localPosition = new Vector3(1.9f, rowVideos, 2.5f);
                    rowVideos -= 0.9f;
                }
                gob.layer = 5;
                GameObject tob = new GameObject("Text");
                gob.transform.SetParent(canvas.transform, false);
                tob.transform.SetParent(gob.transform, false);
                var button = gob.AddComponent<Button>();
                ColorBlock colorVar = button.colors;
                colorVar.highlightedColor = Color.green;
                colorVar.normalColor = new Color(255, 255, 255, 0.3f);
                colorVar.selectedColor = Color.green;
                button.colors = colorVar;
                var rect = gob.AddComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 0.5f);
                var image = gob.AddComponent<Image>();
                button.targetGraphic = image;
                image.type = Image.Type.Sliced;
                var text = tob.AddComponent<Text>();
                text.transform.localScale = new Vector2(0.002f, 0.002f);
                text.text = code;
                text.horizontalOverflow = HorizontalWrapMode.Overflow;
                text.verticalOverflow = VerticalWrapMode.Overflow;
                text.font = Font.CreateDynamicFontFromOSFont("Arial", 15);
                text.color = Color.black;
                text.fontStyle = FontStyle.Bold;
                text.fontSize = 60;
                text.alignment = TextAnchor.MiddleCenter;
                button.onClick.AddListener(() => Wrapper(code, tag, media));
                var script = gob.AddComponent<VRInteractiveItem>();
                var box = gob.AddComponent<BoxCollider>();
                box.size = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }
    public int CountCategories()
    {
        var list = dictionary.Keys.ToList();
        int count = (from x in list
                     select x.Split('_').First()).Distinct().Count();
        categories = (from x in list
                        select x.Split('_').First()).Distinct().ToList();
        return count;
    }

    public Texture2D LoadImage(string path)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(path))
        {
            fileData = File.ReadAllBytes(path);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
        }
        return tex;
    }

    public void GetImages(string path)
    {
        string result = null;
        TextReader tr = new StreamReader(path);
        string len;
        char[] split = new char[] { ',' };
        while ((len = tr.ReadLine()) != null && !len.Equals(""))
        {
            string[] array = len.Split(split);
            string code = array[0];
            result = array[1].Replace("file:///", "");
            string tag = array[2];
            var content = new Content(code, result, tag);
            dictionary[code] = content;
        }
        tr.Close();
    }
}

public class Content
{
    public Content(string code, string file, string tag)
    {
        Code = code;
        File = file;
        Tag = tag;
    }

    public string Code { get; set; }
    public string File { get; set; }
    public string Tag { get; set; }
}
