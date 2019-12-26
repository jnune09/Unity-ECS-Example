using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Collections; 
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
//using Unity.Physics;

public class GameManager : MonoBehaviour
{
    EntityManager entityManager;
    [SerializeField] private Mesh quadMesh;
    [SerializeField] private Material actorMaterial;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material vendorMaterial;

    void Start()
    {
        Vector3[] characterSpriteVertices = new Vector3[4];

        characterSpriteVertices[0] = new Vector3(-8, -16);
        characterSpriteVertices[1] = new Vector3(8, -16);
        characterSpriteVertices[2] = new Vector3(-8, 16);
        characterSpriteVertices[3] = new Vector3(8, 16);

        quadMesh.vertices = characterSpriteVertices;

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        for (int i = 0; i < 1; i++)
        {
            //SpawnPlayer();
            SpawnActor();
        }

        SpawnPlayer();
        SpawnVendor();

    }

    public void SpawnPlayer()
    {
        Entity player = entityManager.CreateEntity(
            typeof(PlayerTag),
            typeof(PlayerInput),
            typeof(AABB),
            typeof(Hunger),
            typeof(Inventory),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(Translation),
            typeof(Velocity)
            );

        //Entity item = entityManager.CreateEntity();
        //entityManager.GetBuffer<Inventory>(player).Add(new Inventory { Item = item, Count = 1 });


        entityManager.SetComponentData(player, new Hunger { Value = 10 });
        entityManager.SetComponentData(player, new Velocity { Speed = 80f });
        entityManager.SetComponentData(player, new Translation { Value = new float3(UnityEngine.Random.Range(-48, 48), 5, 0) });

        entityManager.SetSharedComponentData(player, new RenderMesh { mesh = quadMesh, material = playerMaterial });
    }

    public void SpawnActor()
    {
        Entity actor = entityManager.CreateEntity(
            typeof(ActorTag),
            typeof(AABB),
            typeof(Hunger),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(State),
            typeof(Translation),
            typeof(Velocity)
            );

        entityManager.SetComponentData(actor, new Hunger { Value = 10 });
        entityManager.SetComponentData(actor, new Velocity { Speed = 70f });
        entityManager.SetComponentData(actor, new Translation { Value = new float3(UnityEngine.Random.Range(-150, 150), UnityEngine.Random.Range(-100, 100), 0) });

        entityManager.SetSharedComponentData(actor, new RenderMesh { mesh = quadMesh, material = actorMaterial });
    }

    public void SpawnVendor()
    {
        Entity vendor = entityManager.CreateEntity(
            typeof(VendorTag),
            typeof(AABB),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(Translation)
            );

        entityManager.SetComponentData(vendor, new Translation { Value = new float3(-15, 5, 0) });
        entityManager.SetSharedComponentData(vendor, new RenderMesh { mesh = quadMesh, material = vendorMaterial });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
        }
    }
}
