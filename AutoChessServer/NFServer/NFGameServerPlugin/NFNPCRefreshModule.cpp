/*
            This file is part of: 
                NoahFrame
            https://github.com/ketoo/NoahGameFrame

   Copyright 2009 - 2020 NoahFrame(NoahGameFrame)

   File creator: lvsheng.huang
   
   NoahFrame is open-source software and you can redistribute it and/or modify
   it under the terms of the License; besides, anyone who use this file/software must include this copyright announcement.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/


#include <NFComm/NFPluginModule/NFIPropertyModule.h>
#include "NFComm/NFMessageDefine/NFMsgDefine.h"
#include "NFNPCRefreshModule.h"

bool NFNPCRefreshModule::Init()
{
    return true;
}


bool NFNPCRefreshModule::Shut()
{
    return true;
}

bool NFNPCRefreshModule::Execute()
{
    return true;
}

bool NFNPCRefreshModule::AfterInit()
{
	m_pScheduleModule = pPluginManager->FindModule<NFIScheduleModule>();
	m_pEventModule = pPluginManager->FindModule<NFIEventModule>();
    m_pKernelModule = pPluginManager->FindModule<NFIKernelModule>();
    m_pSceneProcessModule = pPluginManager->FindModule<NFISceneProcessModule>();
    m_pElementModule = pPluginManager->FindModule<NFIElementModule>();
	m_pLogModule = pPluginManager->FindModule<NFILogModule>();
	m_pPropertyModule = pPluginManager->FindModule<NFIPropertyModule>();
	m_pSceneModule = pPluginManager->FindModule<NFISceneModule>();
	
	m_pKernelModule->AddClassCallBack(NFrame::NPC::ThisName(), this, &NFNPCRefreshModule::OnObjectClassEvent);

    return true;
}

int NFNPCRefreshModule::OnObjectClassEvent( const NFGUID& self, const std::string& className, const CLASS_OBJECT_EVENT classEvent, const NFDataList& var )
{
    NF_SHARE_PTR<NFIObject> pSelf = m_pKernelModule->GetObject(self);
    if (nullptr == pSelf)
    {
        return 1;
    }

    if (className == NFrame::NPC::ThisName())
    {
        if ( CLASS_OBJECT_EVENT::COE_CREATE_LOADDATA == classEvent )
        {
            const std::string& configIndex = m_pKernelModule->GetPropertyString(self, NFrame::NPC::ConfigID());
			const std::string& strEffectPropertyID = m_pElementModule->GetPropertyString(configIndex, NFrame::NPC::EffectData());
			//const int npcType = m_pElementModule->GetPropertyInt32(configIndex, NFrame::NPC::NPCType());
			NF_SHARE_PTR<NFIPropertyManager> pSelfPropertyManager = pSelf->GetPropertyManager();

			//effect data
			//normal npc
			NF_SHARE_PTR<NFIPropertyManager> pConfigPropertyManager = m_pElementModule->GetPropertyManager(strEffectPropertyID);
			if (pConfigPropertyManager)
			{
				std::string strProperName;
				for (NFIProperty* property = pConfigPropertyManager->FirstNude(strProperName); property != NULL; property = pConfigPropertyManager->NextNude(strProperName))
				{
					if (pSelfPropertyManager && property->Changed()
						&& strProperName != NFrame::IObject::ID()
						&& strProperName != NFrame::IObject::ConfigID()
						&& strProperName != NFrame::IObject::ClassName()
						&& strProperName != NFrame::IObject::SceneID()
						&& strProperName != NFrame::IObject::GroupID())
					{
						pSelfPropertyManager->SetProperty(property->GetKey(), property->GetValue());
					}
				}
			}

			//if (npcType == NFMsg::ENPCType::HERO_NPC)
			//{
			//	//star & level
			//}
        }
        else if ( CLASS_OBJECT_EVENT::COE_CREATE_HASDATA == classEvent )
        {
			int nHPMax = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::MAXHP());
            m_pKernelModule->SetPropertyInt(self, NFrame::NPC::HP(), nHPMax);

            m_pKernelModule->AddPropertyCallBack( self, NFrame::NPC::HP(), this, &NFNPCRefreshModule::OnObjectHPEvent );
            m_pKernelModule->AddPropertyCallBack(self, NFrame::NPC::Target(), this, NFNPCRefreshModule::OnTagetChangeEvent);
        }
    }

    return 0;
}

int NFNPCRefreshModule::OnObjectHPEvent( const NFGUID& self, const std::string& propertyName, const NFData& oldVar, const NFData& newVar)
{
    if (newVar.GetInt() == 0) {
        m_pKernelModule->SetPropertyInt(self, NFrame::NPC::State(), 0);
    }
    return 0;
}

int NFNPCRefreshModule::OnTagetChangeEvent(const NFGUID& self, const std::string& propertyName, const NFData& oldVar, const NFData& newVar)
{
    string name("attack");
    string oldName =name + oldVar.ToString();
    string newName = name + newVar.ToString();
    m_pScheduleModule->RemoveSchedule(self, oldName);
    float attackSpeed = m_pKernelModule->GetPropertyFloat(self, NFrame::NPC::ATK_SPEED());
    m_pScheduleModule->AddSchedule(self, newName, this, &NFNPCRefreshModule::OnNPCAttack, 1/attackSpeed, 1000);
    return 0;
}

int NFNPCRefreshModule::OnAttackSpeedChangeEvent(const NFGUID& self, const std::string& propertyName, const NFData& oldVar, const NFData& newVar)
{
    string name("attack");
    NFGUID target = m_pKernelModule->GetPropertyObject(self, NFrame::NPC::Target());
    name += target.ToString();
    m_pScheduleModule->RemoveSchedule(self, name);
    float attackSpeed = newVar.GetInt();
    m_pScheduleModule->AddSchedule(self, name,this, &NFNPCRefreshModule::OnNPCAttack, 1 / attackSpeed, 1000);
    return 0;
}

int NFNPCRefreshModule::OnNPCAttack(const NFGUID& self, const std::string& heartBeat, const float time, const int count)
{
    int damage = m_pKernelModule->GetPropertyInt(self, NFrame::NPC::ATK_VALUE());
    NFGUID target = m_pKernelModule->GetPropertyObject(self, NFrame::NPC::Target());
    int targetHP = m_pKernelModule->GetPropertyInt(self, NFrame::NPC::HP());

    targetHP -= damage;
    if (targetHP < 0) targetHP = 0;
    m_pKernelModule->SetPropertyInt(target, NFrame::NPC::HP(),targetHP);
    return 0;
}

int NFNPCRefreshModule::OnNPCDeadDestroyHeart( const NFGUID& self, const std::string& heartBeat, const float time, const int count)
{
    //and create new object
	int sceneID = m_pKernelModule->GetPropertyInt32( self, NFrame::NPC::SceneID());
	int groupID = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::GroupID());
	/*int npcType = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::NPCType());

	if (npcType == NFMsg::ENPCType::NORMAL_NPC)
	{

		const std::string& className = m_pKernelModule->GetPropertyString( self, NFrame::NPC::ClassName());
		const std::string& seedID = m_pKernelModule->GetPropertyString( self, NFrame::NPC::SeedID());
		const std::string& configID = m_pKernelModule->GetPropertyString( self, NFrame::NPC::ConfigID());
		const NFGUID masterID = m_pKernelModule->GetPropertyObject(self, NFrame::NPC::MasterID());
		const NFGUID camp = m_pKernelModule->GetPropertyObject(self, NFrame::NPC::CampID());
		int refresh = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::Refresh());

		m_pKernelModule->DestroySelf(self);

		const NFVector3& seedPos = m_pSceneModule->GetSeedPos(sceneID, seedID);
		if (refresh > 0)
		{
			NFDataList arg;
			arg << NFrame::NPC::Position() << seedPos;
			arg << NFrame::NPC::SeedID() << seedID;
			arg << NFrame::NPC::MasterID() << masterID;
			arg << NFrame::NPC::CampID() << camp;
			arg << NFrame::NPC::Refresh() << refresh;

			m_pKernelModule->CreateObject(NFGUID(), sceneID, groupID, className, configID, arg);
		}
	}
	else
	{
		m_pKernelModule->DestroySelf(self);
	}*/

    return 0;
}

