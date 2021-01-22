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

#include "NFCreateRoleModule.h"
#include "NFComm/NFMessageDefine/NFProtocolDefine.hpp"
#include "NFServer/NFDBLogicPlugin/NFCommonRedisModule.h"
#include "NFGameServerModule.h"

bool NFCreateRoleModule::Init()
{
	m_pElementModule = pPluginManager->FindModule<NFIElementModule>();
	m_pClassModule = pPluginManager->FindModule<NFIClassModule>();
	m_pNetModule = pPluginManager->FindModule<NFINetModule>();
	m_pKernelModule = pPluginManager->FindModule<NFIKernelModule>();
	m_pGameToDBModule = pPluginManager->FindModule<NFIGameServerToDBModule>();
	m_pSceneProcessModule = pPluginManager->FindModule<NFISceneProcessModule>();
	m_pGameServerNet_ServerModule = pPluginManager->FindModule<NFIGameServerNet_ServerModule>();
	m_pNetClientModule = pPluginManager->FindModule<NFINetClientModule>();
	m_pScheduleModule = pPluginManager->FindModule<NFIScheduleModule>();
	m_pDataTailModule = pPluginManager->FindModule<NFIDataTailModule>();
	m_pSceneModule = pPluginManager->FindModule<NFISceneModule>();
	m_pEventModule = pPluginManager->FindModule<NFIEventModule>();
    m_pGameServerModule = pPluginManager->FindModule<NFIGameServerModule>();

    return true;
}

bool NFCreateRoleModule::AfterInit()
{
	m_pKernelModule->AddClassCallBack(NFrame::Player::ThisName(), this, &NFCreateRoleModule::OnObjectPlayerEvent);

	return true;
}

bool NFCreateRoleModule::ReadyExecute()
{
	m_pNetModule->RemoveReceiveCallBack(NFMsg::REQ_ROLE_LIST);
	m_pNetModule->RemoveReceiveCallBack(NFMsg::REQ_CREATE_ROLE);
	m_pNetModule->RemoveReceiveCallBack(NFMsg::REQ_DELETE_ROLE);
	m_pNetModule->RemoveReceiveCallBack(NFMsg::REQ_ENTER_GAME);
    m_pNetModule->RemoveReceiveCallBack(NFMsg::REQ_SWAP_SCENE);

	m_pNetModule->AddReceiveCallBack(NFMsg::REQ_ROLE_LIST, this, &NFCreateRoleModule::OnRequireRoleListProcess);
	m_pNetModule->AddReceiveCallBack(NFMsg::REQ_CREATE_ROLE, this, &NFCreateRoleModule::OnCreateRoleGameProcess);
	m_pNetModule->AddReceiveCallBack(NFMsg::REQ_DELETE_ROLE, this, &NFCreateRoleModule::OnDeleteRoleGameProcess);
	m_pNetModule->AddReceiveCallBack(NFMsg::REQ_ENTER_GAME, this, &NFCreateRoleModule::OnClientEnterGameProcess);
    m_pNetModule->AddReceiveCallBack(NFMsg::REQ_SWAP_SCENE, this, &NFCreateRoleModule::OnSwapScene);

	m_pNetClientModule->AddReceiveCallBack(NF_SERVER_TYPES::NF_ST_DB, NFMsg::ACK_ROLE_LIST, this, &NFCreateRoleModule::OnResponseRoleListProcess);
	m_pNetClientModule->AddReceiveCallBack(NF_SERVER_TYPES::NF_ST_DB, NFMsg::ACK_LOAD_ROLE_DATA, this, &NFCreateRoleModule::OnDBLoadRoleDataProcess);

	return true;
}


