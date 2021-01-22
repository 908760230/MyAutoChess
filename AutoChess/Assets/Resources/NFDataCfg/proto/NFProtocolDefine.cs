// -------------------------------------------------------------------------
//    @FileName         :    NFProtocolDefine.cs
//    @Author           :    NFrame Studio
//    @Module           :    NFProtocolDefine
// -------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace NFrame
{
	public class CommonChessPlane
	{
		//Class name
		public static readonly String ThisName = "CommonChessPlane";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		// Record
		public class ChessPlane1
		{
			//Class name
			public static readonly String ThisName = "ChessPlane1";
			public const int HeroOne = 0;//object
			public const int HeroTwo = 1;//object
			public const int HeroThree = 2;//object
			public const int HeroFour = 3;//object
			public const int HeroFive = 4;//object
			public const int HeroSix = 5;//object
			public const int HeroSeven = 6;//object

		}
		public class ChessPlane2
		{
			//Class name
			public static readonly String ThisName = "ChessPlane2";
			public const int HeroOne = 0;//object
			public const int HeroTwo = 1;//object
			public const int HeroThree = 2;//object
			public const int HeroFour = 3;//object
			public const int HeroFive = 4;//object
			public const int HeroSix = 5;//object
			public const int HeroSeven = 6;//object

		}
		public class ChessPlane3
		{
			//Class name
			public static readonly String ThisName = "ChessPlane3";
			public const int HeroOne = 0;//object
			public const int HeroTwo = 1;//object
			public const int HeroThree = 2;//object
			public const int HeroFour = 3;//object
			public const int HeroFive = 4;//object
			public const int HeroSix = 5;//object
			public const int HeroSeven = 6;//object

		}
		public class ChessPlane4
		{
			//Class name
			public static readonly String ThisName = "ChessPlane4";
			public const int HeroOne = 0;//object
			public const int HeroTwo = 1;//object
			public const int HeroThree = 2;//object
			public const int HeroFour = 3;//object
			public const int HeroFive = 4;//object
			public const int HeroSix = 5;//object
			public const int HeroSeven = 6;//object

		}

	}
	public class CooldownRecord
	{
		//Class name
		public static readonly String ThisName = "CooldownRecord";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		// Record
		public class Cooldown
		{
			//Class name
			public static readonly String ThisName = "Cooldown";
			public const int ConfigID = 0;//string
			public const int Time = 1;//int

		}

	}
	public class EffectData
	{
		//Class name
		public static readonly String ThisName = "EffectData";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String ARMOR = "ARMOR";// int
		public static readonly String ATK_POSITION = "ATK_POSITION";// int
		public static readonly String ATK_SPEED = "ATK_SPEED";// float
		public static readonly String ATK_VALUE = "ATK_VALUE";// int
		public static readonly String ATTACK_RANGE = "ATTACK_RANGE";// int
		public static readonly String EVASION = "EVASION";// int
		public static readonly String HPREGEN = "HPREGEN";// int
		public static readonly String MAGIC_DEF = "MAGIC_DEF";// int
		public static readonly String MAGIC_VALUE = "MAGIC_VALUE";// int
		public static readonly String MAXHP = "MAXHP";// int
		public static readonly String MAXMP = "MAXMP";// int
		public static readonly String MOVE_SPEED = "MOVE_SPEED";// int
		public static readonly String SUCKBLOOD = "SUCKBLOOD";// int
		// Record

	}
	public class GM
	{
		//Class name
		public static readonly String ThisName = "GM";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String Level = "Level";// int
		// Record

	}
	public class Group
	{
		//Class name
		public static readonly String ThisName = "Group";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String Completed = "Completed";// int
		public static readonly String PlayerCount = "PlayerCount";// int
		// Record
		public class ChessPlane1
		{
			//Class name
			public static readonly String ThisName = "ChessPlane1";
			public const int HeroOne = 0;//object
			public const int HeroTwo = 1;//object
			public const int HeroThree = 2;//object
			public const int HeroFour = 3;//object
			public const int HeroFive = 4;//object
			public const int HeroSix = 5;//object
			public const int HeroSeven = 6;//object

		}
		public class ChessPlane2
		{
			//Class name
			public static readonly String ThisName = "ChessPlane2";
			public const int HeroOne = 0;//object
			public const int HeroTwo = 1;//object
			public const int HeroThree = 2;//object
			public const int HeroFour = 3;//object
			public const int HeroFive = 4;//object
			public const int HeroSix = 5;//object
			public const int HeroSeven = 6;//object

		}

	}
	public class IObject
	{
		//Class name
		public static readonly String ThisName = "IObject";
		// Property
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Record

	}
	public class Item
	{
		//Class name
		public static readonly String ThisName = "Item";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String CoolDownTime = "CoolDownTime";// float
		public static readonly String Description = "Description";// string
		public static readonly String EffectData = "EffectData";// string
		public static readonly String Icon = "Icon";// string
		public static readonly String ShowName = "ShowName";// string
		public static readonly String SkillId = "SkillId";// string
		public static readonly String SpriteFile = "SpriteFile";// string
		// Record

	}
	public class Language
	{
		//Class name
		public static readonly String ThisName = "Language";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String Chinese = "Chinese";// string
		public static readonly String English = "English";// string
		public static readonly String French = "French";// string
		public static readonly String Spanish = "Spanish";// string
		// Record

	}
	public class NPC
	{
		//Class name
		public static readonly String ThisName = "NPC";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String Cost = "Cost";// int
		public static readonly String DescID = "DescID";// string
		public static readonly String EffectData = "EffectData";// string
		public static readonly String ElementType = "ElementType";// string
		public static readonly String HP = "HP";// int
		public static readonly String Icon = "Icon";// string
		public static readonly String Level = "Level";// int
		public static readonly String MP = "MP";// int
		public static readonly String Prefab = "Prefab";// string
		public static readonly String RaceType = "RaceType";// string
		public static readonly String ShowCard = "ShowCard";// string
		public static readonly String ShowName = "ShowName";// string
		public static readonly String SpriteFile = "SpriteFile";// string
		public static readonly String Target = "Target";// object
		// Include Property, come from EffectData 
		public static readonly String ARMOR = "ARMOR";// int
		public static readonly String ATK_POSITION = "ATK_POSITION";// int
		public static readonly String ATK_SPEED = "ATK_SPEED";// float
		public static readonly String ATK_VALUE = "ATK_VALUE";// int
		public static readonly String ATTACK_RANGE = "ATTACK_RANGE";// int
		public static readonly String EVASION = "EVASION";// int
		public static readonly String HPREGEN = "HPREGEN";// int
		public static readonly String MAGIC_DEF = "MAGIC_DEF";// int
		public static readonly String MAGIC_VALUE = "MAGIC_VALUE";// int
		public static readonly String MAXHP = "MAXHP";// int
		public static readonly String MAXMP = "MAXMP";// int
		public static readonly String MOVE_SPEED = "MOVE_SPEED";// int
		public static readonly String SUCKBLOOD = "SUCKBLOOD";// int
		// Include Property, come from CooldownRecord 
		// Record
		public class Cooldown
		{
			//Class name
			public static readonly String ThisName = "Cooldown";
			public const int ConfigID = 0;//string
			public const int Time = 1;//int

		}

	}
	public class NoSqlServer
	{
		//Class name
		public static readonly String ThisName = "NoSqlServer";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String Auth = "Auth";// string
		public static readonly String IP = "IP";// string
		public static readonly String Port = "Port";// int
		public static readonly String ServerID = "ServerID";// int
		// Record

	}
	public class Player
	{
		//Class name
		public static readonly String ThisName = "Player";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String Account = "Account";// string
		public static readonly String BattlePoint = "BattlePoint";// int
		public static readonly String ClanID = "ClanID";// object
		public static readonly String ConnectKey = "ConnectKey";// string
		public static readonly String Diamond = "Diamond";// int
		public static readonly String EXP = "EXP";// int
		public static readonly String GMLevel = "GMLevel";// int
		public static readonly String GameGold = "GameGold";// int
		public static readonly String GameID = "GameID";// int
		public static readonly String GameLVL = "GameLVL";// int
		public static readonly String GameTime = "GameTime";// int
		public static readonly String GateID = "GateID";// int
		public static readonly String Gold = "Gold";// int
		public static readonly String Group = "Group";// int
		public static readonly String HP = "HP";// int
		public static readonly String Head = "Head";// string
		public static readonly String HeroCount = "HeroCount";// int
		public static readonly String LastOfflineTime = "LastOfflineTime";// object
		public static readonly String Level = "Level";// int
		public static readonly String MAXEXP = "MAXEXP";// int
		public static readonly String MaxHero = "MaxHero";// int
		public static readonly String NickName = "NickName";// string
		public static readonly String NoticeID = "NoticeID";// int
		public static readonly String OnlineCount = "OnlineCount";// int
		public static readonly String OnlineTime = "OnlineTime";// object
		public static readonly String Sex = "Sex";// int
		public static readonly String TeamID = "TeamID";// object
		public static readonly String TotalTime = "TotalTime";// int
		// Include Property, come from EffectData 
		public static readonly String ARMOR = "ARMOR";// int
		public static readonly String ATK_POSITION = "ATK_POSITION";// int
		public static readonly String ATK_SPEED = "ATK_SPEED";// float
		public static readonly String ATK_VALUE = "ATK_VALUE";// int
		public static readonly String ATTACK_RANGE = "ATTACK_RANGE";// int
		public static readonly String EVASION = "EVASION";// int
		public static readonly String HPREGEN = "HPREGEN";// int
		public static readonly String MAGIC_DEF = "MAGIC_DEF";// int
		public static readonly String MAGIC_VALUE = "MAGIC_VALUE";// int
		public static readonly String MAXHP = "MAXHP";// int
		public static readonly String MAXMP = "MAXMP";// int
		public static readonly String MOVE_SPEED = "MOVE_SPEED";// int
		public static readonly String SUCKBLOOD = "SUCKBLOOD";// int
		// Include Property, come from CooldownRecord 
		// Record
		public class ChampionShop
		{
			//Class name
			public static readonly String ThisName = "ChampionShop";
			public const int ElementType = 0;//string
			public const int RaceType = 1;//string
			public const int Cost = 2;//int

		}
		public class ChessPlane
		{
			//Class name
			public static readonly String ThisName = "ChessPlane";
			public const int HeroOne = 0;//object
			public const int HeroTwo = 1;//object
			public const int HeroThree = 2;//object
			public const int HeroFour = 3;//object
			public const int HeroFive = 4;//object
			public const int HeroSix = 5;//object
			public const int HeroSeven = 6;//object

		}
		public class HeroEquipmentList
		{
			//Class name
			public static readonly String ThisName = "HeroEquipmentList";
			public const int HeroID = 0;//object
			public const int EquipmentID = 1;//object
			public const int SlotIndex = 2;//int

		}
		public class HeroList
		{
			//Class name
			public static readonly String ThisName = "HeroList";
			public const int GUID = 0;//object
			public const int ItemConfigID = 1;//string
			public const int ConfigID = 2;//string
			public const int Activated = 3;//int
			public const int Level = 4;//int
			public const int Exp = 5;//int
			public const int Star = 6;//int
			public const int HP = 7;//int

		}
		public class opponentInventory
		{
			//Class name
			public static readonly String ThisName = "opponentInventory";
			public const int GUID = 0;//object

		}
		public class ownInventory
		{
			//Class name
			public static readonly String ThisName = "ownInventory";
			public const int GUID = 0;//object

		}
		public class Cooldown
		{
			//Class name
			public static readonly String ThisName = "Cooldown";
			public const int ConfigID = 0;//string
			public const int Time = 1;//int

		}

	}
	public class Scene
	{
		//Class name
		public static readonly String ThisName = "Scene";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String ActorID = "ActorID";// int
		public static readonly String FilePath = "FilePath";// string
		public static readonly String LoadingUI = "LoadingUI";// string
		public static readonly String MaxGroup = "MaxGroup";// int
		public static readonly String MaxGroupPlayers = "MaxGroupPlayers";// int
		public static readonly String NavigationResPath = "NavigationResPath";// string
		public static readonly String RelivePos = "RelivePos";// string
		public static readonly String ResPath = "ResPath";// string
		public static readonly String SceneName = "SceneName";// string
		public static readonly String SceneShowName = "SceneShowName";// string
		public static readonly String SoundList = "SoundList";// string
		public static readonly String Type = "Type";// int
		public static readonly String Width = "Width";// int
		// Record

	}
	public class Security
	{
		//Class name
		public static readonly String ThisName = "Security";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String SecurityData = "SecurityData";// string
		// Record

	}
	public class Server
	{
		//Class name
		public static readonly String ThisName = "Server";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String Area = "Area";// int
		public static readonly String CpuCount = "CpuCount";// int
		public static readonly String IP = "IP";// string
		public static readonly String MaxOnline = "MaxOnline";// int
		public static readonly String Port = "Port";// int
		public static readonly String ServerID = "ServerID";// int
		public static readonly String Type = "Type";// int
		public static readonly String UDPPort = "UDPPort";// int
		public static readonly String WSPort = "WSPort";// int
		public static readonly String WebPort = "WebPort";// int
		// Record

	}
	public class Skill
	{
		//Class name
		public static readonly String ThisName = "Skill";
		// IObject
		public static readonly String CampID = "CampID";// object
		public static readonly String ClassName = "ClassName";// string
		public static readonly String ConfigID = "ConfigID";// string
		public static readonly String Connection = "Connection";// int
		public static readonly String Disable = "Disable";// int
		public static readonly String GMMoveTo = "GMMoveTo";// vector3
		public static readonly String GroupID = "GroupID";// int
		public static readonly String ID = "ID";// string
		public static readonly String MasterID = "MasterID";// object
		public static readonly String Mirror = "Mirror";// object
		public static readonly String MoveTo = "MoveTo";// vector3
		public static readonly String Name = "Name";// string
		public static readonly String Position = "Position";// vector3
		public static readonly String SceneID = "SceneID";// int
		public static readonly String State = "State";// int
		// Property
		public static readonly String AnimaState = "AnimaState";// string
		public static readonly String AtkDis = "AtkDis";// float
		public static readonly String ConsumeProperty = "ConsumeProperty";// string
		public static readonly String ConsumeType = "ConsumeType";// int
		public static readonly String ConsumeValue = "ConsumeValue";// int
		public static readonly String CoolDownTime = "CoolDownTime";// float
		public static readonly String DamageProperty = "DamageProperty";// string
		public static readonly String DamageValue = "DamageValue";// int
		public static readonly String DefaultHitTime = "DefaultHitTime";// string
		public static readonly String Desc = "Desc";// string
		public static readonly String EffectObjType = "EffectObjType";// int
		public static readonly String GetBuffList = "GetBuffList";// string
		public static readonly String Icon = "Icon";// string
		public static readonly String NewObject = "NewObject";// string
		public static readonly String ScriptObject = "ScriptObject";// string
		public static readonly String SendBuffList = "SendBuffList";// string
		public static readonly String ShowName = "ShowName";// string
		public static readonly String SkillType = "SkillType";// int
		public static readonly String SpriteFile = "SpriteFile";// string
		// Record

	}

}