using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CMSLoader : MonoBehaviour
{
    private string _path = "Assets/ABI-VR/CMS/CMS.txt";
    private Dictionary<string, string> dictionary = new Dictionary<string, string>();

    IEnumerator Start()
    {
        // LOAD ALL IMAGES
        GetImages(_path);

        // GET CANVAS
        var canvas = GameObject.FindWithTag("Canvas");

        // CREATE BUTTONS USING CUBES
        for (int i = 0; i <= dictionary.Count - 1; i++)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localPosition = new Vector3(-500, -500, -500);
            cube.transform.localScale = new Vector2(1000, 1000);
        }

        // LOOP IMAGES
        for (int i = 0; i <= dictionary.Count - 1; i++)
        {
            string code = dictionary.Keys.ElementAt(i);
            string file = dictionary.Values.ElementAt(i);

            Texture2D texture = LoadImage(file);
            WWW www = new WWW(file);
            yield return www;
            www.LoadImageIntoTexture(texture);
            var go = GameObject.FindWithTag("Image");
            var image = go.GetComponent<RawImage>();

            if (texture.width >= 1600)
            {
                image.transform.localScale = new Vector2(texture.width / 200, texture.height / 200);
            }
            else
            {
                image.transform.localScale = new Vector2(texture.width / 100, texture.height / 100);
            }

            image.texture = texture;

            // CODE
            var codeGO = (GameObject)GameObject.FindWithTag("Code");
            var codeComp = codeGO.GetComponent<Text>();
            codeComp.text = "Code : " + code;

            yield return new WaitForSecondsRealtime(2.0f);
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
            dictionary[code] = result;
        }
        tr.Close();
    }
}
