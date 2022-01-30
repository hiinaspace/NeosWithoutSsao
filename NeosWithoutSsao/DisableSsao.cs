using MelonLoader;
using System.Collections;
using UnityEngine;

namespace NeosWithoutSsao
{
    public class DisableSsao : MelonMod
    {
        public static MelonPreferences_Category prefCategory;
        public static MelonPreferences_Entry<bool> ssaoEnabled;
        public static MelonPreferences_Entry<bool> postProcessingEnabled;


        public override void OnApplicationStart()
        {
            prefCategory = MelonPreferences.CreateCategory("NeosWithoutSsao");
            ssaoEnabled = prefCategory.CreateEntry("ssaoEnabled", default_value: false);
            postProcessingEnabled = prefCategory.CreateEntry("postProcessingEnabled", default_value: false);
            ssaoEnabled.OnValueChanged += toggleSsao;
            postProcessingEnabled.OnValueChanged += togglePp;

            LoggerInstance.Msg($"initialized");
            MelonCoroutines.Start(checkToggles());
        }

        void toggleSsao(bool _, bool enabled)
        {
            foreach (Camera c in Camera.allCameras)
            {
                var ssao = c.GetComponent<AmplifyOcclusionEffect>();
                if (ssao != null && ssao.enabled != enabled)
                {
                    LoggerInstance.Msg($"toggling ssao component {ssao} to {enabled}");
                    ssao.enabled = enabled;
                }
            }
        }

        void togglePp(bool _, bool enabled)
        {
            foreach (Camera c in Camera.allCameras)
            {
                var pp = c.GetComponent<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
                if (pp != null && pp.enabled != enabled)
                {
                    LoggerInstance.Msg($"toggling pp component {pp} to {enabled}");
                    pp.enabled = enabled;
                }
            }
        }

        private IEnumerator checkToggles()
        {
            while (true)
            {
                //LoggerInstance.Msg($"checking toggles ssao {ssaoEnabled} pp {postProcessingEnabled}");
                toggleSsao(false, ssaoEnabled.Value);
                togglePp(false, postProcessingEnabled.Value);
                yield return new WaitForSeconds(5);
            }
        }

        public override void OnUpdate()
        {
            if (UnityEngine.InputSystem.Keyboard.current.f10Key.wasPressedThisFrame)
            {
                LoggerInstance.Msg($"toggling ssao {ssaoEnabled}");
                ssaoEnabled.Value = !ssaoEnabled.Value;
            }
            if (UnityEngine.InputSystem.Keyboard.current.f9Key.wasPressedThisFrame)
            {
                LoggerInstance.Msg($"toggling post processing {postProcessingEnabled}");
                postProcessingEnabled.Value = !postProcessingEnabled.Value;
            }
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);
            LoggerInstance.Msg($"on scene init {buildIndex} {sceneName}");
            toggleSsao(false, ssaoEnabled.Value);
            toggleSsao(false, postProcessingEnabled.Value);
        }

        public override void OnApplicationQuit()
        {
            LoggerInstance.Msg("on application quit");
        }

        public override void OnApplicationLateStart() // Runs after OnApplicationStart.
        {
            LoggerInstance.Msg("OnApplicationLateStart");
        }

        public override void OnSceneWasLoaded(int buildindex, string sceneName) // Runs when a Scene has Loaded and is passed the Scene's Build Index and Name.
        {
            LoggerInstance.Msg("OnSceneWasLoaded: " + buildindex.ToString() + " | " + sceneName);
        }

        public override void OnPreferencesSaved() // Runs when Melon Preferences get saved.
        {
            LoggerInstance.Msg("OnPreferencesSaved");
        }

        public override void OnPreferencesLoaded() // Runs when Melon Preferences get loaded.
        {
            LoggerInstance.Msg("OnPreferencesLoaded");
        }
    }
}
