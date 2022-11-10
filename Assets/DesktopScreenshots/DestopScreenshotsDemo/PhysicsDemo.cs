using UnityEngine;
namespace DesktopScreenshotsDemo {
    public class PhysicsDemo : MonoBehaviour {
        const float scale = 0.01f;

        Texture2D screencap;

        PhysicsMaterial2D bouncyMaterial;

        Rigidbody2D dragbody;

        void addBox(Rect rect, bool enablePhysics = false) {
            var obj = new GameObject();
            obj.name = "Slice:" + rect;
            var pos = new Vector3(rect.xMin, rect.yMin, 0);
            obj.transform.position = pos * scale;
            obj.transform.parent = transform.parent;

            // slice the screenshot:
            var sprite = Sprite.Create(screencap, rect, new Vector2(0, 0));
            sprite.name = "SliceSprite:" + rect;
            var renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = sprite;

            // give it a collider:
            obj.AddComponent<BoxCollider2D>();
            if (enablePhysics) {
                var col = obj.AddComponent<Rigidbody2D>();
                col.angularDrag = 0.5f;
                col.AddTorque(10f);
                col.AddForce(Random.insideUnitCircle * 10f);
                col.sharedMaterial = bouncyMaterial;
                col.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
        }
        void Start() {
            bouncyMaterial = new PhysicsMaterial2D("BouncyMaterial");
            bouncyMaterial.bounciness = 0.9f;
            bouncyMaterial.friction = 0.1f;

            // get a screencap of desktop behind the window:
            DSSDemoTools.SetWindowVisible(false);
            screencap = DesktopScreenshot.Capture(DesktopScreenshot.GetDisplayBounds());
            DSSDemoTools.SetWindowVisible(true);

            // prepare camera:
            float w = screencap.width;
            float h = screencap.height;
            var cam = GetComponent<Camera>();
            cam.orthographicSize = h / 2 * scale;
            cam.transform.position = new Vector3(w / 2 * scale, h / 2 * scale, cam.transform.position.z);
            
            // add screen borders:
            var pad = 40f;
            addBox(new Rect(0f, 0f, pad, h));
            addBox(new Rect(w - pad, 0f, pad, h));
            addBox(new Rect(pad, 0f, w - pad * 2, pad));
            addBox(new Rect(pad, h - pad, w - pad * 2, pad));

            // add dynamic boxes:
            var boxAreaSize = Mathf.Min(w - pad * 2, h - pad * 2);
            var boxCount = 10;
            var boxSize = boxAreaSize / boxCount;
            for (var cx = 0; cx < boxCount; cx++) {
                for (var cy = 0; cy < boxCount; cy++) {
                    var x = pad + (w - boxAreaSize) / 2 + boxSize * (cx + 0.1f);
                    var y = pad + (h - boxAreaSize) / 2 + boxSize * (cy + 0.1f);
                    addBox(new Rect(x, y, boxSize * 0.8f, boxSize * 0.8f), true);
                }
            }
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                Vector2 pos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                var col = Physics2D.OverlapPoint(pos);
                if (col != null) {
                    dragbody = col.attachedRigidbody;
                    dragbody.GetComponent<SpriteRenderer>().color = Color.red;
                } else dragbody = null;
            }
            if (Input.GetMouseButtonUp(0)) {
                if (dragbody != null) {
                    dragbody.GetComponent<SpriteRenderer>().color = Color.white;
                    dragbody = null;
                }
            }
        }

        void FixedUpdate() {
            if (dragbody != null) {
                Vector2 pos = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                var force = pos - dragbody.position;
                dragbody.AddForce(force * 500f * Time.fixedDeltaTime);
            }
        }
    }
}