void NFCreateRoleModule::OnSwapScene(const NFSOCK sockIndex, const int msgID, const char* msg, const uint32_t len)
{
    NFGUID clientID;
    NFMsg::ReqAckSwapScene xMsg;
    if (!m_pNetModule->ReceivePB(msgID, msg, len, xMsg, clientID))
    {
        return;
    }

    int sceneId = xMsg.scene_id();
    NFVector3 pos = { 1,1,1 };
    NFDataList args;

    playerPool.emplace_back(clientID);
    if (sceneId == 3 && playerPool.size() == 1) {

        NFMsg::AckPlayerEntryList xPlayerEntryInfoList;
        for (int i = 0; i < playerPool.size(); i++) {
            NFMsg::PlayerEntryInfo* pEntryInfo = xPlayerEntryInfoList.add_object_list();
            *(pEntryInfo->mutable_object_guid()) = NFINetModule::NFToPB(playerPool[i]);
            pEntryInfo->set_x(i);
            pEntryInfo->set_y(0);
            pEntryInfo->set_z(0);
            pEntryInfo->set_player_state(0);
            pEntryInfo->set_config_id(m_pKernelModule->GetPropertyString(playerPool[i], NFrame::Player::ConfigID()));
            pEntryInfo->set_scene_id(m_pKernelModule->GetPropertyInt32(playerPool[i], NFrame::Player::SceneID()));
            pEntryInfo->set_class_id(m_pKernelModule->GetPropertyString(playerPool[i], NFrame::Player::ClassName()));

        }

        if (xPlayerEntryInfoList.object_list_size() <= 0)
        {
            return ;
        }
        int group = m_pKernelModule->RequestGroupScene(sceneId);
        
        for (int i = 0; i < playerPool.size(); i++)
        {
            m_pGameServerNet_ServerModule->SendMsgPBToGate(NFMsg::ACK_OBJECT_ENTRY, xPlayerEntryInfoList, playerPool[i]);
            xMsg.set_x(i + 1);
            pos.SetX(i + 1);
            m_pSceneModule->RequestEnterScene(clientID, sceneId, group, 0, pos, args);
            m_pGameServerNet_ServerModule->SendMsgPBToGate(NFMsg::ACK_SWAP_SCENE, xMsg, playerPool[i]);
            m_pKernelModule->SetPropertyInt(playerPool[i], NFrame::Player::HP(), 100);
            m_pKernelModule->SetPropertyInt(playerPool[i], NFrame::Player::State(), 0);
            m_pKernelModule->SetPropertyInt(playerPool[i], NFrame::Player::GameGold(), 3);
            m_pKernelModule->SetPropertyInt(playerPool[i], NFrame::Player::GameLVL(), 1);
            m_pKernelModule->SetPropertyInt(playerPool[i], NFrame::Player::MaxHero(), 3);
            m_pGameServerModule->refreshShopItem(playerPool[i]);
        }
        return;
    }
    else if (sceneId == 1) {
        m_pSceneModule->RequestEnterScene(clientID, sceneId, 1, 0, pos, args);
        m_pGameServerNet_ServerModule->SendMsgPBToGate(NFMsg::ACK_SWAP_SCENE, xMsg, clientID);
    }
}


void NFCreateRoleModule::OnRequireRoleListProcess(const NFSOCK sockIndex, const int msgID, const char * msg, const uint32_t len)
{
	m_pNetClientModule->SendBySuitWithOutHead(NF_SERVER_TYPES::NF_ST_DB, sockIndex, msgID, std::string(msg, len));
}

void NFCreateRoleModule::OnResponseRoleListProcess(const NFSOCK sockIndex, const int msgID, const char * msg, const uint32_t len)
{
	NFGUID clientID;
	NFMsg::AckRoleLiteInfoList xData;
	if (!m_pNetModule->ReceivePB(msgID, msg, len, xData, clientID))
	{
		return;
	}

	NF_SHARE_PTR<NFIGameServerNet_ServerModule::GateServerInfo> xGateInfo = m_pGameServerNet_ServerModule->GetGateServerInfo(clientID.GetHead());
	if (xGateInfo)
	{
		m_pNetModule->SendMsgWithOutHead (msgID, std::string(msg, len), xGateInfo->xServerData.nFD);
	}
}


void NFCreateRoleModule::OnCreateRoleGameProcess(const NFSOCK sockIndex, const int msgID, const char * msg, const uint32_t len)
{
	m_pNetClientModule->SendBySuitWithOutHead(NF_SERVER_TYPES::NF_ST_DB, sockIndex, msgID, std::string(msg, len));
}

void NFCreateRoleModule::OnDeleteRoleGameProcess(const NFSOCK sockIndex, const int msgID, const char * msg, const uint32_t len)
{
	m_pNetClientModule->SendBySuitWithOutHead(NF_SERVER_TYPES::NF_ST_DB, sockIndex, msgID, std::string(msg, len));
}

