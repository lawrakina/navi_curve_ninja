using System;
using System.Collections.Generic;
using System.Linq;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    public static PlayerSkins Instance { get; private set; }

    public List<SkinContainer> containers = new List<SkinContainer>();
    public List<GameObject> allDeactivateObjects = new List<GameObject>();

    private int selectedIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (containers.All(e => e.ID != GlobalParameters.SkinID))
        {
            selectedIndex = 0;
            GlobalParameters.SkinID = containers[0].ID;
        }

        selectedIndex = containers.FindIndex(e => e.ID == GlobalParameters.SkinID);
    }

    public void NextSkin()
    {
        selectedIndex++;
        if (selectedIndex >= containers.Count) selectedIndex = 0;
        foreach (var o in allDeactivateObjects)
        {
            o.SetActive(false);
        }
        if (!containers[selectedIndex].isLocked)
        {
            GlobalParameters.SkinID = containers[selectedIndex].ID;
            SetActiveSkin();
        }
        else
        {
            containers[selectedIndex].Activate();
        }
    }

    public SkinContainer GetSkinData(out int index)
    {
        index = selectedIndex;
        return containers[selectedIndex];
    }

    public void UpdateSelected()
    {
        foreach (var o in allDeactivateObjects)
        {
            o.SetActive(false);
        }
        if (!containers[selectedIndex].isLocked)
        {
            GlobalParameters.SkinID = containers[selectedIndex].ID;
            SetActiveSkin();
        }
        else
        {
            containers[selectedIndex].Activate();
        }
    }

    public void PreviousSkin()
    {
        selectedIndex--;
        if (selectedIndex < 0) selectedIndex = containers.Count - 1;
        foreach (var o in allDeactivateObjects)
        {
            o.SetActive(false);
        }
        if (!containers[selectedIndex].isLocked)
        {
            GlobalParameters.SkinID = containers[selectedIndex].ID;
            SetActiveSkin();
        }
        else
        {
            containers[selectedIndex].Activate();
        }
    }

    public void SetActiveSkin()
    {
        foreach (var o in allDeactivateObjects.Where(e => e != null))
        {
            o.SetActive(false);
        }

        //containers.FirstOrDefault(e => e.ID == GlobalParameters.SkinID).Activate();
    }

}

[Serializable]
public class SkinContainer
{
    public int ID;
    public List<SkinPartContainer> skinPack = new List<SkinPartContainer>();
    public bool isStartLocked = false;
    public int price;
    public string Name;
    public bool isLocked
    {
        get
        {
            return bool.Parse(SaveManager.Load("SKIN_LOCKED_" + ID, isStartLocked.ToString()));
        }
        set
        {
            SaveManager.Save("SKIN_LOCKED_" + ID, value.ToString());

        }
    }

    public void Activate()
    {
        foreach (var skin in skinPack)
        {
            skin.go.SetActive(true);
            skin.renderer.material = UnityEngine.Object.Instantiate(skin.material);
        }
    }
}

[Serializable]
public class SkinPartContainer
{
    public GameObject go;
    public SkinnedMeshRenderer renderer;
    public Material material;
}