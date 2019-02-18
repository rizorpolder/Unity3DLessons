using  UnityEngine;

    interface ISpawns
    {
        Rigidbody itemSpawned { get; set; }
        Renderer itemMaterial { get; set; }
        ItemPickUp itemType { get; set; }

        void CreateSpawn();
    }

