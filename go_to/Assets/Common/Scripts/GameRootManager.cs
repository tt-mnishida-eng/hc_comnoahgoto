using UnityEngine;
using UnityEngine.UI;
using Common;
using DG.Tweening;
using UniRx;

[DefaultExecutionOrder(-1)]
public class GameRootManager : SingletonMonoBehaviour<GameRootManager>
{
    [SerializeField] GameSceneManager sceneManager;
    public GameSceneManager SceneManager => sceneManager;

    [SerializeField] GameRootUI ui;
    public GameRootUI UI => ui;

    Subject<Unit> onNextUnit = new Subject<Unit>();
    public Subject<Unit> OnNextUnit => onNextUnit;

    Subject<Unit> onTapToStartUnit = new Subject<Unit>();
    public Subject<Unit> OnTapToStartUnit => onTapToStartUnit;

    Subject<Unit> onRetryUnit = new Subject<Unit>();
    public Subject<Unit> OnRetryUnit => onRetryUnit;


    [SerializeField] string[] sceneNames;

    protected override void Initialize()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
        LoadMainScene();
        StageMax = sceneNames.Length;
        // Bind();
    }

    void Bind()
    {
        ui.NextStageButton.onClick.AddListener(() => { onNextUnit.OnNext(Unit.Default); });
        ui.TapToStartButton.onClick.AddListener(() => { onTapToStartUnit.OnNext(Unit.Default); });
        ui.RetryButton.onClick.AddListener(() => { onRetryUnit.OnNext(Unit.Default); });
    }

    void LoadMainScene()
    {
        if (sceneNames.Length == 0) return;
        sceneManager.LoadAsyncScene(GetStageName(), null, (scene) =>
        {
            var stageObjects = scene.GetRootGameObjects();
            foreach (var stageObject in stageObjects)
            {
                // var stage = stageObject.GetComponent<IGameStage>();
                // if (stage != null)
                // {
                //     stage.SetRootUI(ui);
                // }
            }
        });
    }

    public void ReloadScene()
    {
        DOTween.KillAll();
        sceneManager.UnloadAsyncScene(GetStageName(), () => { LoadMainScene(); });
    }

    public void Retry()
    {
        DOTween.KillAll();
        sceneManager.UnloadAsyncScene(GetStageName(), () =>
        {
            // UI.Initialize();
            // sceneManager.LoadAsyncScene(GetStageName());
            LoadMainScene();
        });
    }

    string GetStageName()
    {
        return sceneNames[CurrentStageCount];
    }

    public void DebugProgress()
    {
        var idx = PlayerPrefs.GetInt("Progress", 0);
        PlayerPrefs.SetInt("Progress", idx + 1);
        ReloadScene();
    }

    public void DeletePrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    int StageMax = 1;
    int CurrentStageCount = 0;
    public int ProgressCount = 0;

    public void ProgressStage()
    {
        if (sceneManager.IsLoading) return;

        DOTween.KillAll();
        sceneManager.UnloadAsyncAllSubScenes(() =>
        {
            sceneManager.UnloadAsyncScene(GetStageName(), () =>
            {
                if (CurrentStageCount >= StageMax - 1)
                    CurrentStageCount = 0;
                else
                    CurrentStageCount++;
                ProgressCount++;

                UI.Initialize();
                sceneManager.LoadAsyncScene(GetStageName(), null, (scene) => { SetStageName(ProgressCount); });
            });
        });
    }

    public void BackStage()
    {
        if (sceneManager.IsLoading) return;

        DOTween.KillAll();
        sceneManager.UnloadAsyncAllSubScenes(() =>
        {
            sceneManager.UnloadAsyncScene(GetStageName(), () =>
            {
                if (CurrentStageCount <= 0)
                    CurrentStageCount = 0;
                else
                    CurrentStageCount--;

                if (ProgressCount <= 0)
                    ProgressCount = 0;
                else
                    ProgressCount--;

                UI.Initialize();
                sceneManager.LoadAsyncScene(GetStageName(), null, (scene) => { SetStageName(ProgressCount); });
            });
        });
    }


    [SerializeField] TMPro.TextMeshProUGUI stageName;

    void SetStageName(int stageLevel)
    {
        if (stageName != null)
            stageName.text = $"Stage {stageLevel + 1}";
    }

    // [SerializeField]
    // TMPro.TextMeshProUGUI digSizeText;
    // DigAndHelp.PlayableDigger digger;
    // public void AddDigSizeForDebug(float value)
    // {
    //     if (digger == null)
    //     {
    //         digger = GameObject.FindGameObjectWithTag("PlayableDigger").GetComponent<DigAndHelp.PlayableDigger>();
    //     }
    //     if (digger != null)
    //     {
    //         digger.AddDigSize(value);
    //         digSizeText.text = digger.DigSize.ToString();
    //     }

    // }
    // [SerializeField]
    // TMPro.TextMeshProUGUI ropeSizeText;


    // DigAndHelp.Rope rope;
    // public void ChangeRopeLengthForDebug(float value)
    // {
    //     if (rope == null)
    //     {
    //         rope = GameObject.FindGameObjectWithTag("Rope").GetComponent<DigAndHelp.Rope>();
    //     }
    //     if (rope != null)
    //     {
    //         if (rope.MaxLength > 11)
    //         {
    //             rope.MaxLength = 11;
    //             ropeSizeText.text = $"MaxLength = {rope.MaxLength}";
    //             return;
    //         }
    //         if (rope.MaxLength < 2)
    //         {
    //             rope.MaxLength = 2;
    //             ropeSizeText.text = $"MaxLength = {rope.MaxLength}";
    //             return;
    //         }
    //         rope.MaxLength += value;
    //         ropeSizeText.text = $"MaxLength = {rope.MaxLength}";
    //     }
    // }
    // public void SwitchGoalViewForDebug()
    // {
    //     var gameStage = GameObject.FindGameObjectWithTag("GameStage").GetComponent<DriveInStagePresenter>();
    //     if (gameStage != null)
    //         gameStage.NodeContainer.SwitchGoalViewForDebug();
    // }
}