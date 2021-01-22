//-----------------------------------------------------------------------
// <copyright file="NFCObject.cs">
//     Copyright (C) 2015-2015 lvsheng.huang <https://github.com/ketoo/NFrame>
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFSDK
{
    public class NFObject : NFIObject
    {
        public NFObject(NFGUID self, int nContainerID, int nGroupID, string strClassName, string strConfigIndex)
        {
            mSelf = self;
            mstrClassName = strClassName;
            mstrConfigIndex = strConfigIndex;
            mnContainerID = nContainerID;
            mnGroupID = nGroupID;
            Init();
        }

        ~NFObject()
        {
            Shut();
        }

        public override void Init()
        {
            mRecordManager = new NFRecordManager(mSelf);
            mPropertyManager = new NFPropertyManager(mSelf);

            return;
        }

        public override void Shut()
        {
            NFDataList xRecordList = mRecordManager.GetRecordList();
            if (null != xRecordList)
            {
                for (int i = 0; i < xRecordList.Count(); ++i)
                {
                    string strRecordName = xRecordList.StringVal(i);
                    NFIRecord xRecord = mRecordManager.GetRecord(strRecordName);
                    if (null != xRecord)
                    {
                        xRecord.Clear();
                    }
                }
            }

            mRecordManager = null;
            mPropertyManager = null;

            return;
        }

        ///////////////////////////////////////////////////////////////////////
        public override NFGUID Self()
        {
            return mSelf;
        }

        public override int ContainerID()
        {
            return mnContainerID;
        }

        public override int GroupID()
        {
            return mnGroupID;
        }

        public override string ClassName()
        {
            return mstrClassName;
        }

        public override string ConfigIndex()
        {
            return mstrConfigIndex;
        }

		public override NFIProperty FindProperty(string strPropertyName)
        {
			return mPropertyManager.GetProperty(strPropertyName);
        }

        public override bool SetPropertyInt(string strPropertyName, Int64 nValue)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null == property)
            {
                NFDataList.TData xValue = new NFDataList.TData(NFDataList.VARIANT_TYPE.VTYPE_INT);
                property = mPropertyManager.AddProperty(strPropertyName, xValue);
            }

            property.SetInt(nValue);
            return true;
        }

        public override bool SetPropertyFloat(string strPropertyName, double fValue)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null == property)
            {
                NFDataList.TData xValue = new NFDataList.TData(NFDataList.VARIANT_TYPE.VTYPE_FLOAT);
                property = mPropertyManager.AddProperty(strPropertyName, xValue);
            }

            property.SetFloat(fValue);
            return true;
        }

        public override bool SetPropertyString(string strPropertyName, string strValue)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null == property)
            {
                NFDataList.TData xValue = new NFDataList.TData(NFDataList.VARIANT_TYPE.VTYPE_STRING);
                property = mPropertyManager.AddProperty(strPropertyName, xValue); ;
            }

            property.SetString(strValue);
            return true;
        }

        public override bool SetPropertyObject(string strPropertyName, NFGUID obj)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null == property)
            {
                NFDataList.TData xValue = new NFDataList.TData(NFDataList.VARIANT_TYPE.VTYPE_OBJECT);
                property = mPropertyManager.AddProperty(strPropertyName, xValue);
            }

            property.SetObject(obj);
            return true;

        }

        public override bool SetPropertyVector2(string strPropertyName, NFVector2 obj)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null == property)
            {
                NFDataList.TData xValue = new NFDataList.TData(NFDataList.VARIANT_TYPE.VTYPE_VECTOR2);
                property = mPropertyManager.AddProperty(strPropertyName, xValue);
            }

            property.SetVector2(obj);
            return true;

        }

        public override bool SetPropertyVector3(string strPropertyName, NFVector3 obj)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null == property)
            {
                NFDataList.TData xValue = new NFDataList.TData(NFDataList.VARIANT_TYPE.VTYPE_VECTOR3);
                property = mPropertyManager.AddProperty(strPropertyName, xValue);
            }

            property.SetVector3(obj);
            return true;

        }

        public override Int64 QueryPropertyInt(string strPropertyName)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null != property)
            {
                return property.QueryInt();
            }

            return 0;
        }

        public override double QueryPropertyFloat(string strPropertyName)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null != property)
            {
                return property.QueryFloat();
            }

            return 0.0;
        }

        public override string QueryPropertyString(string strPropertyName)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null != property)
            {
                return property.QueryString();
            }

            return NFDataList.NULL_STRING;
        }

        public override NFGUID QueryPropertyObject(string strPropertyName)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null != property)
            {
                return property.QueryObject();
            }

            return NFDataList.NULL_OBJECT;
        }

        public override NFVector2 QueryPropertyVector2(string strPropertyName)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null != property)
            {
                return property.QueryVector2();
            }

            return NFDataList.NULL_VECTOR2;
        }

        public override NFVector3 QueryPropertyVector3(string strPropertyName)
        {
            NFIProperty property = mPropertyManager.GetProperty(strPropertyName);
            if (null != property)
            {
                return property.QueryVector3();
            }

            return NFDataList.NULL_VECTOR3;
        }

		public override NFIRecord FindRecord(string strRecordName)
        {
            return mRecordManager.GetRecord(strRecordName);
        }

        public override bool SetRecordInt(string strRecordName, int nRow, int nCol, Int64 nValue)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                record.SetInt(nRow, nCol, nValue);
                return true;
            }

            return false;
        }

        public override bool SetRecordFloat(string strRecordName, int nRow, int nCol, double fValue)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                record.SetFloat(nRow, nCol, fValue);
                return true;
            }

            return false;
        }

        public override bool SetRecordString(string strRecordName, int nRow, int nCol, string strValue)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                record.SetString(nRow, nCol, strValue);
                return true;
            }

            return false;
        }

        public override bool SetRecordObject(string strRecordName, int nRow, int nCol, NFGUID obj)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                record.SetObject(nRow, nCol, obj);
                return true;
            }

            return false;
        }

        public override bool SetRecordVector2(string strRecordName, int nRow, int nCol, NFVector2 obj)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                record.SetVector2(nRow, nCol, obj);
                return true;
            }

            return false;
        }

        public override bool SetRecordVector3(string strRecordName, int nRow, int nCol, NFVector3 obj)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                record.SetVector3(nRow, nCol, obj);
                return true;
            }

            return false;
        }

        public override Int64 QueryRecordInt(string strRecordName, int nRow, int nCol)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                return record.QueryInt(nRow, nCol);
            }

            return 0;
        }

        public override double QueryRecordFloat(string strRecordName, int nRow, int nCol)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                return record.QueryFloat(nRow, nCol);
            }

            return 0.0;
        }

        public override string QueryRecordString(string strRecordName, int nRow, int nCol)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                return record.QueryString(nRow, nCol);
            }

            return NFDataList.NULL_STRING;
        }

        public override NFGUID QueryRecordObject(string strRecordName, int nRow, int nCol)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                return record.QueryObject(nRow, nCol);
            }

            return null;
        }

        public override NFVector2 QueryRecordVector2(string strRecordName, int nRow, int nCol)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                return record.QueryVector2(nRow, nCol);
            }

            return null;
        }

        public override NFVector3 QueryRecordVector3(string strRecordName, int nRow, int nCol)
        {
            NFIRecord record = mRecordManager.GetRecord(strRecordName);
            if (null != record)
            {
                return record.QueryVector3(nRow, nCol);
            }

            return null;
        }

        public override NFIRecordManager GetRecordManager()
        {
            return mRecordManager;
        }

        public override NFIPropertyManager GetPropertyManager()
        {
            return mPropertyManager;
        }

        NFGUID mSelf;
        int mnContainerID;
        int mnGroupID;

        string mstrClassName;
        string mstrConfigIndex;

        NFIRecordManager mRecordManager;
        NFIPropertyManager mPropertyManager;
    }
}