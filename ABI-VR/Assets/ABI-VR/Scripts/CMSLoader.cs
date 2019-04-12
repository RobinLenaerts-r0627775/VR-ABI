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
    private Dictionary<string, string> dictionary = new Dictionary<string, string>();

    IEnumerator Start()
    {
        // LOAD ALL IMAGES
        GetImages(_path);

        // GET CANVAS
        var canvas = GameObject.FindWithTag("Canvas");

        // CREATE BUTTONS
        for (int i = 0; i <= dictionary.Count - 1; i++)
        {
            string code = dictionary.Keys.ElementAt(i);
            string file = dictionary.Values.ElementAt(i);
            Texture2D texture = LoadImage(file);
            WWW www = new WWW(file);
            yield return www;
            www.LoadImageIntoTexture(texture);
            GameObject gob = new GameObject("Button");
            gob.transform.localPosition = new Vector3(0.5f * i, 0, 0);
            GameObject tob = new GameObject("Text");
            gob.transform.SetParent(canvas.transform, false);
            tob.transform.SetParent(gob.transform, false);
            var button = gob.AddComponent<Button>();
            button.transform.localScale = new Vector2(0.001f, 0.001f);
            //button.GetComponent<RectTransform>().sizeDelta = new Vector2(0.2f, 0.2f);
            var image = gob.AddComponent<Image>();
            // SPRITES DOESN'T WORK
            //image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            //image.sprite = Resources.GetBuiltinResource<Sprite>("unity_builtin_extra/UISprite");
            //image.sprite = Resources.Load<Sprite>("sprite");
            //image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            image.type = Image.Type.Sliced;
            var text = tob.AddComponent<Text>();
            text.text = code;
            text.font = Font.CreateDynamicFontFromOSFont("Arial", 50);
            text.color = Color.black;
            text.fontSize = 50;
            text.alignment = TextAnchor.MiddleCenter;
            button.onClick.AddListener(() => StartCoroutine(Test(code)));
            button.onClick.AddListener(() => Testje());
            var script = gob.AddComponent<VRInteractiveItem>();
            var box = gob.AddComponent<BoxCollider>();
            var mesh = gob.AddComponent<MeshRenderer>();
        }
    }

    public void Testje()
    {
        Debug.Log("TEST");
    }

    public IEnumerator Test(string code)
    {
        string file = dictionary[code];
        Texture2D texture = LoadImage(file);
        WWW www = new WWW(file);
        yield return www;
        www.LoadImageIntoTexture(texture);
        var go = GameObject.FindWithTag("Image");
        var image = go.GetComponent<RawImage>();
        image.transform.localScale = new Vector2(texture.width / 1000, texture.height / 1000);
        image.texture = texture;
        Debug.Log(code);
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
            dictionary[code] = result;
        }
        tr.Close();
    }
}
