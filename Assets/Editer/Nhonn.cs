// using UnityEditor;
// using UnityEngine;
//
// [InitializeOnLoad]
// public class BackgroundWatcher
// {
//     static BackgroundWatcher()
//     {
//         EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
//     }
//
//     private static void OnPlayModeStateChanged(PlayModeStateChange state)
//     {
//         if (!PlaymodeTestsController.IsControllerOnScene())
//             return;
//
//         var runner = PlaymodeTestsController.GetController();
//         if (runner == null)
//         {
//             Debug.LogError("Runner is null");
//             return;
//         }
//         if (runner.m_Runner == null)
//         {
//             Debug.LogError("runner.m_Runner is null");
//             return;
//         }
//         if (runner.settings == null)
//         {
//             Debug.LogError("runner.settings is null");
//             return;
//         }
//
//         if (state == PlayModeStateChange.ExitingPlayMode)
//         {
//             AssetDatabase.DeleteAsset(runner.settings.bootstrapScene);
//
//             if (runner.m_Runner.LoadedTest != null)
//             {
//                 var filter = runner.settings.BuildNUnitFilter();
//                 ExecutePostBuildCleanupMethods(runner.m_Runner.LoadedTest, filter, Application.platform);
//             }
//             else
//             {
//                 Debug.LogError("runner.m_Runner.LoadedTest is null");
//             }
//
//             HasFinished = true;
//         }
//         else if (state == PlayModeStateChange.EnteredEditMode)
//         {
//             ConsoleWindow.SetConsoleErrorPause(runner.settings.consoleErrorPaused);
//             Application.runInBackground = runner.settings.runInBackgroundValue;
//             // reopen the original scene once we exit playmode
//             ReopenOriginalScene(runner);
//         }
//     }
//
//     private static void ExecutePostBuildCleanupMethods(object loadedTest, object filter, RuntimePlatform platform)
//     {
//         // Implement your cleanup methods here
//     }
//
//     private static void ReopenOriginalScene(object runner)
//     {
//         // Implement your logic to reopen the original scene here
//     }
//
//     private static bool HasFinished { get; set; }
// }