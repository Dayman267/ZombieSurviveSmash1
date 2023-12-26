using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PointsManager : NetworkBehaviour
{
    [SyncVar] public int darkEnergyPoints;
    [SyncVar] public int darkEnergyPointsToAccess;
    public int pointsPerTime;
    public int secondsBetweenAward;

    [SyncVar] public int firstFibonacci;
    [SyncVar] public int secondFibonacci;

    public Text[] darkEnergyText;
    //public Text[] solidMaterialText;

    [SyncVar] public int counterICanEnter;

    private void Start()
    {
        if(!isServer) return;
        firstFibonacci = secondFibonacci = darkEnergyPointsToAccess;
        StartCoroutine(DarkEnergyTimeIncreaser());
    }
    
    [ClientRpc]
    public void RpcAddDarkEnergyPoints(int points)
    {
        darkEnergyPoints += points;
        if (darkEnergyPoints >= darkEnergyPointsToAccess)
        {
            darkEnergyPointsToAccess = firstFibonacci + secondFibonacci;
            secondFibonacci = firstFibonacci;
            firstFibonacci = darkEnergyPointsToAccess;
            counterICanEnter += 1;
        }
    }

    [ClientRpc]
    public void RpcSubtractOneCounterICanEnter()
    {
        counterICanEnter -= 1;
    }

    private IEnumerator DarkEnergyTimeIncreaser()
    {
        while (true)
        {
            RpcAddDarkEnergyPoints(pointsPerTime);
            yield return new WaitForSeconds(secondsBetweenAward);
        }
    }
}