int NFNPCRefreshModule::OnBuildingDeadDestroyHeart(const NFGUID & self, const std::string & heartBeat, const float time, const int count)
{
	//and create new object
	/*const std::string& className = m_pKernelModule->GetPropertyString(self, NFrame::NPC::ClassName());
	const std::string& seedID = m_pKernelModule->GetPropertyString(self, NFrame::NPC::SeedID());
	const std::string& configID = m_pKernelModule->GetPropertyString(self, NFrame::NPC::ConfigID());
	const NFGUID masterID = m_pKernelModule->GetPropertyObject(self, NFrame::NPC::MasterID());
	int npcType = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::NPCType());
	int sceneID = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::SceneID());
	int groupID = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::GroupID());

	NFVector3 fSeedPos = m_pKernelModule->GetPropertyVector3(self, NFrame::NPC::Position());

	if (npcType == NFMsg::ENPCType::TURRET_NPC)
	{
		m_pKernelModule->DestroySelf(self);

		NFDataList arg;
		arg << NFrame::NPC::Position() << fSeedPos;
		arg << NFrame::NPC::SeedID() << seedID;
		arg << NFrame::NPC::MasterID() << masterID;

		m_pKernelModule->CreateObject(NFGUID(), sceneID, groupID, className, configID, arg);
	}*/
	
	return 0;
}

