using System.Text.Json.Serialization;

namespace Stardeck.Models;

public partial class Gamelog
{
    public string Gameid { get; set; } = null!;

    public string? Log { get; set; }
    [JsonIgnore] public virtual Gameroom? Game { get; set; } = null!;

    public void LogCard(string playerid, string cardid, string territoryid)
    {
        LogMsg($"{playerid} ha jugado la carta {cardid} en el territorio {territoryid}");
    }
    public void LogDrawError(string playerId)
    {
        LogMsg($"{playerId} no puede robar cartas por deck vacio");
    }
    public void LogDraw(string playerId, string drawedId)
    {
        LogMsg($"Player {playerId} robo la carta {drawedId}");
    }

    public void LogMsg(string msg)
    {
        Log += msg;
        Log += "\n";
    }



}