void NFCreateRoleModule::OnClientEnterGameProcess(const NFSOCK sockIndex, const int msgID, const char * msg, const uint32_t len)
{
	NFGUID clientID;
	NFMsg::ReqEnterGameServer xMsg;
	if (!m_pNetModule->ReceivePB( msgID, msg, len, xMsg, clientID))
	{
		return;
	}

	NFGUID roleID = NFINetModule::PBToNF(xMsg.id());

	if (m_pKernelModule->GetObject(roleID))
	{
		m_pKernelModule->DestroyObject(roleID);
	}

	//////////////////////////////////////////////////////////////////////////

	NF_SHARE_PTR<NFIGameServerNet_ServerModule::GateBaseInfo>  pGateInfo = m_pGameServerNet_ServerModule->GetPlayerGateInfo(roleID);
	if (nullptr != pGateInfo)
	{
		if (pGateInfo->eStatus == NFIGameServerNet_ServerModule::GateBaseInfo::E_LOADING)
		{
			return;
		}

		m_pGameServerNet_ServerModule->RemovePlayerGateInfo(roleID);
	}

	NF_SHARE_PTR<NFIGameServerNet_ServerModule::GateServerInfo> pGateServerinfo = m_pGameServerNet_ServerModule->GetGateServerInfoBySockIndex(sockIndex);
	if (nullptr == pGateServerinfo)
	{
		return;
	}

	int gateID = -1;
	if (pGateServerinfo->xServerData.pData)
	{
		gateID = pGateServerinfo->xServerData.pData->server_id();
	}

	if (gateID < 0)
	{
		return;
	}

	if (!m_pGameServerNet_ServerModule->AddPlayerGateInfo(roleID, clientID, gateID))
	{
		return;
	}

	m_pNetClientModule->SendBySuitWithOutHead(NF_SERVER_TYPES::NF_ST_DB, sockIndex, NFMsg::REQ_LOAD_ROLE_DATA, std::string(msg, len));

}

void NFCreateRoleModule::OnDBLoadRoleDataProcess(const NFSOCK sockIndex, const int msgID, const char * msg, const uint32_t len)
{
	NFGUID clientID;
	NFMsg::RoleDataPack xMsg;
	if (!m_pNetModule->ReceivePB(msgID, msg, len, xMsg, clientID))
	{
		//releasing all the resource that allow when the user login, then kick off the user
		// TODO

		//m_pGameServerNet_ServerModule->RemovePlayerGateInfo();

		//Avtually, the developer may not know the user id in this situation, therefore the developer must record the login-time when the user coming
		//and check the time per min to kick off the user who are not active.

		return;
	}

	NFGUID roleID = NFINetModule::PBToNF(xMsg.id());
	if (m_pKernelModule->GetObject(roleID))
	{
		//it should be rebind with proxy's netobject
		m_pKernelModule->DestroyObject(roleID);
	}

	mxObjectDataCache[roleID] = xMsg;

	NF_SHARE_PTR<NFIGameServerNet_ServerModule::GateBaseInfo>  pGateInfo = m_pGameServerNet_ServerModule->GetPlayerGateInfo(roleID);
	if (nullptr != pGateInfo)
	{
		if (pGateInfo->eStatus == NFIGameServerNet_ServerModule::GateBaseInfo::E_LOADING)
		{
			pGateInfo->eStatus = NFIGameServerNet_ServerModule::GateBaseInfo::E_LOADED;
		}


		NFDataList var;

		var.AddString(NFrame::Player::GateID());
		var.AddInt(pGateInfo->gateID);

		var.AddString(NFrame::Player::GameID());
		var.AddInt(pPluginManager->GetAppID());

		var.AddString(NFrame::Player::Connection());
		var.AddInt(1);

		/*
		var.AddString(NFrame::Player::HomeSceneID());
		var.AddInt(1);

		var.AddString(NFrame::Player::SceneID());
		var.AddInt(1);
		*/

		NF_SHARE_PTR<NFIObject> pObject = m_pKernelModule->CreateObject(roleID, 1, 0, NFrame::Player::ThisName(), "", var);
		if (nullptr == pObject)
		{
			//mRoleBaseData
			//mRoleFDData
			mxObjectDataCache.erase(roleID);
			return;
		}

		/////////////////////////////
		const int group = m_pKernelModule->GetPropertyInt(pObject->Self(), NFrame::IObject::GroupID());
		if (group <= 0)
		{
			/////////////////////////////
			//sometimes, the player might disconnected from game server and want to reconnect.
			//Basic on this reason, developer could move this kinds of players into the specific scene or group to avoid players move to the default scene.
			//If developers move that kinds of players into the specific scene or group, which means the group value will NOT ZERO!
			//COE_CREATE_FINISH

			/////////////////////////////
	
            m_pSceneProcessModule->RequestEnterScene(pObject->Self(), 1, -1, 0, {0,0,0}, NFDataList::Empty());
		}
	}
}

