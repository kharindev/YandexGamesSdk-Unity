using System;
using System.Collections.Generic;


[Serializable]
public struct ScopePermissions
{
    public string avatar;
    public string public_name;
}
[Serializable]

public struct Player
{
    public string lang;
    public string publicName;
    public ScopePermissions scopePermissions;
    public string uniqueID;
    public string avatarUrlSmall;
    public string avatarUrlMedium;
    public string avatarUrlLarge;
}

[Serializable]
public struct Entry
{
    public int score;
    public string extraData;
    public int rank;
    public Player player;
    public string formattedScore;
}

[Serializable]
public struct Options
{
    public int decimal_offset;
}


[Serializable]
public struct ScoreFormat
{
    public string type;
    public Options options;
}


[Serializable]
public struct Description
{
    public bool invert_sort_order;
    public ScoreFormat score_format;
}


[Serializable]
public struct Title
{
    public string ru;
    public string en;
}


[Serializable]
public struct Leaderboard
{
    public int appID;
    public string name;
    public bool @default;
    public Description description;
    public Title title;
}


[Serializable]
public struct Range
{
    public int start;
    public int size;
}


[Serializable]
public struct LeaderboardObject
{
    public List<Entry> entries;
    public Leaderboard leaderboard;
    public List<Range> ranges;
    public int userRank;
}

[Serializable]
public struct RewardedError {
    public string id;
    public string error;
}

[Serializable]
public struct UserData {
    public string id;
    public string name;
    public string avatarUrlSmall;
    public string avatarUrlMedium;
    public string avatarUrlLarge;
    public bool isAuthorized;
}

[Serializable]
public struct DeviceInfo {
    public string type;
    public bool IsMobile => type.Equals(DeviceType.Mobile);
    public bool IsDesktop => type.Equals(DeviceType.Desktop);
    public bool IsTablet => type.Equals(DeviceType.Tablet);
    public bool IsTv => type.Equals(DeviceType.Tv);
    public bool IsMobilePlatform => IsMobile || IsTablet;
}

public class DeviceType
{
    public const string Desktop = "desktop";
    public const string Mobile = "mobile";
    public const string Tablet = "tablet";
    public const string Tv = "tv";
}

[Serializable]
public struct LoadedData
{
    public string key;
    public string data;
}