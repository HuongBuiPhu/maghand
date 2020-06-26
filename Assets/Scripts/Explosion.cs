using System;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public const string TAG = "Explosion";

    public Action do_after_boom = null;

    [SerializeField]
    private ParticleSystem particle_system = null;

    public void Boom() {
        this.particle_system.Play();
        Invoke("DoAfterBoom", this.particle_system.main.duration);
    }

    public void DoAfterBoom() {
        if (do_after_boom != null) do_after_boom();
    }
}
