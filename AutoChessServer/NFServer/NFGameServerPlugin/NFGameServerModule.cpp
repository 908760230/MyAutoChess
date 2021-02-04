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


#include "NFGameServerPlugin.h"
#include "NFGameServerModule.h"
#include "NFComm/NFKernelPlugin/NFSceneModule.h"

vector<string> raceType = { "Saber","Archer","Wizard" };
vector<string> elementType = { "Aurum", "Wood", "Water", "Fire", "Earth" };


bool NFGameServerModule::Init()
{
    m_pKernelModule = pPluginManager->FindModule<NFIKernelModule>();
    m_pClassModule = pPluginManager->FindModule<NFIClassModule>();
    m_pSceneModule = pPluginManager->FindModule<NFISceneModule>();
    m_pNetModule = pPluginManager->FindModule<NFINetModule>();
    return true;
}

bool NFGameServerModule::Shut()
{

    return true;
}

bool NFGameServerModule::Execute()
{
#ifdef _DEBUG
    /*
    char szContent[MAX_PATH] = { 0 };
    if (kbhit() && gets(szContent))
    {
        NFDataList val(szContent, ",");
        if (val.GetCount() > 0)
        {
            //const char* pstrCmd = val.String( 0 );
            m_pKernelModule->Command(val);

        }
    }
    */
#endif

    int64_t currentTime = NFGetTimeS();
    int deltaTime = currentTime - preTime;
    preTime = currentTime;

    vector<int> groups = m_pSceneModule->GetGroups(3);
    for (int& id : groups) {

        if (id == 0) continue;

        NF_SHARE_PTR<NFIRecord> chessPlaneOne = m_pSceneModule->FindRecord(3, id, NFrame::Group::ChessPlane1::ThisName());

        int gameState = m_pSceneModule->GetPropertyInt(3, id, NFrame::Group::GameState());

        NFDataList playerList;
        m_pKernelModule->GetGroupObjectList(3, id, playerList, true);
        // come in preparation stage  
        if (!gameState) {

            for (int i = 0; i < playerList.GetCount(); i++) {
                const NFGUID& playerId = playerList.Object(i);
                m_pKernelModule->SetPropertyInt(playerId, NFrame::Player::State(), 0);
            }

            int currentTime = m_pSceneModule->GetPropertyInt(3, id, NFrame::Group::GameTime());
            currentTime += deltaTime;

            if(currentTime < preparationDuration)

                m_pSceneModule->SetPropertyInt(3, id, NFrame::Group::GameTime(), currentTime);
            else {

                NF_SHARE_PTR<NFIRecord> playerPlane = m_pKernelModule->FindRecord(playerList.Object(0), NFrame::Player::ChessPlane::ThisName());

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        const NFGUID id = playerPlane->GetObjectA(i, j);
                        chessPlaneOne->SetObject(i, j, id);
                    }
                }

                m_pSceneModule->SetPropertyInt(3, id, NFrame::Group::GameState(), 1);
                m_pSceneModule->SetPropertyInt(3, id, NFrame::Group::GameTime(), 0);

            }
        }
        else {
            /*int planeOne = m_pSceneModule->GetPropertyInt(3, id, NFrame::Group::PlaneOne());

            if (!planeOne) {
                fight(id, chessPlaneOne);
            }*/

            int currentTime = m_pSceneModule->GetPropertyInt(3, id, NFrame::Group::GameTime());
            currentTime += deltaTime;

            if (currentTime < preparationDuration)

                m_pSceneModule->SetPropertyInt(3, id, NFrame::Group::GameTime(), currentTime);
            else {
                m_pSceneModule->SetPropertyInt(3, id, NFrame::Group::GameState(), 0);
                m_pSceneModule->SetPropertyInt(3, id, NFrame::Group::GameTime(), 0);
            }
            //m_pSceneModule->SetPropertyInt(3, id, NFrame::Group::GameState(), 0);
        }

        
        //for (int i = 0; i < playerList.GetCount(); i++) {
        //    const NFGUID& playerId = playerList.Object(i);
        //    int gameStage = m_pKernelModule->GetPropertyInt(playerId, NFrame::Player::State());
        //    if (gameStage == 0) {
        //        // preparation stage
        //        int oldVal = m_pKernelModule->GetPropertyInt(playerId, NFrame::Player::GameTime());

        //        int newVal = oldVal + deltaTime;

        //        if (newVal > preparationDuration) {
        //            m_pKernelModule->SetPropertyInt(playerId, NFrame::Player::GameTime(), 0);
        //            // into combat stage
        //            m_pKernelModule->SetPropertyInt(playerId, NFrame::Player::State(), 1);
        //            m_pSceneModule->SetPropertyInt(3, id, NFrame::Group::PlaneOne(), 0);
        //        }
        //        else m_pKernelModule->SetPropertyInt(playerId, NFrame::Player::GameTime(), newVal);
        //    }
        //}

        
        
    }

    return true;
}

