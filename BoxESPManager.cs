using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace unreliablecode
{
    public class BoxESPManager : MonoBehaviour
    {
        public GameObject espCanvas; // The canvas where ESP boxes will be drawn
        public Camera mainCamera; // Reference to the main camera
        private Dictionary<GameObject, ESPBox> espBoxDictionary = new Dictionary<GameObject, ESPBox>();
        private bool isEspEnabled = true; // Toggle for enabling/disabling ESP boxes

        private void CreateEspBoxForObject(GameObject targetObject)
        {
            ESPBox espBox = new ESPBox(targetObject, espCanvas.transform);
            espBoxDictionary[targetObject] = espBox;
        }

        private void UpdateEspBoxes()
        {
            if (!isEspEnabled || espBoxDictionary == null) return;

            foreach (var kvp in espBoxDictionary)
            {
                GameObject targetObject = kvp.Key;
                ESPBox espBox = kvp.Value;

                // Check if the target object is null
                if (targetObject == null)
                {
                    espBox.Hide();
                    continue;
                }

                espBox.UpdatePosition(mainCamera);
            }
        }

        private IEnumerator UpdateEspBoxesCoroutine()
        {
            while (isEspEnabled)
            {
                UpdateEspBoxes();
                yield return null; // Wait for the next frame
            }
        }

        private void OnEnable()
        {
            StartCoroutine(UpdateEspBoxesCoroutine());
        }

        private void OnDisable()
        {
            StopCoroutine(UpdateEspBoxesCoroutine());
        }
    }

    public class ESPBox
    {
        private GameObject targetObject; // The object this ESP box is tracking
        private GameObject boxGameObject; // The visual representation of the ESP box
        private Image boxImage; // The image component of the ESP box

        public ESPBox(GameObject target, Transform parent)
        {
            targetObject = target;
            boxGameObject = new GameObject($"ESPBox_{target.name}");
            boxGameObject.transform.SetParent(parent, false);

            // Create an Image component for the ESP box
            boxImage = boxGameObject.AddComponent<Image>();
            boxImage.color = new Color(1f, 0f, 0f, 0.5f); // Set the color and transparency of the box

            RectTransform rectTransform = boxGameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(100f, 100f); // Default size
        }

        public void UpdatePosition(Camera mainCamera)
        {
            RectTransform rectTransform = boxGameObject.GetComponent<RectTransform>();

            // Get the screen position of the target object
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetObject.transform.position);

            // Check if the target object is in front of the camera
            if (screenPosition.z > 0f)
            {
                // Set the position of the ESP box
                rectTransform.position = screenPosition;

                // Calculate the size of the ESP box based on the target object's dimensions
                Vector3 objectSize = targetObject.GetComponent<Renderer>().bounds.size;
                rectTransform.sizeDelta = new Vector2(objectSize.x * 100f, objectSize.y * 100f); // Scale size for visibility
                boxGameObject.SetActive(true);
            }
            else
            {
                boxGameObject.SetActive(false); // Hide ESP box if behind the camera
            }
        }

        public void Hide()
        {
            boxGameObject.SetActive(false); // Hide the ESP box
        }
    }
}
