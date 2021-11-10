using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

namespace NFSDK
{
    public class NFUIModule : NFIModule
    {
        private Dictionary<string, GameObject> mAllUIs = new Dictionary<string, GameObject>();
        private Queue<NFUIDialog> mDialogs = new Queue<NFUIDialog>();
        private NFUIDialog mCurrentDialog = null;

        public override void Awake() 
        { 
            
        }
        public override void AfterInit() { }
        public override void Execute() { }
        public override void BeforeShut() { }
        public override void Shut() { }

        public NFUIModule(NFIPluginManager pluginManager)
        {
            mPluginManager = pluginManager;
        }

        public override void Init()
        {
        }

        public T ShowUI<T>(bool closeLastOne = true, bool pushHistory = true,NFDataList valueList = null) where T : NFUIDialog
        {
            string name = typeof(T).ToString();
            GameObject uiObject;
            if (!mAllUIs.TryGetValue(name, out uiObject))
            {
                GameObject perfb = Resources.Load<GameObject>("Prefabs/UI/" + name);
                uiObject = GameObject.Instantiate(perfb);
                uiObject.name = name;
                uiObject.transform.SetParent(GameMain.Instance().transform);
                mAllUIs.Add(name, uiObject);
                T panel = uiObject.GetComponent<T>();
                panel.Init();
            }
            else uiObject.SetActive(true);

            if (uiObject)
            {
                T panel = uiObject.GetComponent<T>();
                if (valueList != null) panel.mUserData = valueList;
                mCurrentDialog = panel;
                uiObject.SetActive(true);
                if (pushHistory) mDialogs.Enqueue(panel);
                return panel;
            }

            return null;
        }
        public bool isActive<T>() where T : NFUIDialog
        {
            string name = typeof(T).ToString();
            GameObject uiObject;
            if (mAllUIs.TryGetValue(name, out uiObject)) return uiObject.activeSelf;

            return false;
        }
        public T GetUI<T>() where T: NFUIDialog
        {
            string name = typeof(T).ToString();
            GameObject uiObject;
            if (mAllUIs.TryGetValue(name, out uiObject)) return uiObject.GetComponent<T>();

            return null;
        }
        public void HidenUI<T>() where T : NFUIDialog
        {
            string name = typeof(T).ToString();
            GameObject uiObject;
            if (mAllUIs.TryGetValue(name, out uiObject)) uiObject.SetActive(false);
        }

        public void CloseUI<T>() where T : NFUIDialog
        {
            if (mCurrentDialog)
            {
                mCurrentDialog.gameObject.SetActive(false);
                mCurrentDialog = null;
            }
        }

        public void CloseAllUI()
        {
            if (mCurrentDialog)
            {
                mCurrentDialog.gameObject.SetActive(false);
                mCurrentDialog = null;
            }
            foreach(var item in mAllUIs.ToList())
            {
                item.Value.SetActive(false);
            }
            mDialogs.Clear();
        }


        public void DestroyAllUI()
        {
            foreach(var item in mAllUIs.ToList())
            {
                GameObject.DestroyImmediate(item.Value);
            }
            mAllUIs.Clear();
            mDialogs.Clear();
        }
    }
}