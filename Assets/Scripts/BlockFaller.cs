using UnityEngine;

public class BlockFaller : MonoBehaviour {
    public Block Block;
    public BlockData BlockData;
    float delayElapsed;
    public float Elapsed;
    public Block Target;
    public bool JustFell;

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

    void Update() {
        if(Block.State == BlockState.WaitingToFall) {
            delayElapsed += Time.deltaTime;

            if(delayElapsed >= BlockData.FallDelayDuration) {
                FinishWaitingToFall();
            }
        }

        if(Block.State == BlockState.Falling) {
            Elapsed += Time.deltaTime;

            if(Elapsed >= BlockData.FallDuration) {
                if(Target != null) {
                    Target.Garbage.Width = Block.Garbage.Width;
                    Target.Garbage.Height = Block.Garbage.Height;
                    Target.Garbage.IsNeighbor = Block.Garbage.IsNeighbor;
                    Target.Type = Block.Type;
                    Target.State = BlockState.Falling;
                    Target.Faller.JustFell = true;
                    Target.Chainer.ChainEligible = Block.Chainer.ChainEligible;
                }
                
                Block.Garbage.Width = 1;
                Block.Garbage.Height = 1;
                Block.Garbage.IsNeighbor = false;
                Block.State = BlockState.Empty;
                Block.Type = -1;
            }
        }
    }
}