bool NFGameServerModule::AfterInit()
{

    m_pNetModule->AddReceiveCallBack(NFMsg::EVENT_REFRESH_SHOP, this, &NFGameServerModule::OnRefreshShop);
    m_pNetModule->AddReceiveCallBack(NFMsg::EVENT_BUY_LEVEL, this, &NFGameServerModule::OnBuyLvL);
    m_pNetModule->AddReceiveCallBack(NFMsg::EVENT_BUY_CHAMPION, this, &NFGameServerModule::OnBuyChampion);
    return true;
}

bool NFGameServerModule::BeforeShut()
{

    return true;
}


void NFGameServerModule::refreshShopItem(const NFGUID& id)
{
    std::shared_ptr<NFIRecord> record = m_pKernelModule->FindRecord(id, NFrame::Player::ChampionShop::ThisName());

    int rows = record->GetUsedRows();
    if (rows < 5) {
        for (int i = 0; i < 5; i++) {
            string race = raceType[(int)m_pKernelModule->Random(0, 3)];
            string element = elementType[(int)m_pKernelModule->Random(0, 5)];

            NFDataList tmp;
            tmp.AddString(element);
            tmp.AddString(race);
            tmp.AddInt(3);
            record->AddRow(-1, tmp);
        }
    }
    else {
        for (int i = 0; i < 5; i++) {
            string race = raceType[(int)m_pKernelModule->Random(0, 3)];
            string element = elementType[(int)m_pKernelModule->Random(0, 5)];
            record->SetString(i, NFrame::Player::ChampionShop::ElementType, element);
            record->SetString(i, NFrame::Player::ChampionShop::RaceType, race);
            record->SetInt(i, NFrame::Player::ChampionShop::Cost, 3);
        }
    }


    cout << record->GetUsedRows();
    cout << record->ToString() << endl;
    std::shared_ptr<NFIRecord> ownRecord = m_pKernelModule->FindRecord(id, NFrame::Player::ownInventory::ThisName());
    cout << ownRecord->ToString() << endl;

    std::shared_ptr<NFIRecord> planeRecord = m_pKernelModule->FindRecord(id, NFrame::Player::ChessPlane::ThisName());
    cout << planeRecord->ToString() << endl;
}

void NFGameServerModule::OnRefreshShop(const NFSOCK sockIndex, const int msgID, const char* msg, const uint32_t len)
{
    NFGUID clientID;
    NFMsg::ReqAckSwapScene xMsg;
    if (!m_pNetModule->ReceivePB(msgID, msg, len, xMsg, clientID))
    {
        return;
    }
    int goldVal = m_pKernelModule->GetPropertyInt(clientID, NFrame::Player::GameGold());
    if (goldVal >= 2) {
        goldVal -= 2;
        if (goldVal < 0) goldVal = 0;
        m_pKernelModule->SetPropertyInt(clientID, NFrame::Player::GameGold(), goldVal);

        refreshShopItem(clientID);
    }

}

void NFGameServerModule::OnBuyLvL(const NFSOCK sockIndex, const int msgID, const char* msg, const uint32_t len)
{
    NFGUID clientID;
    NFMsg::ReqAckSwapScene xMsg;
    if (!m_pNetModule->ReceivePB(msgID, msg, len, xMsg, clientID))
    {
        return;
    }
    int goldVal = m_pKernelModule->GetPropertyInt(clientID, NFrame::Player::GameGold());
    if (goldVal >= 4) {
        goldVal -= 4;
        if (goldVal < 0) goldVal = 0;

        m_pKernelModule->SetPropertyInt(clientID, NFrame::Player::GameGold(), goldVal);

        int lvlVal = m_pKernelModule->GetPropertyInt(clientID, NFrame::Player::GameLVL());
        m_pKernelModule->SetPropertyInt(clientID, NFrame::Player::GameLVL(), ++lvlVal);
    }
}

