using UnityEngine;

public class BlockFaller : MonoBehaviour {
    public Block Block;
    public FloatReference FallDelayDuration;
    public FloatReference FallDuration;
    float delayElapsed;
    public float Elapsed;
    public Block Target;
    public bool JustFell;
    public bool JustLanded;

    public void Fall() {
        Block.State = BlockState.WaitingToFall;
        delayElapsed = 0f;
    }

    public void ContinueFalling() {
        FinishWaitingToFall();
    }

    void FinishWaitingToFall() {
        Block.State = BlockState.Falling;
        Elapsed = 0f;
    }

    void FixedUpdate() {
        if(Block.State == BlockState.WaitingToFall) {
            delayElapsed += Time.deltaTime;

            if(delayElapsed >= FallDelayDuration.Value) {
                FinishWaitingToFall();
            }
        }

        if(Block.State == BlockState.Falling) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= FallDuration.Value) {
                if(Target != null) {
                    Target.Garbage.Width = Block.Garbage.Width;
                    Target.Garbage.Height = Block.Garbage.Height;
                    Target.Garbage.IsNeighbor = Block.Garbage.IsNeighbor;
                    Target.Type = Block.Type;
                    Target.State = BlockState.Falling;
                    Target.Faller.JustFell = true;
                    Target.Faller.Elapsed = 0;
                    Target.Chainer.ChainEligible = Block.Chainer.ChainEligible;
                }
                
                Block.Garbage.Width = 1;
                Block.Garbage.Height = 1;
                Block.Garbage.IsNeighbor = false;
                Block.State = BlockState.Empty;
                Block.Type = -1;
                Block.Chainer.ChainEligible = false;
            }
        }
    }
}