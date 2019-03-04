using UnityEngine;


   public class SpawnItem : MonoBehaviour, ISpawns
   {
       public ItemPickUps_SO[] itemDefenitions;

       private int whitchToSpawn = 0;
       private int totalSpawnWeight = 0;
       private int chosen = 0;


        public Rigidbody itemSpawned { get; set; }
        public Renderer itemMaterial { get; set; }
        public ItemPickUp itemType { get; set; }


       void Start()
       {
           foreach (ItemPickUps_SO ip in itemDefenitions)
           {
               totalSpawnWeight += ip.spawnChanceWeight;
           }
       }
        public void CreateSpawn()
        {
            chosen = Random.Range(0, totalSpawnWeight);
            foreach (ItemPickUps_SO ip in itemDefenitions)
            {
                whitchToSpawn += ip.spawnChanceWeight;
                if (whitchToSpawn >= chosen)
                {
                    itemSpawned = Instantiate(ip.itemSpawnObject,
                        new Vector3(transform.position.x, transform.position.y, transform.position.z),
                        Quaternion.identity);

                    itemMaterial = itemSpawned.GetComponent<Renderer>();
                    itemMaterial.material = ip.itemMaterial;

                    itemType = itemSpawned.GetComponent<ItemPickUp>();
                    itemType.itemDefinition = ip;
                    break;
                }

            }
        }
    }

