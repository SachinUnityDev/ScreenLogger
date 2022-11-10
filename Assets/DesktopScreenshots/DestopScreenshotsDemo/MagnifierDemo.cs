using UnityEngine;
using UnityEngine.UI;

namespace DesktopScreenshotsDemo {
	public class MagnifierDemo : MonoBehaviour {

		/// <summary>Constantly updates a close-up of a region around the mouse</summary>
		DesktopScreenshot liveCap;

		/// <summary>Shows a zoomed-out version of a larger region on demand</summary>
		DesktopScreenshot onceCap;

		void Start() {
			liveCap = new DesktopScreenshot(120, 120);
			liveCap.texture.filterMode = FilterMode.Point;
			transform.Find("Magnified").GetComponent<RawImage>().texture = liveCap.texture;
			//
			onceCap = new DesktopScreenshot(480, 480);
			onceCap.Clear();
			transform.Find("Preview").GetComponent<RawImage>().texture = onceCap.texture;
		}

		void Update() {
			var p = DSSDemoTools.mousePosition;
			liveCap.Capture(p.x - liveCap.width / 2, p.y - liveCap.height / 2);
			//
			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				// Capture primary display
				var tex = DesktopScreenshot.Capture(DesktopScreenshot.GetDisplayBounds());
				System.IO.File.WriteAllBytes("screenshot.png", tex.EncodeToPNG());
			}
			if (Input.GetKeyDown(KeyCode.Alpha2)) {
				// Capture entire desktop
				var tex = DesktopScreenshot.Capture(DesktopScreenshot.GetDesktopBounds());
				System.IO.File.WriteAllBytes("screenshot.png", tex.EncodeToPNG());
			}
			if (Input.GetKeyDown(KeyCode.Alpha3)) {
				// Capture a snip around the mouse coordinates
				onceCap.Capture(p.x - onceCap.width / 2, p.y - onceCap.height / 2);
			}
			if (Input.GetKeyDown(KeyCode.Alpha4)) {
				// Log each information about each display
				foreach (var inf in DesktopScreenshot.GetDisplayInfos()) {
					Debug.Log(inf);
				}
			}
		}
	}
}