using BeatSaberMarkupLanguage.GameplaySetup;
using InfiniReplay.MainMenuUI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace InfiniReplay
{
    internal class MenuHandler : IInitializable
    {
        private ReplayLoader.ReplayService _replayService;

        [Inject]
        public void Inject(ReplayLoader.ReplayService replayService)
        {
            _replayService = replayService;
        }

        public void Initialize()
        {
            Plugin.Log.Info("MenuHandler Initialize");
            InfiniReplayMenuUI.instance._onReplaySelected += OnReplaySelected;
            GameplaySetup.instance.AddTab("InfiniReplay", "InfiniReplay.MainMenuUI.settings.bsml", InfiniReplayMenuUI.instance, MenuType.All);
        }

        public void OnReplaySelected()
        {
            if(_replayService.TryReadReplay(InfiniReplayMenuUI.instance._replayFiles[Random.Range(0, InfiniReplayMenuUI.instance._replayFiles.Count)], out BeatLeader.Models.Replay.Replay replay))
            {
                _replayService.StartReplay(replay, null);
            }
        }
    }
}
