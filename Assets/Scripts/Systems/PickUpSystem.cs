using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using static Unity.Mathematics.math;

// @update!
public class PickUpSystem : JobComponentSystem
{
    struct PickUpSystemJob : IJobForEachWithEntity<Item, PickUp, Translation>
    {
        [ReadOnly] public BufferFromEntity<Inventory> inventoryData;
        public EntityCommandBuffer.Concurrent buffer;

        public void Execute(Entity entity, int index, [ReadOnly] ref Item item, ref PickUp pickUp, ref Translation translation)
        {
            buffer.AddComponent(index, pickUp.Value, new Hold { Item = entity }) ;
            translation.Value += new float3(0,0.2f,0);
        }
    }

    private EndSimulationEntityCommandBufferSystem endSimulationBuffer;

    protected override void OnCreate()
    {


        endSimulationBuffer = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();

        base.OnCreate();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new PickUpSystemJob
        {
            buffer = endSimulationBuffer.CreateCommandBuffer().ToConcurrent(),
            inventoryData = GetBufferFromEntity<Inventory>(true)
        };

        var jobHandle = job.Schedule(this, inputDeps);

        endSimulationBuffer.AddJobHandleForProducer(jobHandle);

        return jobHandle;
    }
}
