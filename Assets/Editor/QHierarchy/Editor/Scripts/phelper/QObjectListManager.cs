using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using qtools.qhierarchy.pdata;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
#endif

namespace qtools.qhierarchy.phelper
{
    public class QObjectListManager
    {
        // CONST
        private const string QObjectListName = "QHierarchyObjectList";

        // SINGLETON
        private static QObjectListManager instance;
        public static QObjectListManager getInstance()
        {
            if (instance == null) instance = new QObjectListManager();
            return instance;
        }

        // PRIVATE
        private bool showObjectList;
        private bool preventSelectionOfLockedObjects;
        private bool preventSelectionOfLockedObjectsDuringPlayMode;
        private GameObject lastSelectionGameObject = null;
        private int lastSelectionCount = 0;

        // CONSTRUCTOR
        private QObjectListManager()
        {
           
            QSettings.getInstance().addEventListener(QSetting.PreventSelectionOfLockedObjects, settingsChanged);
            QSettings.getInstance().addEventListener(QSetting.ShowLockComponent              , settingsChanged);
            QSettings.getInstance().addEventListener(QSetting.ShowLockComponentDuringPlayMode, settingsChanged);
            settingsChanged();
        }

        private void settingsChanged()
        {
          
            preventSelectionOfLockedObjects = QSettings.getInstance().get<bool>(QSetting.ShowLockComponent) && QSettings.getInstance().get<bool>(QSetting.PreventSelectionOfLockedObjects);
            preventSelectionOfLockedObjectsDuringPlayMode = preventSelectionOfLockedObjects && QSettings.getInstance().get<bool>(QSetting.ShowLockComponentDuringPlayMode);
        }

        private bool isSelectionChanged()
        {
            if (lastSelectionGameObject != Selection.activeGameObject || lastSelectionCount  != Selection.gameObjects.Length)
            {
                lastSelectionGameObject = Selection.activeGameObject;
                lastSelectionCount = Selection.gameObjects.Length;
                return true;
            }
            return false;
        }

        public void validate()
        {
         
        }

        #if UNITY_5_3_OR_NEWER
      
        private Scene lastActiveScene;
        private int lastSceneCount = 0;

        public void update()
        {
         
        }


        public bool isSceneChanged()
        {
            if (lastActiveScene != EditorSceneManager.GetActiveScene() || lastSceneCount != EditorSceneManager.loadedSceneCount)
                return true;
            else 
                return false;
        }

        #else

        private HashSet<QObjectList> objectListHashSet;

        public void update()
        {
            try
            {  
                List<QObjectList> objectListList = QObjectList.instances;
                int objectListCount = objectListList.Count;
                if (objectListCount > 0) 
                {
                    if (objectListCount > 1)
                    {
                        for (int i = objectListCount - 1; i > 0; i--)
                        {
                            objectListList[0].merge(objectListList[i]);
                            GameObject.DestroyImmediate(objectListList[i].gameObject);
                        }
                    }

                     = QObjectList.instances[0];
                    setupObjectList(objectList);

                    if (( showObjectList && ((objectList.gameObject.hideFlags & HideFlags.HideInHierarchy)  > 0)) ||
                        (!showObjectList && ((objectList.gameObject.hideFlags & HideFlags.HideInHierarchy) == 0)))
                    {
                        objectList.gameObject.hideFlags ^= HideFlags.HideInHierarchy; 
                        EditorApplication.DirtyHierarchyWindowSorting();
                    }

                    if ((!Application.isPlaying && preventSelectionOfLockedObjects) || 
                        ((Application.isPlaying && preventSelectionOfLockedObjectsDuringPlayMode))
                        && isSelectionChanged())
                    {
                        GameObject[] selections = Selection.gameObjects;
                        List<GameObject> actual = new List<GameObject>(selections.Length);
                        bool found = false;
                        for (int i = selections.Length - 1; i >= 0; i--)
                        {
                            GameObject gameObject = selections[i];
                            
                            bool isLock = objectList.lockedObjects.Contains(gameObject);                        
                            if (!isLock) actual.Add(selections[i]);
                            else found = true;
                        }
                        if (found) Selection.objects = actual.ToArray();
                    }   
                }
            }
            catch 
            {
            }
        }

        public QObjectList getObjectList(GameObject gameObject, bool createIfNotExists = false)
        { 
            List<QObjectList> objectListList = QObjectList.instances;
            int objectListCount = objectListList.Count;
            if (objectListCount != 1)
            {
                if (objectListCount == 0) 
                {
                    if (createIfNotExists)
                    {
                        createObjectList(gameObject);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
                
            return QObjectList.instances[0];
        }

        #endif

    

      
    }
}