void NFGameServerModule::OnBuyChampion(const NFSOCK sockIndex, const int msgID, const char* msg, const uint32_t len)
{
    NFGUID clientID;
    NFMsg::ReqAckSwapScene xMsg;
    if (!m_pNetModule->ReceivePB(msgID, msg, len, xMsg, clientID))
    {
        return;
    }
    int index = xMsg.x();
    std::shared_ptr<NFIRecord> championShops = m_pKernelModule->FindRecord(clientID, NFrame::Player::ChampionShop::ThisName());
    string element = championShops->GetString(index, NFrame::Player::ChampionShop::ElementType);
    string race = championShops->GetString(index, NFrame::Player::ChampionShop::RaceType);
    int goldVal = m_pKernelModule->GetPropertyInt(clientID, NFrame::Player::GameGold());
    if (goldVal >= 3 && SetHeroOnInventory(clientID, element, race)) {
        goldVal -= 3;
        m_pKernelModule->SetPropertyInt(clientID, NFrame::Player::GameGold(), goldVal);
        m_pNetModule->SendMsgPB(NFMsg::ACK_EVENT_SELL_CHAMPION, xMsg, sockIndex);
    }

}

bool NFGameServerModule::SetHeroOnInventory(NFGUID self, const string& element, const string& race)
{
    std::shared_ptr<NFIRecord> ownInventory = m_pKernelModule->FindRecord(self, NFrame::Player::ownInventory::ThisName());
    int rows = ownInventory->GetUsedRows();
    if (rows >= 9)return false;
    int groupID = m_pKernelModule->GetPropertyInt32(self, NFrame::Player::GroupID());

    NF_SHARE_PTR<NFSceneInfo> pSceneInfo = m_pSceneModule->GetElement(3);
    if (!pSceneInfo)
    {
        return false;
    }

    string name = element;
    name += race;

    NFGUID npcID = m_pKernelModule->CreateGUID();
    m_pKernelModule->CreateObject(npcID, 3, groupID, NFrame::NPC::ThisName(), name, NFDataList::Empty());
    m_pKernelModule->SetPropertyObject(npcID, NFrame::NPC::MasterID(), self);
    m_pKernelModule->SetPropertyInt(npcID, NFrame::NPC::State(), 1);
    m_pKernelModule->SetPropertyString(npcID, NFrame::NPC::ElementType(), element);
    m_pKernelModule->SetPropertyString(npcID, NFrame::NPC::RaceType(), race);

    NFGUID clonedNpcID = m_pKernelModule->CreateGUID();
    m_pKernelModule->CreateObject(clonedNpcID, 3, groupID, NFrame::NPC::ThisName(), name, NFDataList::Empty());
    m_pKernelModule->SetPropertyInt(clonedNpcID, NFrame::NPC::State(), 0);
    m_pKernelModule->SetPropertyString(clonedNpcID, NFrame::NPC::ElementType(), element);
    m_pKernelModule->SetPropertyString(clonedNpcID, NFrame::NPC::RaceType(), race);
    m_pKernelModule->SetPropertyObject(clonedNpcID, NFrame::NPC::MasterID(), self);
    m_pKernelModule->SetPropertyObject(npcID, NFrame::NPC::Mirror(), clonedNpcID);


    NFDataList data;
    data.Add(npcID);
    ownInventory->AddRow(-1, data);

    return true;
}

void NFGameServerModule::fight(int group, const NF_SHARE_PTR<NFIRecord>& record)
{
    if (record->GetUsedRows() < 8) return;

    NFGUID empty(0, 0);
    for (int row = 0; row < 8; row++) {
        for (int col = 0; col < 7; col++) {

            NFGUID id = record->GetObjectA(row, col);
            if (id != empty) {

                NFGUID target = m_pKernelModule->GetPropertyObject(id, NFrame::NPC::Target());
                if (target == empty) {
                    target = SearchEnemy(id, record);
                    //if (target == empty) m_pSceneModule->SetPropertyInt(3, group, NFrame::Group::PlaneOne(), 1);
                    m_pKernelModule->SetPropertyObject(id, NFrame::NPC::Target(), target, 0);
                }
            }
        }
    }
}

NFGUID NFGameServerModule::SearchEnemy(const NFGUID& self, const NF_SHARE_PTR<NFIRecord>& record)
{
    NFGUID empty(0, 0);

    NFGUID selfMaster = m_pKernelModule->GetPropertyObject(self, NFrame::NPC::MasterID());
    for (int row = 0; row < 8; row++) {
        for (int col = 0; col < 7; col++) {
            NFGUID id = record->GetObjectA(row, col);
            if (id != empty) {

                NFGUID masterID = m_pKernelModule->GetPropertyObject(id, NFrame::NPC::MasterID());
                if (selfMaster != masterID) return id;
            }
        }
    }
    return empty;
}
