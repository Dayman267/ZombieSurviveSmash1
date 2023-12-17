using UnityEngine;
using Mirror;

public class PlayerEnergyAndMaterialPoints : NetworkBehaviour
{
    public PointsManager manager;
    private int solidMaterial;

    private void Start()
    {
        manager = GameObject.FindWithTag("PointsManager").GetComponent<PointsManager>();
    }

    private void Update()
    {
        if(!isLocalPlayer) return;

        for (int i = 0; i < manager.darkEnergyText.Length; i++)
            manager.darkEnergyText[i].text = $"{manager.darkEnergyPoints}/{manager.darkEnergyPointsToAccess}";
        for (int i = 0; i < manager.solidMaterialText.Length; i++)
            manager.solidMaterialText[i].text = $"{solidMaterial}";
        if (Input.GetKeyDown(KeyCode.Space)) AddSolidMaterial(20);
    }

    public void AddSolidMaterial(int points)
    {
        solidMaterial += points;
    }
}