int NFNPCRefreshModule::OnObjectBeKilled( const NFGUID& self, const NFGUID& killer )
{
	/*if ( m_pKernelModule->GetObject(killer) )
	{
		const int64_t exp = m_pKernelModule->GetPropertyInt(self, NFrame::NPC::EXP());
		const int64_t gold = m_pKernelModule->GetPropertyInt(self, NFrame::NPC::Gold());

		m_pPropertyModule->AddExp(killer, exp);
		m_pPropertyModule->AddGold(killer, gold);
	}*/

	return 0;
}


bool NFNPCRefreshModule::AddHP(const NFGUID& self, const int nValue)
{
    if (nValue <= 0)
    {
        return false;
    }

    int nCurValue = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::HP());
    int nMaxValue = m_pKernelModule->GetPropertyInt32(self, NFrame::NPC::MAXHP());

    if (nCurValue > 0)
    {
        nCurValue += nValue;
        if (nCurValue > nMaxValue)
        {
            nCurValue = nMaxValue;
        }

        m_pKernelModule->SetPropertyInt(self, NFrame::NPC::HP(), nCurValue);
    }
    return true;
}

bool NFNPCRefreshModule::EnoughHP(const NFGUID& self, const int nValue)
{
    NFINT64 nCurValue = m_pKernelModule->GetPropertyInt(self, NFrame::Player::HP());
    if ((nCurValue > 0) && (nCurValue - nValue >= 0))
    {
        return true;
    }
    return false;
}

bool NFNPCRefreshModule::DamageHP(const NFGUID& self, const int nValue)
{
    int nCurValue = m_pKernelModule->GetPropertyInt32(self, NFrame::Player::HP());
    if (nCurValue > 0)
    {
        nCurValue -= nValue;
        nCurValue = (nCurValue >= 0) ? nCurValue : 0;

        m_pKernelModule->SetPropertyInt(self, NFrame::Player::HP(), nCurValue);

        return true;
    }

    return false;
}

bool NFNPCRefreshModule::ConsumeHP(const NFGUID& self, const int nValue)
{
    int nCurValue = m_pKernelModule->GetPropertyInt32(self, NFrame::Player::HP());
    if ((nCurValue > 0) && (nCurValue - nValue >= 0))
    {
        nCurValue -= nValue;
        m_pKernelModule->SetPropertyInt(self, NFrame::Player::HP(), nCurValue);

        return true;
    }
    return false;
}