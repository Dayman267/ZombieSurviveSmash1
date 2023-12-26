using Mirror;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int darkEnergyPointsPerDeath;
    [SerializeField] private int minSolidMaterialPointsPerDeath;
    [SerializeField] private int maxSolidMaterialPointsPerDeath;
    public bool isDead = false;

    [ClientRpc]
    public void TakeDamage(int damage, uint owner)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            PointsManager manager = GameObject.FindWithTag("PointsManager").GetComponent<PointsManager>();
            manager.RpcAddDarkEnergyPoints(darkEnergyPointsPerDeath);
            
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                if (player.GetComponent<NetworkIdentity>().netId.Equals(owner))
                {
                    player.GetComponentInChildren<PlayerEnergyAndMaterialPoints>()
                        .AddSolidMaterial(Random.Range(minSolidMaterialPointsPerDeath, maxSolidMaterialPointsPerDeath+1));
                }
            }
            
            NetworkServer.Destroy(gameObject);
        }
    }
}
