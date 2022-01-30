using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetOpacity : MonoBehaviour
{
    private List<Material> materials;
    private bool materialTransparency = false;
    private Color newColor;

    void Awake()
    {
        // Get reference to materials
        materials = GetComponent<Renderer>().materials.ToList();
    }

    public void ChangeOpacity(bool transparency)
    {
        if (transparency != materialTransparency)
        {
            if (transparency)
            {
                foreach (var material in materials)
                {
                    newColor = material.color;
                    newColor.a = 0.0f;
                    material.SetColor("_Color", newColor);
                }
            }
            else
            {
                foreach (var material in materials)
                {
                    newColor = material.color;
                    newColor.a = 1.0f;
                    material.SetColor("_Color", newColor);
                }
            }

            materialTransparency = transparency;
        }
    }
}