int NFCreateRoleModule::OnObjectPlayerEvent(const NFGUID & self, const std::string & className, const CLASS_OBJECT_EVENT classEvent, const NFDataList & var)
{
	if (CLASS_OBJECT_EVENT::COE_DESTROY == classEvent)
	{
		//m_pDataTailModule->LogObjectData(self);

		SaveData(self);
	}
	else if (CLASS_OBJECT_EVENT::COE_CREATE_LOADDATA == classEvent)
	{
        //m_pDataTailModule->StartTrail(self);
        //m_pDataTailModule->LogObjectData(self);
        AttachData(self);
		
	}
	else if (CLASS_OBJECT_EVENT::COE_CREATE_FINISH == classEvent)
	{
		auto it = mxObjectDataCache.find(self);
		if (it != mxObjectDataCache.end())
		{
			mxObjectDataCache.erase(it);
		}


		m_pScheduleModule->AddSchedule(self, "SaveDataOnTime", this, &NFCreateRoleModule::SaveDataOnTime, 180.0f, -1);

    }
    else if (CLASS_OBJECT_EVENT::COE_CREATE_HASDATA == classEvent) {
        
    }

	return 0;
}

void NFCreateRoleModule::AttachData(const NFGUID & self)
{
	auto it = mxObjectDataCache.find(self);
	if (it != mxObjectDataCache.end())
	{
		NF_SHARE_PTR<NFIObject> xObject = m_pKernelModule->GetObject(self);
		if (xObject)
		{
			NF_SHARE_PTR<NFIPropertyManager> xPropManager = xObject->GetPropertyManager();
			NF_SHARE_PTR<NFIRecordManager> xRecordManager = xObject->GetRecordManager();

			if (xPropManager)
			{
				NFCommonRedisModule::ConvertPBToPropertyManager(it->second.property(), xPropManager);
			}

			if (xRecordManager)
			{
				NFCommonRedisModule::ConvertPBToRecordManager(it->second.record(), xRecordManager);
			}
		}
	}
    
}

void NFCreateRoleModule::SaveData(const NFGUID & self)
{
	NF_SHARE_PTR<NFIObject> xObject = m_pKernelModule->GetObject(self);
	if (xObject)
	{
		NF_SHARE_PTR<NFIPropertyManager> xPropManager = xObject->GetPropertyManager();
		NF_SHARE_PTR<NFIRecordManager> xRecordManager = xObject->GetRecordManager();
		NFMsg::RoleDataPack xDataPack;

		*xDataPack.mutable_id() = NFINetModule::NFToPB(self);

		*(xDataPack.mutable_property()->mutable_player_id()) = NFINetModule::NFToPB(self);
		*(xDataPack.mutable_record()->mutable_player_id()) = NFINetModule::NFToPB(self);

		if (xPropManager)
		{
			NFCommonRedisModule::ConvertPropertyManagerToPB(xPropManager, xDataPack.mutable_property());
		}

		if (xRecordManager)
		{
			NFCommonRedisModule::ConvertRecordManagerToPB(xRecordManager, xDataPack.mutable_record());
		}

		m_pNetClientModule->SendSuitByPB(NF_SERVER_TYPES::NF_ST_DB, self.GetData(), NFMsg::REQ_SAVE_ROLE_DATA, xDataPack);
	}
}

int NFCreateRoleModule::SaveDataOnTime(const NFGUID & self, const std::string & name, const float fIntervalTime, const int count)
{
	SaveData(self);
	return 0;
}

bool NFCreateRoleModule::Shut()
{
    return true;
}

bool NFCreateRoleModule::Execute()
{
    return true;
}
