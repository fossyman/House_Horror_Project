using Godot;
using System;

namespace Helpers
{
    public class AudioHelper : Node
    {
    public RandomNumberGenerator rng = new RandomNumberGenerator();


    public void PlaySound(Spatial Position, AudioStream Sound)
    {
        AudioStreamPlayer player = new AudioStreamPlayer();
        player.Connect("finished",this,"queueForDeletion",new Godot.Collections.Array(){player});
        player.Stream = Sound;
        player.PitchScale = rng.RandfRange(0.5f,1.5f);
            player.Play();
        //Position.AddChild(player);
    }

        private void queueForDeletion(AudioStreamPlayer player)
        {
            player.QueueFree();
        }
    }
}