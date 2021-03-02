using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;
public class TimeLinePlayer : MonoBehaviour
{
    public enum Skit
    {
        Success,
        Failure
    }
    Skit currentSkit;
    public PlayableDirector CurrentSkit => skits[(int)currentSkit];
    [SerializeField]
    PlayableDirector successSkit;
    [SerializeField]
    PlayableDirector failureSkit;

    Dictionary<int, PlayableDirector> skits = new Dictionary<int, PlayableDirector>();
    public Action OnIntroductionLoopStart { get; set; }
    public Action OnIntroductionLoopFinish { get; set; }
    public Action OnEndingLoopStart { get; set; }
    public Action OnEndingLoopFinish { get; set; }
    public Action OnGoToResult { get; set; }
    public Action OnExplode { get; set; }
    public double IntroductionLoopStartSeconds { get; set; } = 0;
    public double IntroductionLoopFinishSeconds { get; set; } = 0;
    public double EndingLoopStartSeconds { get; set; } = 0;
    public double EndingLoopFinishSeconds { get; set; } = 0;

    public bool IsIntroductionLoop { get; set; } = false;
    public bool IsEndingLoop { get; set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        IsIntroductionLoop = true;
        IsEndingLoop = true;
        skits.Add((int)Skit.Success, successSkit);
        skits.Add((int)Skit.Failure, failureSkit);
        foreach (var skit in skits.Values)
        {
            skit.Pause();
            skit.stopped += Stopped;
        }

    }

    public void Play<T>(T skit, double time = 0) where T : Enum
    {

        var idx = Convert.ToInt16(skit);
        var targetSkit = skits[idx];
        if (CurrentSkit != targetSkit)
        {
            if (CurrentSkit == null)
            {
                currentSkit = (Skit)idx;
            }
            else
            {
                CurrentSkit.Stop();
                CurrentSkit.enabled = false;
                currentSkit = (Skit)idx;
            }

            CurrentSkit.enabled = true;
        }
        if (time > 0.01f)
            CurrentSkit.time = time;
        CurrentSkit.Play();

    }
    public void Pause()
    {
        CurrentSkit.Pause();
    }

    public void ReceiveIntroductionLoopStart()
    {
        if (OnIntroductionLoopStart != null) OnIntroductionLoopStart();
        IntroductionLoopStartSeconds = CurrentSkit.time;
    }
    public void ReceiveIntroductionLoopFinish()
    {
        if (OnIntroductionLoopFinish != null) OnIntroductionLoopFinish();

        IntroductionLoopFinishSeconds = CurrentSkit.time;
        if (IsIntroductionLoop)
            Play<Skit>(currentSkit, IntroductionLoopStartSeconds);


    }
    public void ReceiveEndingLoopStart()
    {
        if (OnEndingLoopStart != null) OnEndingLoopStart();
        EndingLoopStartSeconds = CurrentSkit.time;

    }
    public void ReceiveEndingLoopFinish()
    {
        if (OnEndingLoopFinish != null) OnEndingLoopFinish();

        EndingLoopFinishSeconds = CurrentSkit.time;
        if (IsEndingLoop)
            Play<Skit>(currentSkit, EndingLoopStartSeconds);
    }

    public void ReceiveGoToResult()
    {
        if (OnGoToResult != null) OnGoToResult();
    }

    public void ReceiveExplode()
    {
        if (OnExplode != null) OnExplode();
    }
    void Stopped(PlayableDirector director)
    {
        // Play<Skit>(currentSkit, EndingLoopStartSeconds);
    }
}
