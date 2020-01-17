using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

public class HoldSystem : JobComponentSystem
{
    struct HoldSystemJob : IJobForEachWithEntity<Hold>
    {
        public void Execute(Entity entity, int index, ref Hold hold)
        {
            
        }
    }
    
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        var job = new HoldSystemJob();
        
        return job.Schedule(this, inputDependencies);
    }
}
