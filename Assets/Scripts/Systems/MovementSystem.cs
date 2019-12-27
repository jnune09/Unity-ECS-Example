using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class MovementSystem : JobComponentSystem
{

    [BurstCompile]
    struct MovementSystemJob : IJobForEach<Velocity, Translation>
    {
        public float deltaTime;
        public void Execute(
            [ReadOnly] ref Velocity velocity,
            ref Translation translation
            )
        {
            translation.Value += velocity.Value * deltaTime;
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new MovementSystemJob()
        {
            deltaTime = UnityEngine.Time.deltaTime
        };

        return job.Schedule(this, inputDeps);
    }
}
