using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class CollisionSystem : JobComponentSystem
{
    [BurstCompile]
    struct CollisionSystemJob : IJobForEachWithEntity<AABB, Collision>
    {
        [ReadOnly] public NativeArray<AABB> colliders;

        public void Execute(Entity entity, int index, 
            [ReadOnly] ref AABB aabb,
            ref Collision collision
            )
        {
            for (int j = index + 1; j < colliders.Length; j++)
            {
                bool4 collide = SimplePhysics.Collision(colliders[index], colliders[j]);
                if (SimplePhysics.Intersect(colliders[index], colliders[j]))
                {
                    //if (collide.x)
                    //{
                    //    collision.Value = new float3(0, -1, 0);
                    //}
                    //if (collide.y)
                    //{
                    //    collision.Value = new float3(0, 1, 0);
                    //}
                    //if (collide.z)
                    //{
                    //    collision.Value = new float3(1, 0, 0);
                    //}
                    //if (collide.w)
                    //{
                    //    collision.Value = new float3(-1, 0, 0);
                    //}
                    UnityEngine.Debug.Log("Collision");
                }
                else
                {
                    //collision.Value = float3.zero;
                }
            }
        }
    }

    EntityQuery m_aabbQuery;

    protected override void OnCreate()
    {
        m_aabbQuery = GetEntityQuery(typeof(AABB));
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var colliders = m_aabbQuery.ToComponentDataArray<AABB>(Allocator.TempJob);

        var job = new CollisionSystemJob
        {
            colliders = colliders
        };
        
        var jobHandle = job.Schedule(this, inputDeps);

        jobHandle.Complete();

        colliders.Dispose();

        return jobHandle;
    }
}
