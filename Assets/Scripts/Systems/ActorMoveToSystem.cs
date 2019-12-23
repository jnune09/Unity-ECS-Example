using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class ActorMoveToSystem : JobComponentSystem
{

    [BurstCompile]
    struct ActorMoveToSystemJob : IJobForEach<Destination, Translation, MoveSpeed>
    {
        public float deltaTime;
        
        public void Execute([ReadOnly] ref Destination destination, ref Translation translation, ref MoveSpeed moveSpeed)
        {
            float3 targetDirection = math.normalize(destination.Value - translation.Value);

            translation.Value += targetDirection * moveSpeed.Value * deltaTime;

            if (math.distance(translation.Value, destination.Value) < 2f)
            {
                moveSpeed.Value = 0;
            }
            
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new ActorMoveToSystemJob()
        {
            deltaTime = UnityEngine.Time.deltaTime
        };

        return job.Schedule(this, inputDeps);
    }
}
 