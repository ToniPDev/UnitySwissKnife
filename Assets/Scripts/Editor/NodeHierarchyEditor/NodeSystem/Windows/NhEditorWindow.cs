using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.NodeHierarchyEditor.NodeSystem.Windows
{
    public class NhEditorWindow : EditorWindow
    {
        [MenuItem("Window/Node Graph/Node Hierarchy")]
        public static void OpenWindow() => GetWindow<NhEditorWindow>("Node Hierarchy Graph");

        private void CreateGUI()
        {
            AddGraphView();

            AddStyles();
        }

        private void AddGraphView()
        {
            NhGraphView graphView = new NhGraphView();
            
            graphView.StretchToParentSize();
            
            rootVisualElement.Add(graphView);
        }

        private void AddStyles()
        {
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Assets/Scripts/EditorDefaultResources/NodeHierarchy/NhGraphStyles.uss");
            
            rootVisualElement.styleSheets.Add(styleSheet);
        }
    }
}