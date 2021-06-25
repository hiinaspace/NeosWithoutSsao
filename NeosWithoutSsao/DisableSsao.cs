using MelonLoader;
using UnityEngine;

namespace NeosWithoutSsao
{
    public class DisableSsao : MelonMod
    {
        float elapsed = 0;

        public override void OnUpdate()
        {
            if ((elapsed += Time.deltaTime) > 5.0f)
            {
                elapsed = 0;
                //MelonLogger.Msg($"scanning for AmplifyOcclusionEffect");
                var ssaoComponents = UnityEngine.Object.FindObjectsOfType<AmplifyOcclusionEffect>();
                foreach (var ssao in ssaoComponents)
                {
                    if (ssao.enabled)
                    {
                        MelonLogger.Msg($"found ssao {ssao} component on {ssao.gameObject.name}, disabling");
                        ssao.enabled = false;
                    }
                }
            }
        }
    }
}
