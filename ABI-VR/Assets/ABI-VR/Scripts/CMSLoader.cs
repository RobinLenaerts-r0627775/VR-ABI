using Interactive360.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CMSLoader : MonoBehaviour
{
    private string _path = "E:\\CMS\\CMS.txt";
    private Dictionary<string, Content> dictionary = new Dictionary<string, Content>();

    IEnumerator Start()
    {
        // LOAD ALL IMAGES
        GetImages(_path);

        // GET CANVAS
        var canvas = GameObject.FindWithTag("Canvas");

        // GRID
        float row = 0;
        float column = -0.9f;

        // DEFAULT SKYBOX IS NEEDED FOR VIDEO
        Material yourMaterial = (Material)Resources.Load("3D_Stella_Filter_1", typeof(Material));
        RenderSettings.skybox = yourMaterial;

        // CREATE BUTTONS IN GRID FOR IMAGES AND VIDEOS
        for (int i = 0; i <= dictionary.Count - 1; i++)
        {
            if (i != 0 && i % 3 == 0)
            {
                row -= 0.9f;
                column = -0.9f;
            }
            string code = dictionary.Keys.ElementAt(i);
            string file = dictionary.Values.ElementAt(i).File;
            string tag = dictionary.Values.ElementAt(i).Tag;
            Texture2D texture = LoadImage(file);
            WWW www = new WWW(file);
            yield return www;
            if (code.Equals("SOUND"))
            {
                var sound = www.GetAudioClip(false, false);
                var aobj = GameObject.FindWithTag("Audio");
                var audiosource = aobj.GetComponent<AudioSource>();
                audiosource.clip = sound;
                audiosource.playOnAwake = true;
                audiosource.Play();
            } else
            {
                www.LoadImageIntoTexture(texture);
            }
            GameObject gob = new GameObject("Button");
            gob.transform.localPosition = new Vector3(column, row, 2);
            column += 1.9f;
            gob.layer = 5;
            GameObject tob = new GameObject("Text");
            gob.transform.SetParent(canvas.transform, false);
            tob.transform.SetParent(gob.transform, false);
            var button = gob.AddComponent<Button>();
            ColorBlock colorVar = button.colors;
            colorVar.highlightedColor = Color.blue;
            button.colors = colorVar;
            var rect = gob.AddComponent<RectTransform>().sizeDelta = new Vector2(1.5f, 0.5f);
            var image = gob.AddComponent<Image>();
            image.type = Image.Type.Sliced;
            var text = tob.AddComponent<Text>();
            text.transform.localScale = new Vector2(0.002f, 0.002f);
            text.text = code;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.font = Font.CreateDynamicFontFromOSFont("Arial", 15);
            text.color = Color.black;
            text.fontSize = 50;
            text.alignment = TextAnchor.MiddleCenter;
            button.onClick.AddListener(() => Wrapper(code, tag));
            var script = gob.AddComponent<VRInteractiveItem>();
            var box = gob.AddComponent<BoxCollider>();
            box.size = new Vector3(0.5f, 0.5f, 0.5f);
        }

        // GET VIDEOPLAYER WITH DEFAULT VIDEO
        var vobj = GameObject.FindWithTag("Video");
        var videoplayer = vobj.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoplayer.url = "E:\\CMS\\videos\\START.mp4";

        // CREATE AUDIOPLAYER

    }

    public void Wrapper(string code, string tag)
    {
        StartCoroutine(DisplayImage(code, tag));
    }

    public IEnumerator DisplayImage(string code, string tag)
    {
        // JUST A TEST CODE
        if (code.Equals("VIDEO"))
        {
            Material yourMaterial = (Material)Resources.Load("3D_Stella_Filter_1", typeof(Material));
            RenderSettings.skybox = yourMaterial;
            var vobj = GameObject.FindWithTag("Video");
            var videoplayer = vobj.GetComponent<UnityEngine.Video.VideoPlayer>();
            videoplayer.url = dictionary[code].File;
        } else if (code.Equals("SOUND"))
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
        while ((len = tr.ReadLine()) != null)
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
