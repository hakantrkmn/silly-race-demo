using Es.InkPainter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class percentage : MonoBehaviour
{
    RenderTexture texture;
    Texture2D texture2D;
    public static Action<float> onPercentChanged;
    float percent;

    private void Update()
    {
        //eğer oyun paint durumunda ise ve ekrana dokunulmuş ise boyadığımız nesnenin main texturesini alıyoruz
        if (GameManager.Instance.gameState==GameManager.gameStates.paint)
        {
            if (Input.GetMouseButton(0))
            {
                GetMainTexture(gameObject.GetComponent<InkCanvas>());
            }
        }
        
    }
    //main textureyi almamıza yarayan fonksiyon
    private void GetMainTexture(InkCanvas obj)
    {
        foreach (var item in obj.PaintDatas)
        {
            texture = item.paintMainTexture;
        }

        texture2D = toTexture2D(texture);
        readPixels(texture2D);
        
    }

    //aldığımız textureyi pixel pixel okuyarak kırmızı mı değil mi diye kontrol ediyoruz. kırmızıysa oranlayarak actionu harekete geçiyoruz
    //eğer yüzde değişmiş ise aciton harekete geçiyor.
    void readPixels(Texture2D texture)
    {
        Debug.Log(texture.width + " " + texture.height);
        var redPix = 0;
        var otherPix = 0;
        for (int i = 0; i < 32; i++)
            for (int j = 0; j < 32; j++)
            {
                Color pixel = texture.GetPixel(i, j);
                if (pixel==Color.red)
                {
                    redPix++;
                }
                else
                    otherPix++;
            }

        if (percent != ((redPix * 100) / 1024) && percent < 100)
        {
            percent = (redPix * 100) / 1024;
            onPercentChanged(percent);
        }
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(32, 32, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
