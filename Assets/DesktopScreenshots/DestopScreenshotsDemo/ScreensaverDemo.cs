using UnityEngine;
using UnityEngine.UI;

namespace DesktopScreenshotsDemo {
    public class ScreensaverDemo : MonoBehaviour {

        void InitDisplay(GameObject obj, DesktopScreenshot.DisplayInfo inf) {
            var tex = DesktopScreenshot.Capture(inf.screenArea);
            var img = obj.GetComponentInChildren<RawImage>();
            img.texture = tex;
        }
        void Start() {
            var proto = GameObject.Find("ScreenCanvas");
            var infos = DesktopScreenshot.GetDisplayInfos();

            // If we're in standalone mode, our window is covering the desktop
            // so we need to hide it, take a screenshot, and then unhide it
            if (!Application.isEditor) {
                DSSDemoTools.SetWindowVisible(false);
                InitDisplay(proto, infos[0]);
                DSSDemoTools.SetWindowVisible(true);
            } else InitDisplay(proto, infos[0]);

            // Initialize extra displays so that they are covered too.
            // It's kind of hard to tell whether Unity indexes displays in same order,
            // but first two definitely match up.
            for (var i = 1; i < Display.displays.Length; i++) {
                var dsp = Display.displays[i];
                if (i >= infos.Length) break; // should never happen, but just in case
                dsp.Activate();
                //
                var obj = Instantiate(proto, proto.transform.parent);
                obj.GetComponent<Canvas>().targetDisplay = i;
                InitDisplay(obj, infos[i]);
            }
        }
        void Update() {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0)) Application.Quit();
        }
    }
}
