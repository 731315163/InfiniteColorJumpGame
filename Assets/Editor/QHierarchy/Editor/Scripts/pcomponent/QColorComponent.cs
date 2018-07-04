using UnityEngine;
using UnityEditor;
using qtools.qhierarchy.pcomponent.pbase;
using qtools.qhierarchy.pdata;
using qtools.qhierarchy.phelper;

namespace qtools.qhierarchy.pcomponent
{
    public class QColorComponent: QBaseComponent
    {
        // PRIVATE
        private Texture2D colorTexture;
        private Color backgroundColor;

        // CONSTRUCTOR
        public QColorComponent()
        {
            colorTexture = QResources.getInstance().getTexture(QTexture.QColorButton);
            backgroundColor = QResources.getInstance().getColor(QColor.Background);

            QSettings.getInstance().addEventListener(QSetting.ShowColorComponent              , settingsChanged);
            QSettings.getInstance().addEventListener(QSetting.ShowColorComponentDuringPlayMode, settingsChanged);
            settingsChanged();
        }

        // PRIVATE
        private void settingsChanged()
        {
            enabled = QSettings.getInstance().get<bool>(QSetting.ShowColorComponent);
            showComponentDuringPlayMode = QSettings.getInstance().get<bool>(QSetting.ShowColorComponentDuringPlayMode);
        }

        // LAYOUT
        public override void layout(GameObject gameObject,  ref Rect rect)
        {
            rect.x -= 9;
            rect.width = 9;
        }

        // DRAW
        public override void draw(GameObject gameObject,  Rect selectionRect, Rect curRect)
        {
            EditorGUI.DrawRect(curRect, backgroundColor);
            GUI.DrawTexture(curRect, colorTexture, ScaleMode.StretchToFill, true, 1);

            
        }

        // EVENTS
        public override void eventHandler(GameObject gameObject,  Event currentEvent, Rect curRect)
        {
            if (currentEvent.isMouse && currentEvent.type == EventType.MouseDown && currentEvent.button == 0 && curRect.Contains(currentEvent.mousePosition))
            {
                if (currentEvent.type == EventType.MouseDown)
                {
                    Color color = QResources.getInstance().getColor(QColor.Background);
                    color.a = 0.1f;

               

                    try 
                    {
                        PopupWindow.Show(curRect, new QColorPickerWindow(Selection.Contains(gameObject) ? Selection.gameObjects : new GameObject[] { gameObject }, colorSelectedHandler, colorRemovedHandler));
                    } 
                    catch 
                    {}
                }
                currentEvent.Use();
            }
        }

        // PRIVATE
        private void colorSelectedHandler(GameObject[] gameObjects, Color color)
        {
            for (int i = gameObjects.Length - 1; i >= 0; i--)
            {
                GameObject gameObject = gameObjects[i];
            }
            EditorApplication.RepaintHierarchyWindow();
        }

        private void colorRemovedHandler(GameObject[] gameObjects)
        {
            for (int i = gameObjects.Length - 1; i >= 0; i--)
            {
                GameObject gameObject = gameObjects[i];
            }
            EditorApplication.RepaintHierarchyWindow();
        }
    }
}

