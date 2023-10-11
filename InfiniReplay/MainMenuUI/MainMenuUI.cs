using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage.Attributes;
using IPA.Utilities.Async;

namespace InfiniReplay.MainMenuUI
{
    internal class InfiniReplayMenuUI : PersistentSingleton<InfiniReplayMenuUI>
    {
        internal List<string> _replayFiles;

        internal bool hasCollectedReplays = false;

        internal Action _onReplaySelected;

        [UIAction("#post-parse")]
        private void PostParse()
        {
            _replayFiles = new List<string>();
            UnityMainThreadTaskScheduler.Factory.StartNew(() => GetAllReplays());
            Plugin.Log.Info("InfiniReplayMenuUI PostParse");
        }

        [UIAction("startReplayWatching")]
        private void startReplayWatching()
        {
            _onReplaySelected.Invoke();
        }

        internal async Task GetAllReplays()
        {
            hasCollectedReplays = false;
            string replayFileLocation = "./UserData/LocalLeaderboard/Replays";

            try
            {
                string[] bsorFiles = Directory.GetFiles(replayFileLocation, "*.bsor");

                foreach (string bsorFile in bsorFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(bsorFile);

                    if (!fileName.Contains("-exit") && !fileName.Contains("-fail") && !fileName.Contains("-practice"))
                    {
                        _replayFiles.Add(bsorFile);
                    }
                }
                Plugin.Log.Notice($"ReplayFiles: {_replayFiles.Count}");
                hasCollectedReplays = true;
            }
            catch (Exception ex)
            {
                Plugin.Log.Error($"An error occurred: {ex.Message}");
                return;
            }
        }

        internal void AddReplay(string filename)
        {
            _replayFiles.Add(filename);
            Plugin.Log.Info("InfiniReplayMenuUI AddReplay");
        }
    }
}
