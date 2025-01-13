using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

namespace TMPro.Examples
{
    public class TMP_TextSelector_B : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
    {
        public RectTransform TextPopup_Prefab_01;

        private RectTransform m_TextPopup_RectTransform;
        private TextMeshProUGUI m_TextPopup_TMPComponent;

        private TextMeshProUGUI m_TextMeshPro;
        private Canvas m_Canvas;
        private Camera m_Camera;

        private bool isHoveringObject;
        private int m_lastIndex = -1;

        private TMP_MeshInfo[] m_cachedMeshInfoVertexData;

        void Awake()
        {
            m_TextMeshPro = GetComponent<TextMeshProUGUI>();
            m_Canvas = GetComponentInParent<Canvas>();
            m_Camera = m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : m_Canvas.worldCamera;

            m_TextPopup_RectTransform = Instantiate(TextPopup_Prefab_01);
            m_TextPopup_RectTransform.SetParent(m_Canvas.transform, false);
            m_TextPopup_TMPComponent = m_TextPopup_RectTransform.GetComponentInChildren<TextMeshProUGUI>();
            m_TextPopup_RectTransform.gameObject.SetActive(false);
        }

        void OnEnable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        }

        void OnDisable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        }

        void ON_TEXT_CHANGED(Object obj)
        {
            if (obj == m_TextMeshPro)
            {
                m_cachedMeshInfoVertexData = m_TextMeshPro.textInfo.CopyMeshInfoVertexData();
            }
        }

        void LateUpdate()
        {
            if (isHoveringObject)
            {
                int charIndex = TMP_TextUtilities.FindIntersectingCharacter(m_TextMeshPro, Input.mousePosition, m_Camera, true);

                if (charIndex == -1 || charIndex != m_lastIndex)
                {
                    RestoreCachedVertexAttributes(m_lastIndex);
                    m_lastIndex = -1;
                }

                if (charIndex != -1 && charIndex != m_lastIndex)
                {
                    m_lastIndex = charIndex;
                    int materialIndex = m_TextMeshPro.textInfo.characterInfo[charIndex].materialReferenceIndex;
                    int vertexIndex = m_TextMeshPro.textInfo.characterInfo[charIndex].vertexIndex;

                    Vector3[] vertices = m_TextMeshPro.textInfo.meshInfo[materialIndex].vertices;

                    Vector2 charMidBasline = (vertices[vertexIndex + 0] + vertices[vertexIndex + 2]) / 2;
                    Vector3 offset = charMidBasline;

                    vertices[vertexIndex + 0] -= offset;
                    vertices[vertexIndex + 1] -= offset;
                    vertices[vertexIndex + 2] -= offset;
                    vertices[vertexIndex + 3] -= offset;

                    float zoomFactor = 1.5f;
                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * zoomFactor);

                    vertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 0]);
                    vertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 1]);
                    vertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 2]);
                    vertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(vertices[vertexIndex + 3]);

                    vertices[vertexIndex + 0] += offset;
                    vertices[vertexIndex + 1] += offset;
                    vertices[vertexIndex + 2] += offset;
                    vertices[vertexIndex + 3] += offset;

                    Color32[] vertexColors = m_TextMeshPro.textInfo.meshInfo[materialIndex].colors32;
                    Color32 c = new Color32(255, 255, 192, 255);

                    vertexColors[vertexIndex + 0] = c;
                    vertexColors[vertexIndex + 1] = c;
                    vertexColors[vertexIndex + 2] = c;
                    vertexColors[vertexIndex + 3] = c;

                    m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHoveringObject = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHoveringObject = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Pointer clicked.");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Pointer released.");
        }

        void RestoreCachedVertexAttributes(int index)
        {
            if (index == -1 || index > m_TextMeshPro.textInfo.characterCount - 1) return;

            int materialIndex = m_TextMeshPro.textInfo.characterInfo[index].materialReferenceIndex;
            int vertexIndex = m_TextMeshPro.textInfo.characterInfo[index].vertexIndex;

            Vector3[] srcVertices = m_cachedMeshInfoVertexData[materialIndex].vertices;
            Vector3[] dstVertices = m_TextMeshPro.textInfo.meshInfo[materialIndex].vertices;

            dstVertices[vertexIndex + 0] = srcVertices[vertexIndex + 0];
            dstVertices[vertexIndex + 1] = srcVertices[vertexIndex + 1];
            dstVertices[vertexIndex + 2] = srcVertices[vertexIndex + 2];
            dstVertices[vertexIndex + 3] = srcVertices[vertexIndex + 3];

            m_TextMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
        }
    }
}
