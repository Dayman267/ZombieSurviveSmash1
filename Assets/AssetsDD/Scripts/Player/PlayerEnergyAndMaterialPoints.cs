using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerEnergyAndMaterialPoints : NetworkBehaviour
{
    private PointsManager manager;
    private int solidMaterial;
    public Text[] solidMaterialText;

    private void Start()
    {
        if(!isLocalPlayer) return;
        manager = GameObject.FindWithTag("PointsManager").GetComponent<PointsManager>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag("SolidMaterial");
        solidMaterialText = new Text[9];
        for (int i = 0; i < 9; i++) solidMaterialText[i] = gos[i].GetComponent<Text>();
    }

    private void Update()
    {
        if(!isLocalPlayer) return;

        for (int i = 0; i < manager.darkEnergyText.Length; i++)
            manager.darkEnergyText[i].text = $"{manager.darkEnergyPoints}/{manager.darkEnergyPointsToAccess}";
        
        for (int i = 0; i < solidMaterialText.Length; i++)
            solidMaterialText[i].text = $"{solidMaterial}";
    }
    
    public void AddSolidMaterial(int points)
    {
        solidMaterial += points;
    }
}
