// -------------------------------------------------------------------------
//    @FileName         :    NFProtocolDefine.hpp
//    @Author           :    NFrame Studio
//    @Module           :    NFProtocolDefine
// -------------------------------------------------------------------------

#ifndef NF_PR_NAME_HPP
#define NF_PR_NAME_HPP

#include <string>
namespace NFrame
{
	class CooldownRecord
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "CooldownRecord"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		// Record
		class Cooldown
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "Cooldown"; return x; };
			static const int ConfigID = 0;//string
			static const int Time = 1;//int

		};

	};
	class EffectData
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "EffectData"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& ARMOR(){ static std::string x = "ARMOR"; return x; };// int
		static const std::string& ATK_POSITION(){ static std::string x = "ATK_POSITION"; return x; };// int
		static const std::string& ATK_SPEED(){ static std::string x = "ATK_SPEED"; return x; };// float
		static const std::string& ATK_VALUE(){ static std::string x = "ATK_VALUE"; return x; };// int
		static const std::string& ATTACK_RANGE(){ static std::string x = "ATTACK_RANGE"; return x; };// int
		static const std::string& EVASION(){ static std::string x = "EVASION"; return x; };// int
		static const std::string& HPREGEN(){ static std::string x = "HPREGEN"; return x; };// int
		static const std::string& MAGIC_DEF(){ static std::string x = "MAGIC_DEF"; return x; };// int
		static const std::string& MAGIC_VALUE(){ static std::string x = "MAGIC_VALUE"; return x; };// int
		static const std::string& MAXHP(){ static std::string x = "MAXHP"; return x; };// float
		static const std::string& MAXMP(){ static std::string x = "MAXMP"; return x; };// int
		static const std::string& MOVE_SPEED(){ static std::string x = "MOVE_SPEED"; return x; };// int
		static const std::string& SUCKBLOOD(){ static std::string x = "SUCKBLOOD"; return x; };// int
		// Record

	};
	class GM
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "GM"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& Level(){ static std::string x = "Level"; return x; };// int
		// Record

	};
	class Group
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "Group"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& GameState(){ static std::string x = "GameState"; return x; };// int
		static const std::string& GameTime(){ static std::string x = "GameTime"; return x; };// int
		static const std::string& PlaneOne(){ static std::string x = "PlaneOne"; return x; };// int
		static const std::string& PlayerCount(){ static std::string x = "PlayerCount"; return x; };// int
		static const std::string& PlayerOne(){ static std::string x = "PlayerOne"; return x; };// object
		static const std::string& PlayerTwo(){ static std::string x = "PlayerTwo"; return x; };// object
		// Record
		class ChessPlane1
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "ChessPlane1"; return x; };
			static const int HeroOne = 0;//object
			static const int HeroTwo = 1;//object
			static const int HeroThree = 2;//object
			static const int HeroFour = 3;//object
			static const int HeroFive = 4;//object
			static const int HeroSix = 5;//object
			static const int HeroSeven = 6;//object
			static const int HeroEight = 7;//object

		};
		class ChessPlane2
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "ChessPlane2"; return x; };
			static const int HeroOne = 0;//object
			static const int HeroTwo = 1;//object
			static const int HeroThree = 2;//object
			static const int HeroFour = 3;//object
			static const int HeroFive = 4;//object
			static const int HeroSix = 5;//object
			static const int HeroSeven = 6;//object
			static const int HeroEight = 7;//object

		};

	};
	class IObject
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "IObject"; return x; };		// Property
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Record

	};
	class Item
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "Item"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& CoolDownTime(){ static std::string x = "CoolDownTime"; return x; };// float
		static const std::string& Description(){ static std::string x = "Description"; return x; };// string
		static const std::string& EffectData(){ static std::string x = "EffectData"; return x; };// string
		static const std::string& Icon(){ static std::string x = "Icon"; return x; };// string
		static const std::string& ShowName(){ static std::string x = "ShowName"; return x; };// string
		static const std::string& SkillId(){ static std::string x = "SkillId"; return x; };// string
		static const std::string& SpriteFile(){ static std::string x = "SpriteFile"; return x; };// string
		// Record

	};
	class Language
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "Language"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& Chinese(){ static std::string x = "Chinese"; return x; };// string
		static const std::string& English(){ static std::string x = "English"; return x; };// string
		static const std::string& French(){ static std::string x = "French"; return x; };// string
		static const std::string& Spanish(){ static std::string x = "Spanish"; return x; };// string
		// Record

	};
	class NPC
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "NPC"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& Cost(){ static std::string x = "Cost"; return x; };// int
		static const std::string& DescID(){ static std::string x = "DescID"; return x; };// string
		static const std::string& EffectData(){ static std::string x = "EffectData"; return x; };// string
		static const std::string& ElementType(){ static std::string x = "ElementType"; return x; };// string
		static const std::string& HP(){ static std::string x = "HP"; return x; };// float
		static const std::string& Icon(){ static std::string x = "Icon"; return x; };// string
		static const std::string& Level(){ static std::string x = "Level"; return x; };// int
		static const std::string& MP(){ static std::string x = "MP"; return x; };// int
		static const std::string& Prefab(){ static std::string x = "Prefab"; return x; };// string
		static const std::string& RaceType(){ static std::string x = "RaceType"; return x; };// string
		static const std::string& ShowCard(){ static std::string x = "ShowCard"; return x; };// string
		static const std::string& ShowName(){ static std::string x = "ShowName"; return x; };// string
		static const std::string& SpriteFile(){ static std::string x = "SpriteFile"; return x; };// string
		static const std::string& Target(){ static std::string x = "Target"; return x; };// object
		// Include Property, come from EffectData 
		static const std::string& ARMOR(){ static std::string x = "ARMOR"; return x; };// int
		static const std::string& ATK_POSITION(){ static std::string x = "ATK_POSITION"; return x; };// int
		static const std::string& ATK_SPEED(){ static std::string x = "ATK_SPEED"; return x; };// float
		static const std::string& ATK_VALUE(){ static std::string x = "ATK_VALUE"; return x; };// int
		static const std::string& ATTACK_RANGE(){ static std::string x = "ATTACK_RANGE"; return x; };// int
		static const std::string& EVASION(){ static std::string x = "EVASION"; return x; };// int
		static const std::string& HPREGEN(){ static std::string x = "HPREGEN"; return x; };// int
		static const std::string& MAGIC_DEF(){ static std::string x = "MAGIC_DEF"; return x; };// int
		static const std::string& MAGIC_VALUE(){ static std::string x = "MAGIC_VALUE"; return x; };// int
		static const std::string& MAXHP(){ static std::string x = "MAXHP"; return x; };// float
		static const std::string& MAXMP(){ static std::string x = "MAXMP"; return x; };// int
		static const std::string& MOVE_SPEED(){ static std::string x = "MOVE_SPEED"; return x; };// int
		static const std::string& SUCKBLOOD(){ static std::string x = "SUCKBLOOD"; return x; };// int
		// Include Property, come from CooldownRecord 
		// Record
		class Cooldown
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "Cooldown"; return x; };
			static const int ConfigID = 0;//string
			static const int Time = 1;//int

		};

	};
	class NoSqlServer
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "NoSqlServer"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& Auth(){ static std::string x = "Auth"; return x; };// string
		static const std::string& IP(){ static std::string x = "IP"; return x; };// string
		static const std::string& Port(){ static std::string x = "Port"; return x; };// int
		static const std::string& ServerID(){ static std::string x = "ServerID"; return x; };// int
		// Record

	};
	class Player
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "Player"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& Account(){ static std::string x = "Account"; return x; };// string
		static const std::string& BattlePoint(){ static std::string x = "BattlePoint"; return x; };// int
		static const std::string& ClanID(){ static std::string x = "ClanID"; return x; };// object
		static const std::string& ConnectKey(){ static std::string x = "ConnectKey"; return x; };// string
		static const std::string& Diamond(){ static std::string x = "Diamond"; return x; };// int
		static const std::string& EXP(){ static std::string x = "EXP"; return x; };// int
		static const std::string& GMLevel(){ static std::string x = "GMLevel"; return x; };// int
		static const std::string& GameGold(){ static std::string x = "GameGold"; return x; };// int
		static const std::string& GameID(){ static std::string x = "GameID"; return x; };// int
		static const std::string& GameLVL(){ static std::string x = "GameLVL"; return x; };// int
		static const std::string& GameTime(){ static std::string x = "GameTime"; return x; };// int
		static const std::string& GateID(){ static std::string x = "GateID"; return x; };// int
		static const std::string& Gold(){ static std::string x = "Gold"; return x; };// int
		static const std::string& Group(){ static std::string x = "Group"; return x; };// int
		static const std::string& HP(){ static std::string x = "HP"; return x; };// int
		static const std::string& Head(){ static std::string x = "Head"; return x; };// string
		static const std::string& HeroCount(){ static std::string x = "HeroCount"; return x; };// int
		static const std::string& LastOfflineTime(){ static std::string x = "LastOfflineTime"; return x; };// object
		static const std::string& Level(){ static std::string x = "Level"; return x; };// int
		static const std::string& MAXEXP(){ static std::string x = "MAXEXP"; return x; };// int
		static const std::string& MaxHero(){ static std::string x = "MaxHero"; return x; };// int
		static const std::string& NickName(){ static std::string x = "NickName"; return x; };// string
		static const std::string& NoticeID(){ static std::string x = "NoticeID"; return x; };// int
		static const std::string& OnlineCount(){ static std::string x = "OnlineCount"; return x; };// int
		static const std::string& OnlineTime(){ static std::string x = "OnlineTime"; return x; };// object
		static const std::string& Sex(){ static std::string x = "Sex"; return x; };// int
		static const std::string& TeamID(){ static std::string x = "TeamID"; return x; };// object
		static const std::string& TotalTime(){ static std::string x = "TotalTime"; return x; };// int
		// Include Property, come from EffectData 
		static const std::string& ARMOR(){ static std::string x = "ARMOR"; return x; };// int
		static const std::string& ATK_POSITION(){ static std::string x = "ATK_POSITION"; return x; };// int
		static const std::string& ATK_SPEED(){ static std::string x = "ATK_SPEED"; return x; };// float
		static const std::string& ATK_VALUE(){ static std::string x = "ATK_VALUE"; return x; };// int
		static const std::string& ATTACK_RANGE(){ static std::string x = "ATTACK_RANGE"; return x; };// int
		static const std::string& EVASION(){ static std::string x = "EVASION"; return x; };// int
		static const std::string& HPREGEN(){ static std::string x = "HPREGEN"; return x; };// int
		static const std::string& MAGIC_DEF(){ static std::string x = "MAGIC_DEF"; return x; };// int
		static const std::string& MAGIC_VALUE(){ static std::string x = "MAGIC_VALUE"; return x; };// int
		static const std::string& MAXHP(){ static std::string x = "MAXHP"; return x; };// float
		static const std::string& MAXMP(){ static std::string x = "MAXMP"; return x; };// int
		static const std::string& MOVE_SPEED(){ static std::string x = "MOVE_SPEED"; return x; };// int
		static const std::string& SUCKBLOOD(){ static std::string x = "SUCKBLOOD"; return x; };// int
		// Include Property, come from CooldownRecord 
		// Record
		class ChampionShop
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "ChampionShop"; return x; };
			static const int ElementType = 0;//string
			static const int RaceType = 1;//string
			static const int Cost = 2;//int

		};
		class ChessPlane
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "ChessPlane"; return x; };
			static const int HeroOne = 0;//object
			static const int HeroTwo = 1;//object
			static const int HeroThree = 2;//object
			static const int HeroFour = 3;//object
			static const int HeroFive = 4;//object
			static const int HeroSix = 5;//object
			static const int HeroSeven = 6;//object
			static const int HeroEight = 7;//object

		};
		class HeroEquipmentList
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "HeroEquipmentList"; return x; };
			static const int HeroID = 0;//object
			static const int EquipmentID = 1;//object
			static const int SlotIndex = 2;//int

		};
		class HeroList
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "HeroList"; return x; };
			static const int GUID = 0;//object
			static const int ItemConfigID = 1;//string
			static const int ConfigID = 2;//string
			static const int Activated = 3;//int
			static const int Level = 4;//int
			static const int Exp = 5;//int
			static const int Star = 6;//int
			static const int HP = 7;//int

		};
		class opponentInventory
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "opponentInventory"; return x; };
			static const int GUID = 0;//object

		};
		class ownInventory
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "ownInventory"; return x; };
			static const int GUID = 0;//object

		};
		class Cooldown
		{
		public:
			//Class name
			static const std::string& ThisName(){ static std::string x = "Cooldown"; return x; };
			static const int ConfigID = 0;//string
			static const int Time = 1;//int

		};

	};
	class Scene
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "Scene"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& ActorID(){ static std::string x = "ActorID"; return x; };// int
		static const std::string& FilePath(){ static std::string x = "FilePath"; return x; };// string
		static const std::string& LoadingUI(){ static std::string x = "LoadingUI"; return x; };// string
		static const std::string& MaxGroup(){ static std::string x = "MaxGroup"; return x; };// int
		static const std::string& MaxGroupPlayers(){ static std::string x = "MaxGroupPlayers"; return x; };// int
		static const std::string& NavigationResPath(){ static std::string x = "NavigationResPath"; return x; };// string
		static const std::string& RelivePos(){ static std::string x = "RelivePos"; return x; };// string
		static const std::string& ResPath(){ static std::string x = "ResPath"; return x; };// string
		static const std::string& SceneName(){ static std::string x = "SceneName"; return x; };// string
		static const std::string& SceneShowName(){ static std::string x = "SceneShowName"; return x; };// string
		static const std::string& SoundList(){ static std::string x = "SoundList"; return x; };// string
		static const std::string& Type(){ static std::string x = "Type"; return x; };// int
		static const std::string& Width(){ static std::string x = "Width"; return x; };// int
		// Record

	};
	class Security
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "Security"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& SecurityData(){ static std::string x = "SecurityData"; return x; };// string
		// Record

	};
	class Server
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "Server"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& Area(){ static std::string x = "Area"; return x; };// int
		static const std::string& CpuCount(){ static std::string x = "CpuCount"; return x; };// int
		static const std::string& IP(){ static std::string x = "IP"; return x; };// string
		static const std::string& MaxOnline(){ static std::string x = "MaxOnline"; return x; };// int
		static const std::string& Port(){ static std::string x = "Port"; return x; };// int
		static const std::string& ServerID(){ static std::string x = "ServerID"; return x; };// int
		static const std::string& Type(){ static std::string x = "Type"; return x; };// int
		static const std::string& UDPPort(){ static std::string x = "UDPPort"; return x; };// int
		static const std::string& WSPort(){ static std::string x = "WSPort"; return x; };// int
		static const std::string& WebPort(){ static std::string x = "WebPort"; return x; };// int
		// Record

	};
	class Skill
	{
	public:
		//Class name
		static const std::string& ThisName(){ static std::string x = "Skill"; return x; };		// IObject
		static const std::string& CampID(){ static std::string x = "CampID"; return x; };// object
		static const std::string& ClassName(){ static std::string x = "ClassName"; return x; };// string
		static const std::string& ConfigID(){ static std::string x = "ConfigID"; return x; };// string
		static const std::string& Connection(){ static std::string x = "Connection"; return x; };// int
		static const std::string& Disable(){ static std::string x = "Disable"; return x; };// int
		static const std::string& GMMoveTo(){ static std::string x = "GMMoveTo"; return x; };// vector3
		static const std::string& GroupID(){ static std::string x = "GroupID"; return x; };// int
		static const std::string& ID(){ static std::string x = "ID"; return x; };// string
		static const std::string& MasterID(){ static std::string x = "MasterID"; return x; };// object
		static const std::string& Mirror(){ static std::string x = "Mirror"; return x; };// object
		static const std::string& MoveTo(){ static std::string x = "MoveTo"; return x; };// vector3
		static const std::string& Name(){ static std::string x = "Name"; return x; };// string
		static const std::string& Position(){ static std::string x = "Position"; return x; };// vector3
		static const std::string& SceneID(){ static std::string x = "SceneID"; return x; };// int
		static const std::string& State(){ static std::string x = "State"; return x; };// int
		// Property
		static const std::string& AnimaState(){ static std::string x = "AnimaState"; return x; };// string
		static const std::string& AtkDis(){ static std::string x = "AtkDis"; return x; };// float
		static const std::string& ConsumeProperty(){ static std::string x = "ConsumeProperty"; return x; };// string
		static const std::string& ConsumeType(){ static std::string x = "ConsumeType"; return x; };// int
		static const std::string& ConsumeValue(){ static std::string x = "ConsumeValue"; return x; };// int
		static const std::string& CoolDownTime(){ static std::string x = "CoolDownTime"; return x; };// float
		static const std::string& DamageProperty(){ static std::string x = "DamageProperty"; return x; };// string
		static const std::string& DamageValue(){ static std::string x = "DamageValue"; return x; };// int
		static const std::string& DefaultHitTime(){ static std::string x = "DefaultHitTime"; return x; };// string
		static const std::string& Desc(){ static std::string x = "Desc"; return x; };// string
		static const std::string& EffectObjType(){ static std::string x = "EffectObjType"; return x; };// int
		static const std::string& GetBuffList(){ static std::string x = "GetBuffList"; return x; };// string
		static const std::string& Icon(){ static std::string x = "Icon"; return x; };// string
		static const std::string& NewObject(){ static std::string x = "NewObject"; return x; };// string
		static const std::string& ScriptObject(){ static std::string x = "ScriptObject"; return x; };// string
		static const std::string& SendBuffList(){ static std::string x = "SendBuffList"; return x; };// string
		static const std::string& ShowName(){ static std::string x = "ShowName"; return x; };// string
		static const std::string& SkillType(){ static std::string x = "SkillType"; return x; };// int
		static const std::string& SpriteFile(){ static std::string x = "SpriteFile"; return x; };// string
		// Record

	};

}
#endif