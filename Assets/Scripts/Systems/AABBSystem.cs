using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class AABBSystem : JobComponentSystem
{
    [BurstCompile]
    struct AABBSystemJob : IJobForEach<AABB, Translation>
    {
        public void Execute(ref AABB aabb, [ReadOnly] ref Translation translation)
        {
            float3 min = new float3(-8f, -16f, 0);
            float3 max = new float3(8f, 16f, 0);
            aabb.Min = translation.Value + min;
            aabb.Max = translation.Value + max;
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new AABBSystemJob();
        

        return job.Schedule(this, inputDeps);
    }
}
