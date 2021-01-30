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

#ifndef NF_GAMESERVER_MODULE_H
#define NF_GAMESERVER_MODULE_H

#include "NFComm/NFCore/NFMap.hpp"
#include "NFComm/NFPluginModule/NFIKernelModule.h"
#include "NFComm/NFPluginModule/NFIClassModule.h"
#include "NFComm/NFPluginModule/NFISceneModule.h"
#include "NFComm/NFPluginModule/NFINetModule.h"


class NFIGameServerModule
    : public NFIModule
{
public:
    virtual void refreshShopItem(const NFGUID& id) {}
};

class NFGameServerModule
    : public NFIGameServerModule
{
public:
    NFGameServerModule(NFIPluginManager* p)
    {
        m_bIsExecute = true;
        pPluginManager = p;
    }
    virtual ~NFGameServerModule() {};

    virtual bool Init();
    virtual bool Shut();
    virtual bool Execute();

    virtual bool AfterInit();
    virtual bool BeforeShut();

    virtual void refreshShopItem(const NFGUID& id);
protected:
    void OnRefreshShop(const NFSOCK sockIndex, const int msgID, const char* msg, const uint32_t len);
    void OnBuyLvL(const NFSOCK sockIndex, const int msgID, const char* msg, const uint32_t len);
    void OnBuyChampion(const NFSOCK sockIndex, const int msgID, const char* msg, const uint32_t len);

    bool SetHeroOnInventory(NFGUID self, const string& element, const string& race);

protected:
    int64_t preTime;
    int preparationDuration = 15;
    NFISceneModule* m_pSceneModule;
    NFIClassModule* m_pClassModule;
    NFIKernelModule* m_pKernelModule;
    NFINetModule* m_pNetModule;
private:
};

#endif
