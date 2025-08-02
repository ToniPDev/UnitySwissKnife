using Editor.NodeHierarchyEditor.NodeSystem.Elements;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.NodeHierarchyEditor.NodeSystem.Windows
{
    public class NhGraphView : GraphView
    {
        public NhGraphView()
        {
            AddManipulators();
            AddGridBg();
            
            AddStyles();
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale,ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            this.AddManipulator(CreateNodeContextualMenu());
        }

        private void AddGridBg()
        {
            var gridBackground = new GridBackground();
            
            gridBackground.StretchToParentSize();
            
            Insert(0, gridBackground);
        }

        private IManipulator CreateNodeContextualMenu()
        {
            var contextualMenuManipulator = new ContextualMenuManipulator(
                menuEvent 
                    => menuEvent.menu.AppendAction("Add Node"
                        ,action => AddElement(CreateNode(action.eventInfo.localMousePosition))));

            return contextualMenuManipulator;
        }

        private NhNode CreateNode(Vector2 position)
        {
            var node = new NhNode();
            
            node.Initialize(position);
            node.Draw();

            return node;
        }

        private void AddStyles()
        {
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Assets/Scripts/EditorDefaultResources/NodeHierarchy/NhGraphStyles.uss");
            
            styleSheets.Add(styleSheet);
        }
    }
}